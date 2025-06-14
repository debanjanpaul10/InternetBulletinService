// *********************************************************************************
//	<copyright file="PostsDataServiceTests.cs" company="Personal">
//		Copyright (c) 2025 Personal
//	</copyright>
// <summary>The Posts Data Serivce Tests class.</summary>
// *********************************************************************************

namespace InternetBulletin.UnitTests.Data
{
    using InternetBulletin.Data;
    using InternetBulletin.Data.DataServices;
    using InternetBulletin.Data.Entities;
    using InternetBulletin.Shared.Constants;
    using InternetBulletin.Shared.DTOs.Posts;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Logging;
    using Moq;
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Xunit;

    /// <summary>
    /// The Posts Data Serivce Tests class.
    /// </summary>
    public class PostsDataServiceTests
    {
        /// <summary>
        /// The SQL Db context.
        /// </summary>
        private readonly SqlDbContext _dbContext;

        /// <summary>
        /// The mock logger.
        /// </summary>
        private readonly Mock<ILogger<PostsDataService>> _mockLogger;

        /// <summary>
        /// The posts data service.
        /// </summary>
        private readonly PostsDataService _postsDataService;

        /// <summary>
        /// The database name.
        /// </summary>
        private readonly string _databaseName;

        /// <summary>
        /// Initializes a new instance of <see cref="PostsDataServiceTests"/>
        /// </summary>
        public PostsDataServiceTests()
        {
            _databaseName = Guid.NewGuid().ToString();
            var options = new DbContextOptionsBuilder<SqlDbContext>()
                .UseInMemoryDatabase(databaseName: _databaseName)
                .Options;
            _dbContext = new SqlDbContext(options);
            _mockLogger = new Mock<ILogger<PostsDataService>>();
            _postsDataService = new PostsDataService(_dbContext, _mockLogger.Object);

            // Seed data for tests
            _dbContext.Posts.AddRange(
                new Post { PostId = Guid.NewGuid(), PostTitle = "Test Post 1", PostContent = "Content 1", PostOwnerUserName = "user1", IsActive = true, PostCreatedDate = DateTime.UtcNow, Ratings = 0 },
                new Post { PostId = Guid.NewGuid(), PostTitle = "Test Post 2", PostContent = "Content 2", PostOwnerUserName = "user2", IsActive = true, PostCreatedDate = DateTime.UtcNow, Ratings = 0 },
                new Post { PostId = Guid.NewGuid(), PostTitle = "Test Post 3", PostContent = "Content 3", PostOwnerUserName = "user1", IsActive = false, PostCreatedDate = DateTime.UtcNow, Ratings = 0 }
            );
            _dbContext.SaveChanges();
        }

        /// <summary>
        /// Tests that GetPostAsync returns a post when the post exists and is for the current user.
        /// </summary>
        [Fact]
        public async Task GetPostAsync_ShouldReturnPost_WhenPostExistsAndIsForCurrentUser()
        {
            // Arrange
            var userName = "user1";
            var existingPost = _dbContext.Posts.First(p => p.PostOwnerUserName == userName && p.IsActive);

            // Act
            var result = await _postsDataService.GetPostAsync(existingPost.PostId, userName, true);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(existingPost.PostId, result.PostId);
            Assert.Equal(userName, result.PostOwnerUserName);

        }

        /// <summary>
        /// Tests that GetPostAsync returns a post when the post exists and is not for the current user.
        /// </summary>
        [Fact]
        public async Task GetPostAsync_ShouldReturnPost_WhenPostExistsAndIsNotForCurrentUser()
        {
            // Arrange
            var userName = "user1";
            var existingPost = _dbContext.Posts.First(p => p.PostOwnerUserName != userName && p.IsActive);

            // Act
            var result = await _postsDataService.GetPostAsync(existingPost.PostId, userName, false);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(existingPost.PostId, result.PostId);
            Assert.NotEqual(userName, result.PostOwnerUserName);

        }

        /// <summary>
        /// Tests that GetPostAsync returns an empty post when the post does not exist.
        /// </summary>
        [Fact]
        public async Task GetPostAsync_ShouldReturnEmptyPost_WhenPostDoesNotExist()
        {
            // Arrange
            var postId = Guid.NewGuid();
            var userName = "user1";

            // Act
            var result = await _postsDataService.GetPostAsync(postId, userName, true);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(Guid.Empty, result.PostId);

        }

        /// <summary>
        /// Tests that AddNewPostAsync successfully adds a new post to the database.
        /// </summary>
        [Fact]
        public async Task AddNewPostAsync_ShouldAddPostSuccessfully()
        {
            // Arrange
            var newPostDto = new AddPostDTO { PostTitle = "New Post", PostContent = "New Content" };
            var userName = "newUser";

            // Act
            var result = await _postsDataService.AddNewPostAsync(newPostDto, userName);

            // Assert
            Assert.True(result);
            var addedPost = await _dbContext.Posts.FirstOrDefaultAsync(p => p.PostTitle == newPostDto.PostTitle && p.PostOwnerUserName == userName);
            Assert.NotNull(addedPost);
            Assert.True(addedPost.IsActive);

        }

