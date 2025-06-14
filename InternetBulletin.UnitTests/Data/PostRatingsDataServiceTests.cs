// *********************************************************************************
//	<copyright file="PostRatingsDataServiceTests.cs" company="Personal">
//		Copyright (c) 2025 Personal
//	</copyright>
// <summary>The Post Ratings Data Service Tests class.</summary>
// *********************************************************************************

namespace InternetBulletin.UnitTests.Data
{
    using InternetBulletin.Data;
    using InternetBulletin.Data.DataServices;
    using InternetBulletin.Data.Entities;
    using InternetBulletin.Shared.Constants;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Logging;
    using Moq;

    /// <summary>
    /// The Post Ratings Data Service Tests class.
    /// </summary>
    public class PostRatingsDataServiceTests
    {
        /// <summary>
        /// The mock logger.
        /// </summary>
        private readonly Mock<ILogger<PostRatingsDataService>> _mockLogger;

        /// <summary>
        /// The mock db context.
        /// </summary>
        private readonly SqlDbContext _mockDbContext;

        /// <summary>
        /// The post ratings data service.
        /// </summary>
        private readonly PostRatingsDataService _postRatingsDataService;

        /// <summary>
        /// The database name for in-memory database.
        /// </summary>
        private readonly string _databaseName;

        /// <summary>
        /// Initializes a new instance of the <see cref="PostRatingsDataServiceTests"/> class.
        /// </summary>
        public PostRatingsDataServiceTests()
        {
            this._mockLogger = new Mock<ILogger<PostRatingsDataService>>();
            this._databaseName = Guid.NewGuid().ToString();
            var options = new DbContextOptionsBuilder<SqlDbContext>()
                .UseInMemoryDatabase(databaseName: this._databaseName)
                .Options;
            this._mockDbContext = new SqlDbContext(options);
            this._postRatingsDataService = new PostRatingsDataService(this._mockDbContext, this._mockLogger.Object);
        }

        /// <summary>
        /// Tests that GetAllUserPostRatingsAsync returns all active ratings for a user.
        /// </summary>
        [Fact]
        public async Task GetAllUserPostRatingsAsync_ReturnsActiveUserRatings()
        {
            // Arrange
            var userName = "TestUser";
            var ratings = new List<PostRating>
            {
                new() { PostId = Guid.NewGuid(), UserName = userName, RatingValue = 5, IsActive = true },
                new() { PostId = Guid.NewGuid(), UserName = userName, RatingValue = 4, IsActive = true },
                new() { PostId = Guid.NewGuid(), UserName = "OtherUser", RatingValue = 3, IsActive = true },
                new() { PostId = Guid.NewGuid(), UserName = userName, RatingValue = 2, IsActive = false }
            };
            await this._mockDbContext.PostRatings.AddRangeAsync(ratings);
            await this._mockDbContext.SaveChangesAsync();

            // Act
            var result = await this._postRatingsDataService.GetAllUserPostRatingsAsync(userName);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count);
            Assert.All(result, r => Assert.Equal(userName, r.UserName));
            Assert.All(result, r => Assert.True(r.IsActive));
        }

        /// <summary>
        /// Tests that GetPostRatingAsync returns the correct rating for a post and user.
        /// </summary>
        [Fact]
        public async Task GetPostRatingAsync_ReturnsCorrectRating()
        {
            // Arrange
            var postId = Guid.NewGuid();
            var userName = "TestUser";
            var rating = new PostRating
            {
                PostId = postId,
                UserName = userName,
                RatingValue = 5,
                IsActive = true,
                RatedOn = DateTime.UtcNow
            };
            await this._mockDbContext.PostRatings.AddAsync(rating);
            await this._mockDbContext.SaveChangesAsync();

            // Act
            var result = await this._postRatingsDataService.GetPostRatingAsync(postId, userName);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(postId, result.PostId);
            Assert.Equal(userName, result.UserName);
            Assert.Equal(5, result.RatingValue);
        }

        /// <summary>
        /// Tests that GetPostRatingAsync returns empty rating when not found.
        /// </summary>
        [Fact]
        public async Task GetPostRatingAsync_NotFound_ReturnsEmptyRating()
        {
            // Arrange
            var postId = Guid.NewGuid();
            var userName = "TestUser";

            // Act
            var result = await this._postRatingsDataService.GetPostRatingAsync(postId, userName);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(default, result.PostId);
            Assert.Empty(result.UserName);
            Assert.Equal(0, result.RatingValue);
        }

