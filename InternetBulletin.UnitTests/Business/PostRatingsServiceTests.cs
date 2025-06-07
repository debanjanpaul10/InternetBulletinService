// *********************************************************************************
//	<copyright file="PostRatingsServiceTests.cs" company="Personal">
//		Copyright (c) 2025 Personal
//	</copyright>
// <summary>The Posts Ratings Services Test Class.</summary>
// *********************************************************************************

namespace InternetBulletin.UnitTests.Business
{
    using System;
    using System.Threading.Tasks;
    using InternetBulletin.Business.Contracts;
    using InternetBulletin.Business.Services;
    using InternetBulletin.Data.Contracts;
    using InternetBulletin.Data.Entities;
    using InternetBulletin.Shared.DTOs.Posts;
    using Microsoft.Extensions.Logging;
    using Moq;
    using Xunit;
    using static InternetBulletin.UnitTests.Helpers.TestsHelper;

    /// <summary>
    /// Post ratings service tests.
    /// </summary>
    public class PostRatingsServiceTests
    {
        /// <summary>
        /// The post id guid.
        /// </summary>
        private static readonly string PostIdGuid = Guid.NewGuid().ToString();

        /// <summary>
        /// The _logger mock.
        /// </summary>
        private readonly Mock<ILogger<PostRatingsService>> _loggerMock;

        /// <summary>
        /// The _post ratings data service mock.
        /// </summary>
        private readonly Mock<IPostRatingsDataService> _postRatingsDataServiceMock;

        /// <summary>
        /// The _posts data service mock.
        /// </summary>
        private readonly Mock<IPostsDataService> _postsDataServiceMock;

        /// <summary>
        /// The post ratings service.
        /// </summary>
        private readonly PostRatingsService _postRatingsService;

        /// <summary>
        /// Initializes a new instance of the <see cref="PostRatingsServiceTests"/> class.
        /// </summary>
        public PostRatingsServiceTests()
        {
            this._loggerMock = new Mock<ILogger<PostRatingsService>>();
            this._postRatingsDataServiceMock = new Mock<IPostRatingsDataService>();
            this._postsDataServiceMock = new Mock<IPostsDataService>();

            this._postRatingsService = new PostRatingsService(
                this._loggerMock.Object, this._postRatingsDataServiceMock.Object, this._postsDataServiceMock.Object);
        }

        /// <summary>
        /// Updates rating async first time rating increments rating.
        /// </summary>
        [Fact]
        public async Task UpdateRatingAsync_FirstTimeRating_IncrementsRating()
        {
            // Arrange
            var post = PrepareNewPostDataDTO(PostIdGuid, 0);
            PostRating nullPostRating = null!;

            this._postsDataServiceMock.Setup(x => x.GetPostAsync(It.IsAny<Guid>(), It.IsAny<string>(), false)).ReturnsAsync(post);
            this._postRatingsDataServiceMock.Setup(x => x.GetPostRatingAsync(It.IsAny<Guid>(), It.IsAny<string>())).ReturnsAsync(nullPostRating);

            // Act
            var result = await this._postRatingsService.UpdateRatingAsync(PostIdGuid, true, UserName);

            // Assert
            Assert.True(result.IsUpdateSuccess);
            Assert.False(result.HasAlreadyUpdated);
            this._postsDataServiceMock.Verify(x => x.UpdatePostAsync(It.IsAny<UpdatePostDTO>(), It.IsAny<string>(), true), Times.Once);
            this._postRatingsDataServiceMock.Verify(x => x.AddPostRatingAsync(It.IsAny<PostRating>()), Times.Once);
        }

        /// <summary>
        /// Updates rating async existing rating zero increments rating.
        /// </summary>
        [Fact]
        public async Task UpdateRatingAsync_ExistingRatingZero_IncrementsRating()
        {
            // Arrange
            var post = PrepareNewPostDataDTO(PostIdGuid, 0);
            var existingRating = PrepareNewPostRatingDataDTO(PostIdGuid, 0);

            this._postsDataServiceMock.Setup(x => x.GetPostAsync(It.IsAny<Guid>(), It.IsAny<string>(), false)).ReturnsAsync(post);
            this._postRatingsDataServiceMock.Setup(x => x.GetPostRatingAsync(It.IsAny<Guid>(), It.IsAny<string>())).ReturnsAsync(existingRating);

            // Act
            var result = await this._postRatingsService.UpdateRatingAsync(PostIdGuid, true, UserName);

            // Assert
            Assert.True(result.IsUpdateSuccess);
            Assert.True(result.HasAlreadyUpdated);
            this._postsDataServiceMock.Verify(x => x.UpdatePostAsync(It.IsAny<UpdatePostDTO>(), It.IsAny<string>(), true), Times.Once);
            this._postRatingsDataServiceMock.Verify(x => x.UpdatePostRatingAsync(It.IsAny<PostRating>()), Times.Once);
        }

