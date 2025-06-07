// *********************************************************************************
//	<copyright file="PostsServiceTests.cs" company="Personal">
//		Copyright (c) 2025 Personal
//	</copyright>
// <summary>The Posts Services Test Class.</summary>
// *********************************************************************************

namespace InternetBulletin.UnitTests.Business
{
    using static InternetBulletin.UnitTests.Helpers.TestsHelper;

    /// <summary>
    /// Posts service tests.
    /// </summary>
    public class PostsServiceTests
    {
        /// <summary>
        /// The post id guid.
        /// </summary>
        private static readonly string PostIdGuid = Guid.NewGuid().ToString();

        /// <summary>
        /// The logger mock.
        /// </summary>
        private readonly Mock<ILogger<PostsService>> _loggerMock;

        /// <summary>
        /// The posts data service mock.
        /// </summary>
        private readonly Mock<IPostsDataService> _postsDataServiceMock;

        /// <summary>
        /// The post ratings data service mock.
        /// </summary>
        private readonly Mock<IPostRatingsDataService> _postRatingsDataServiceMock;      

        /// <summary>
        /// The posts service.
        /// </summary>
        private readonly PostsService _postsService;

        /// <summary>
        /// Initializes a new instance of the <see cref="PostsServiceTests"/> class.
        /// </summary>
        public PostsServiceTests()
        {
            this._loggerMock = new Mock<ILogger<PostsService>>();
            this._postsDataServiceMock = new Mock<IPostsDataService>();
            this._postRatingsDataServiceMock = new Mock<IPostRatingsDataService>();
            this._postsService = new PostsService(this._loggerMock.Object, this._postsDataServiceMock.Object, this._postRatingsDataServiceMock.Object);
        }

        /// <summary>
        /// Gets post async valid post id returns post.
        /// </summary>
        [Fact]
        public async Task GetPostAsync_ValidPostId_ReturnsPost()
        {
            // Arrange
            var expectedPost = PrepareNewPostDataDTO(PostIdGuid, 0);
            this._postsDataServiceMock.Setup(x => x.GetPostAsync(It.IsAny<Guid>(), It.IsAny<string>(), true)).ReturnsAsync(expectedPost);

            // Act
            var result = await this._postsService.GetPostAsync(PostIdGuid, UserName);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(expectedPost.PostId, result.PostId);
        }

        /// <summary>
        /// Gets post async invalid post id throws exception.
        /// </summary>
        [Fact]
        public async Task GetPostAsync_InvalidPostId_ThrowsException()
        {
            // Arrange
            var invalidPostId = "invalid-guid";
            var userName = UserName;

            // Act & Assert
            await Assert.ThrowsAsync<Exception>(() =>
                this._postsService.GetPostAsync(invalidPostId, userName));
        }

        /// <summary>
        /// Adds new post async valid post returns true.
        /// </summary>
        [Fact]
        public async Task AddNewPostAsync_ValidPost_ReturnsTrue()
        {
            // Arrange
            var newPost = PrepareNewAddPostDataDTO();
            this._postsDataServiceMock.Setup(x => x.AddNewPostAsync(It.IsAny<AddPostDTO>(), It.IsAny<string>())).ReturnsAsync(true);

            // Act
            var result = await this._postsService.AddNewPostAsync(newPost, UserName);

            // Assert
            Assert.True(result);
        }

        /// <summary>
        /// Updates post async valid post returns updated post.
        /// </summary>
        [Fact]
        public async Task UpdatePostAsync_ValidPost_ReturnsUpdatedPost()
        {
            // Arrange
            var updatedPost = PrepareNewUpdatePostDataDTO(PostIdGuid);
            var expectedPost = PrepareNewPostDataDTO(PostIdGuid, 1);
            this._postsDataServiceMock.Setup(x => x.UpdatePostAsync(It.IsAny<UpdatePostDTO>(), It.IsAny<string>(), false)).ReturnsAsync(expectedPost);

            // Act
            var result = await this._postsService.UpdatePostAsync(updatedPost, UserName);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(expectedPost.PostId, result.PostId);
        }

        /// <summary>
        /// Deletes post async valid post id returns true.
        /// </summary>
        [Fact]
        public async Task DeletePostAsync_ValidPostId_ReturnsTrue()
        {
            // Arrange
            this._postsDataServiceMock.Setup(x => x.DeletePostAsync(It.IsAny<Guid>(), It.IsAny<string>())).ReturnsAsync(true);

            // Act
            var result = await this._postsService.DeletePostAsync(PostIdGuid, UserName);

            // Assert
            Assert.True(result);
        }

        /// <summary>
        /// Gets all posts async with user name returns posts with ratings.
        /// </summary>
        [Fact]
        public async Task GetAllPostsAsync_WithUserName_ReturnsPostsWithRatings()
        {
            // Arrange
            var expectedPosts = PreparePostWithRatingsDTO();
            this._postRatingsDataServiceMock.Setup(x => x.GetAllPostsWithRatingsAsync(It.IsAny<string>())).ReturnsAsync(expectedPosts);

            // Act
            var result = await _postsService.GetAllPostsAsync(UserName);

            // Assert
            Assert.NotNull(result);
            Assert.NotNull(result);
            Assert.Equal(expectedPosts[0].PostId, result[0].PostId);
        }

        /// <summary>
        /// Gets all posts async without user name returns all posts.
        /// </summary>
        [Fact]
        public async Task GetAllPostsAsync_WithoutUserName_ReturnsAllPosts()
        {
            // Arrange
            var expectedPosts = PreparePostsDataForUser();
            this._postsDataServiceMock.Setup(x => x.GetAllPostsAsync()).ReturnsAsync(expectedPosts);

            // Act
            var result = await this._postsService.GetAllPostsAsync(null!);

            // Assert
            Assert.NotNull(result);
            Assert.NotNull(result);
            Assert.Equal(expectedPosts[0].PostId, result[0].PostId);
        }

        /// <summary>
        /// Adds new post async null post throws exception.
        /// </summary>
        [Fact]
        public async Task AddNewPostAsync_NullPost_ThrowsException()
        {
            // Arrange
            AddPostDTO nullPost = null!;

            // Act & Assert
            await Assert.ThrowsAsync<Exception>(() => this._postsService.AddNewPostAsync(nullPost, UserName));
        }

        /// <summary>
        /// Updates post async null post throws exception.
        /// </summary>
        [Fact]
        public async Task UpdatePostAsync_NullPost_ThrowsException()
        {
            // Arrange
            UpdatePostDTO nullPost = null!;

            // Act & Assert
            await Assert.ThrowsAsync<Exception>(() => this._postsService.UpdatePostAsync(nullPost, UserName));
        }
    }
}