        /// <summary>
        /// Tests that AddPostRatingAsync successfully adds a new rating.
        /// </summary>
        [Fact]
        public async Task AddPostRatingAsync_AddsNewRating()
        {
            // Arrange
            var postId = Guid.NewGuid();
            var userName = "TestUser";
            var rating = new PostRating
            {
                PostId = postId,
                UserName = userName,
                RatingValue = 5,
                IsActive = true,
                RatedOn = DateTime.UtcNow
            };

            // Act
            await this._postRatingsDataService.AddPostRatingAsync(rating);

            // Assert
            var savedRating = await this._mockDbContext.PostRatings.FirstOrDefaultAsync(r => r.PostId == postId && r.UserName == userName);
            Assert.NotNull(savedRating);
            Assert.Equal(rating.RatingValue, savedRating.RatingValue);
            Assert.Equal(rating.IsActive, savedRating.IsActive);
        }

        /// <summary>
        /// Tests that UpdatePostRatingAsync successfully updates an existing rating.
        /// </summary>
        [Fact]
        public async Task UpdatePostRatingAsync_UpdatesExistingRating()
        {
            // Arrange
            var postId = Guid.NewGuid();
            var userName = "TestUser";
            var originalRating = new PostRating
            {
                PostId = postId,
                UserName = userName,
                RatingValue = 3,
                IsActive = true,
                RatedOn = DateTime.UtcNow.AddDays(-1)
            };
            await this._mockDbContext.PostRatings.AddAsync(originalRating);
            await this._mockDbContext.SaveChangesAsync();

            var updatedRating = new PostRating
            {
                PostId = postId,
                UserName = userName,
                RatingValue = 5,
                IsActive = true
            };

            // Act
            await this._postRatingsDataService.UpdatePostRatingAsync(updatedRating);

            // Assert
            var savedRating = await this._mockDbContext.PostRatings.FirstOrDefaultAsync(r => r.PostId == postId && r.UserName == userName);
            Assert.NotNull(savedRating);
            Assert.True((DateTime.UtcNow - savedRating.RatedOn).TotalSeconds < 5);
        }

        /// <summary>
        /// Tests that UpdatePostRatingAsync throws exception when rating not found.
        /// </summary>
        [Fact]
        public async Task UpdatePostRatingAsync_NotFound_ThrowsException()
        {
            // Arrange
            var rating = new PostRating
            {
                PostId = Guid.NewGuid(),
                UserName = "TestUser",
                RatingValue = 5,
                IsActive = true
            };

            // Act & Assert
            var exception = await Assert.ThrowsAsync<Exception>(
                () => this._postRatingsDataService.UpdatePostRatingAsync(rating));
            Assert.Equal(ExceptionConstants.PostNotFoundMessageConstant, exception.Message);
        }

        /// <summary>
        /// Tests that GetAllPostsWithRatingsAsync returns correct posts with ratings.
        /// </summary>
        [Fact]
        public async Task GetAllPostsWithRatingsAsync_ReturnsPostsWithRatings()
        {
            // Arrange
            var userName = "TestUser";
            var posts = new List<Post>
            {
                new() { PostId = Guid.NewGuid(), PostTitle = "Post1", IsActive = true },
                new() { PostId = Guid.NewGuid(), PostTitle = "Post2", IsActive = true },
                new() { PostId = Guid.NewGuid(), PostTitle = "Post3", IsActive = false }
            };
            await this._mockDbContext.Posts.AddRangeAsync(posts);

            var ratings = new List<PostRating>
            {
                new() { PostId = posts[0].PostId, UserName = userName, RatingValue = 5, IsActive = true },
                new() { PostId = posts[1].PostId, UserName = "OtherUser", RatingValue = 4, IsActive = true }
            };
            await this._mockDbContext.PostRatings.AddRangeAsync(ratings);
            await this._mockDbContext.SaveChangesAsync();

            // Act
            var result = await this._postRatingsDataService.GetAllPostsWithRatingsAsync(userName);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count); // Only active posts
            var postWithRating = result.First(p => p.PostId == posts[0].PostId);
            Assert.Equal(5, postWithRating.RatingValue);
            var postWithoutRating = result.First(p => p.PostId == posts[1].PostId);
            Assert.Equal(0, postWithoutRating.RatingValue);
        }
    }
}