        /// <summary>
        /// Updates rating async existing rating one decrements rating.
        /// </summary>
        [Fact]
        public async Task UpdateRatingAsync_ExistingRatingOne_DecrementsRating()
        {
            // Arrange

            var post = PrepareNewPostDataDTO(PostIdGuid, 1);
            var existingRating = PrepareNewPostRatingDataDTO(PostIdGuid, 1);

            this._postsDataServiceMock.Setup(x => x.GetPostAsync(It.IsAny<Guid>(), It.IsAny<string>(), false)).ReturnsAsync(post);
            this._postRatingsDataServiceMock.Setup(x => x.GetPostRatingAsync(It.IsAny<Guid>(), It.IsAny<string>())).ReturnsAsync(existingRating);

            // Act
            var result = await this._postRatingsService.UpdateRatingAsync(PostIdGuid, false, UserName);

            // Assert
            Assert.True(result.IsUpdateSuccess);
            Assert.True(result.HasAlreadyUpdated);
            this._postsDataServiceMock.Verify(x => x.UpdatePostAsync(It.IsAny<UpdatePostDTO>(), It.IsAny<string>(), true), Times.Once);
            this._postRatingsDataServiceMock.Verify(x => x.UpdatePostRatingAsync(It.IsAny<PostRating>()), Times.Once);
        }

        /// <summary>
        /// Updates rating async invalid post id throws exception.
        /// </summary>
        [Fact]
        public async Task UpdateRatingAsync_InvalidPostId_ThrowsException()
        {
            // Arrange
            var invalidPostId = "invalid-guid";

            // Act & Assert
            await Assert.ThrowsAsync<Exception>(() => this._postRatingsService.UpdateRatingAsync(invalidPostId, true, UserName));
        }

        /// <summary>
        /// Updates rating async post not found throws exception.
        /// </summary>
        [Fact]
        public async Task UpdateRatingAsync_PostNotFound_ThrowsException()
        {
            // Arrange
            this._postsDataServiceMock.Setup(x => x.GetPostAsync(It.IsAny<Guid>(), It.IsAny<string>(), false)).ReturnsAsync((Post)null!);

            // Act & Assert
            await Assert.ThrowsAsync<Exception>(() => this._postRatingsService.UpdateRatingAsync(PostIdGuid, true, UserName));
        }

        /// <summary>
        /// Gets all user post ratings async returns user ratings.
        /// </summary>
        [Fact]
        public async Task GetAllUserPostRatingsAsync_ReturnsUserRatings()
        {
            // Arrange
            var expectedRatings = PrepareAllPostRatingsForUserData(UserName);
            this._postRatingsDataServiceMock.Setup(x => x.GetAllUserPostRatingsAsync(It.IsAny<string>())).ReturnsAsync(expectedRatings);

            // Act
            var result = await this._postRatingsService.GetAllUserPostRatingsAsync(UserName);

            // Assert
            Assert.NotNull(result);
            Assert.NotNull(result);
            Assert.Equal(expectedRatings[0].PostId, result[0].PostId);
            Assert.Equal(expectedRatings[0].UserName, result[0].UserName);
        }

        /// <summary>
        /// Updates rating async rating never goes below zero.
        /// </summary>
        [Fact]
        public async Task UpdateRatingAsync_RatingNeverGoesBelowZero()
        {
            // Arrange
            var post = PrepareNewPostDataDTO(PostIdGuid, 0);
            var existingRating = PrepareNewPostRatingDataDTO(PostIdGuid, 1);

            this._postsDataServiceMock.Setup(x => x.GetPostAsync(It.IsAny<Guid>(), It.IsAny<string>(), false)).ReturnsAsync(post);
            this._postRatingsDataServiceMock.Setup(x => x.GetPostRatingAsync(It.IsAny<Guid>(), It.IsAny<string>())).ReturnsAsync(existingRating);

            // Act
            var result = await this._postRatingsService.UpdateRatingAsync(PostIdGuid, false, UserName);

            // Assert
            Assert.True(result.IsUpdateSuccess);
            Assert.True(result.HasAlreadyUpdated);
            this._postsDataServiceMock.Verify(x => x.UpdatePostAsync(It.Is<UpdatePostDTO>(dto => dto.PostRating >= 0), It.IsAny<string>(), true), Times.Once);
        }
    }
}