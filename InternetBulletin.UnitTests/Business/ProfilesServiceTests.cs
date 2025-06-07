// *********************************************************************************
//	<copyright file="ProfilesServiceTests.cs" company="Personal">
//		Copyright (c) 2025 Personal
//	</copyright>
// <summary>The Profiles Service Tests Class.</summary>
// *********************************************************************************

namespace InternetBulletin.UnitTests.Business
{
    using InternetBulletin.Business.Contracts;
    using InternetBulletin.Business.Services;
    using InternetBulletin.Data.Contracts;
    using InternetBulletin.Shared.Constants;
    using InternetBulletin.Shared.DTOs.Users;
    using Microsoft.Extensions.Logging;
    using Moq;
    using System;
    using System.Threading.Tasks;
    using Xunit;
    using static InternetBulletin.UnitTests.Helpers.TestsHelper;

    /// <summary>
    /// The Profiles Service Tests Class.
    /// </summary>
    public class ProfilesServiceTests
    {
        /// <summary>
        /// The logger mock.
        /// </summary>
        private readonly Mock<ILogger<ProfilesService>> _loggerMock;

        /// <summary>
        /// The profiles data service mock.
        /// </summary>
        private readonly Mock<IProfilesDataService> _profilesDataServiceMock;

        /// <summary>
        /// The users service mock.
        /// </summary>
        private readonly Mock<IUsersService> _usersServiceMock;

        /// <summary>
        /// The profiles service.
        /// </summary>
        private readonly ProfilesService _profilesService;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProfilesServiceTests"/> class.
        /// </summary>
        public ProfilesServiceTests()
        {
            this._loggerMock = new Mock<ILogger<ProfilesService>>();
            this._profilesDataServiceMock = new Mock<IProfilesDataService>();
            this._usersServiceMock = new Mock<IUsersService>();
            this._profilesService = new ProfilesService(
                this._loggerMock.Object,
                this._profilesDataServiceMock.Object,
                this._usersServiceMock.Object);
        }

        /// <summary>
        /// Tests that GetUserProfileDataAsync returns correct profile data when valid username is provided.
        /// </summary>
        [Fact]
        public async Task GetUserProfileDataAsync_ValidUserName_ReturnsProfileData()
        {
            // Arrange
            var graphUserData = PrepareGraphUserDTOData(UserName);
            var userPosts = PrepareUserPostsData(UserName);
            var userRatings = PrepareUserPostRatingsData();

            this._usersServiceMock.Setup(x => x.GetGraphUserDataAsync(It.IsAny<string>())).ReturnsAsync(graphUserData);
            this._profilesDataServiceMock.Setup(x => x.GetUserPostsAsync(It.IsAny<string>())).ReturnsAsync(userPosts);
            this._profilesDataServiceMock.Setup(x => x.GetUserRatingsAsync(It.IsAny<string>())).ReturnsAsync(userRatings);

            // Act
            var result = await this._profilesService.GetUserProfileDataAsync(UserName);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(graphUserData.DisplayName, result.DisplayName);
            Assert.Equal(graphUserData.EmailAddress, result.EmailAddress);
            Assert.Equal(graphUserData.UserName, result.UserName);
            Assert.NotNull(result.UserPosts);
            Assert.NotNull(result.UserPostRatings);
        }

        /// <summary>
        /// Tests that GetUserProfileDataAsync throws exception when username is null.
        /// </summary>
        [Fact]
        public async Task GetUserProfileDataAsync_NullUserName_ThrowsException()
        {
            // Arrange
            string userName = null!;

            // Act & Assert
            var exception = await Assert.ThrowsAsync<Exception>(() => this._profilesService.GetUserProfileDataAsync(userName));
            Assert.Equal(ExceptionConstants.UserIdCannotBeNullMessageConstant, exception.Message);
        }

        /// <summary>
        /// Tests that GetUserProfileDataAsync throws exception when username is empty.
        /// </summary>
        [Fact]
        public async Task GetUserProfileDataAsync_EmptyUserName_ThrowsException()
        {
            // Arrange
            var userName = string.Empty;

            // Act & Assert
            var exception = await Assert.ThrowsAsync<Exception>(() => this._profilesService.GetUserProfileDataAsync(userName));
            Assert.Equal(ExceptionConstants.UserIdCannotBeNullMessageConstant, exception.Message);
        }

        /// <summary>
        /// Tests that GetUserProfileDataAsync throws exception when user doesn't exist in Graph.
        /// </summary>
        [Fact]
        public async Task GetUserProfileDataAsync_UserNotFoundInGraph_ThrowsException()
        {
            // Arrange
            var userName = "nonexistentUser";
            this._usersServiceMock.Setup(x => x.GetGraphUserDataAsync(userName))
                .ReturnsAsync((GraphUserDTO)null!);

            // Act & Assert
            var exception = await Assert.ThrowsAsync<Exception>(() => this._profilesService.GetUserProfileDataAsync(userName));
            Assert.Equal(ExceptionConstants.UserDoesNotExistsMessageConstant, exception.Message);
        }

        /// <summary>
        /// Tests that GetUserProfileDataAsync returns empty collections when user has no posts or ratings.
        /// </summary>
        [Fact]
        public async Task GetUserProfileDataAsync_NoPostsOrRatings_ReturnsEmptyCollections()
        {
            // Arrange
            var graphUserData = PrepareGraphUserDTOData(UserName);

            this._usersServiceMock.Setup(x => x.GetGraphUserDataAsync(It.IsAny<string>())).ReturnsAsync(graphUserData);
            this._profilesDataServiceMock.Setup(x => x.GetUserPostsAsync(It.IsAny<string>())).ReturnsAsync([]);
            this._profilesDataServiceMock.Setup(x => x.GetUserRatingsAsync(It.IsAny<string>()))
                .ReturnsAsync([]);

            // Act
            var result = await this._profilesService.GetUserProfileDataAsync(UserName);

            // Assert
            Assert.NotNull(result);
            Assert.Empty(result.UserPosts);
            Assert.Empty(result.UserPostRatings);
        }

        /// <summary>
        /// Tests that GetUserProfileDataAsync correctly maps post data to DTOs.
        /// </summary>
        [Fact]
        public async Task GetUserProfileDataAsync_CorrectlyMapsPostData()
        {
            // Arrange
            var postId = Guid.NewGuid();
            var graphUserData = PrepareGraphUserDTOData(UserName);
            var userPosts = PrepareUserPostsData(UserName);

            this._usersServiceMock.Setup(x => x.GetGraphUserDataAsync(It.IsAny<string>())).ReturnsAsync(graphUserData);
            this._profilesDataServiceMock.Setup(x => x.GetUserPostsAsync(It.IsAny<string>())).ReturnsAsync(userPosts);
            this._profilesDataServiceMock.Setup(x => x.GetUserRatingsAsync(It.IsAny<string>())).ReturnsAsync([]);

            // Act
            var result = await this._profilesService.GetUserProfileDataAsync(UserName);

            // Assert
            Assert.NotNull(result);
            Assert.NotNull(result.UserPosts);
            var postDto = result.UserPosts[0];
            Assert.Equal(userPosts[0].PostId, postDto.PostId);
            Assert.Equal(userPosts[0].PostTitle, postDto.PostTitle);
            Assert.Equal(UserName, postDto.PostOwnerUserName);
        }
    }
}