        /// <summary>
        /// Tests that UpdatePostAsync successfully updates an existing post.
        /// </summary>
        [Fact]
        public async Task UpdatePostAsync_ShouldUpdatePostSuccessfully()
        {
            // Arrange
            var userName = "user1";
            var existingPost = _dbContext.Posts.First(p => p.PostOwnerUserName == userName && p.IsActive);
            var updatedPostDto = new UpdatePostDTO
            {
                PostId = existingPost.PostId,
                PostTitle = "Updated Title",
                PostContent = "Updated Content"
            };

            // Act
            var result = await _postsDataService.UpdatePostAsync(updatedPostDto, userName, false);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(updatedPostDto.PostTitle, result.PostTitle);
            Assert.Equal(updatedPostDto.PostContent, result.PostContent);

        }

        /// <summary>
        /// Tests that UpdatePostAsync throws an exception when attempting to update a non-existent post.
        /// </summary>
        [Fact]
        public async Task UpdatePostAsync_ShouldThrowException_WhenPostNotFoundForUpdate()
        {
            // Arrange
            var userName = "user1";
            var updatedPostDto = new UpdatePostDTO
            {
                PostId = Guid.NewGuid(), // Non-existent ID
                PostTitle = "Updated Title",
                PostContent = "Updated Content"
            };

            // Act & Assert
            var exception = await Assert.ThrowsAsync<Exception>(() => _postsDataService.UpdatePostAsync(updatedPostDto, userName, false));
            Assert.Equal(ExceptionConstants.PostNotFoundMessageConstant, exception.Message);

        }

        /// <summary>
        /// Tests that UpdatePostAsync successfully updates the rating of an existing post.
        /// </summary>
        [Fact]
        public async Task UpdatePostAsync_ShouldUpdateRatingSuccessfully()
        {
            // Arrange
            var userName = "user1";
            var existingPost = _dbContext.Posts.First(p => p.PostOwnerUserName == userName && p.IsActive);
            var updatedPostDto = new UpdatePostDTO
            {
                PostId = existingPost.PostId,
                PostRating = 5
            };

            // Act
            var result = await _postsDataService.UpdatePostAsync(updatedPostDto, userName, true);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(updatedPostDto.PostRating, result.Ratings);

        }

        /// <summary>
        /// Tests that UpdatePostAsync throws an exception when attempting to update rating of a non-existent post.
        /// </summary>
        [Fact]
        public async Task UpdatePostAsync_ShouldThrowException_WhenPostNotFoundForRatingUpdate()
        {
            // Arrange
            var userName = "user1";
            var updatedPostDto = new UpdatePostDTO
            {
                PostId = Guid.NewGuid(), // Non-existent ID
                PostRating = 5
            };

            // Act & Assert
            var exception = await Assert.ThrowsAsync<Exception>(() => _postsDataService.UpdatePostAsync(updatedPostDto, userName, true));
            Assert.Equal(ExceptionConstants.PostNotFoundMessageConstant, exception.Message);

        }

        /// <summary>
        /// Tests that DeletePostAsync successfully deactivates an existing post.
        /// </summary>
        [Fact]
        public async Task DeletePostAsync_ShouldDeactivatePostSuccessfully()
        {
            // Arrange
            var userName = "user1";
            var existingPost = _dbContext.Posts.First(p => p.PostOwnerUserName == userName && p.IsActive);

            // Act
            var result = await _postsDataService.DeletePostAsync(existingPost.PostId, userName);

            // Assert
            Assert.True(result);
            var deactivatedPost = await _dbContext.Posts.FirstOrDefaultAsync(p => p.PostId == existingPost.PostId);
            Assert.NotNull(deactivatedPost);
            Assert.False(deactivatedPost.IsActive);

        }

        /// <summary>
        /// Tests that DeletePostAsync throws an exception when attempting to delete a non-existent post.
        /// </summary>
        [Fact]
        public async Task DeletePostAsync_ShouldThrowException_WhenPostNotFoundForDelete()
        {
            // Arrange
            var postId = Guid.NewGuid(); // Non-existent ID
            var userName = "user1";

            // Act & Assert
            var exception = await Assert.ThrowsAsync<Exception>(() => _postsDataService.DeletePostAsync(postId, userName));
            Assert.Equal(ExceptionConstants.PostNotFoundMessageConstant, exception.Message);

        }

        /// <summary>
        /// Tests that GetAllPostsAsync returns all active posts from the database.
        /// </summary>
        [Fact]
        public async Task GetAllPostsAsync_ShouldReturnAllActivePosts()
        {
            // Arrange
            var expectedActivePostsCount = _dbContext.Posts.Count(p => p.IsActive);

            // Act
            var result = await _postsDataService.GetAllPostsAsync();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(expectedActivePostsCount, result.Count);
            Assert.All(result, p => Assert.True(p.IsActive));
        }

        /// <summary>
        /// Tests that GetAllPostsAsync returns an empty list when no active posts exist in the database.
        /// </summary>
        [Fact]
        public async Task GetAllPostsAsync_ShouldReturnEmptyList_WhenNoActivePostsExist()
        {
            // Arrange
            _dbContext.Posts.RemoveRange(_dbContext.Posts); // Clear existing posts
            _dbContext.SaveChanges();

            // Act
            var result = await _postsDataService.GetAllPostsAsync();

            // Assert
            Assert.NotNull(result);
            Assert.Empty(result);

        }
    }
}