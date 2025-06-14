// *********************************************************************************
//	<copyright file="ProfilesDataServiceTests.cs" company="Personal">
//		Copyright (c) 2025 Personal
//	</copyright>
// <summary>The Profiles Data Service Tests class.</summary>
// *********************************************************************************

namespace InternetBulletin.UnitTests.Data
{
    using InternetBulletin.Data;
    using InternetBulletin.Data.DataServices;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Logging;
    using Moq;

    /// <summary>
    /// The Profiles Data Service Tests class.
    /// </summary>
    public class ProfilesDataServiceTests
    {
        /// <summary>
        /// The mock logger.
        /// </summary>
        private readonly Mock<ILogger<ProfilesDataService>> _mockLogger;

        /// <summary>
        /// The mock db context.
        /// </summary>
        private readonly SqlDbContext _mockDbContext;

        /// <summary>
        /// The profiles data service.
        /// </summary>
        private readonly ProfilesDataService _profilesDataService;

        /// <summary>
        /// The database name for in-memory database.
        /// </summary>
        private readonly string _databaseName;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProfilesDataServiceTests"/> class.
        /// </summary>
        public ProfilesDataServiceTests()
        {
            this._mockLogger = new Mock<ILogger<ProfilesDataService>>();
            this._databaseName = Guid.NewGuid().ToString();
            var options = new DbContextOptionsBuilder<SqlDbContext>()
                .UseInMemoryDatabase(databaseName: this._databaseName)
                .Options;
            this._mockDbContext = new SqlDbContext(options);
            this._profilesDataService = new ProfilesDataService(this._mockDbContext, this._mockLogger.Object);
        }

        /// <summary>
        /// Tests that GetUserPostsAsync returns active posts for a user.
        /// </summary>
        [Fact]
        public async Task GetUserPostsAsync_ReturnsActiveUserPosts()
        {
            // Arrange
            var userName = "TestUser";
            var posts = PrepareMockPostsData(userName);
            await this._mockDbContext.Posts.AddRangeAsync(posts);
            await this._mockDbContext.SaveChangesAsync();

            // Act
            var result = await this._profilesDataService.GetUserPostsAsync(userName);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count);
            Assert.All(result, p => Assert.Equal(userName, p.PostOwnerUserName));
            Assert.All(result, p => Assert.True(p.IsActive));
        }

        /// <summary>
        /// Tests that GetUserPostsAsync returns empty list when no posts found.
        /// </summary>
        [Fact]
        public async Task GetUserPostsAsync_NoPostsFound_ReturnsEmptyList()
        {
            // Arrange
            var userName = "TestUser";

            // Act
            var result = await this._profilesDataService.GetUserPostsAsync(userName);

            // Assert
            Assert.NotNull(result);
            Assert.Empty(result);
        }

        /// <summary>
        /// Tests that GetUserRatingsAsync returns active ratings for a user.
        /// </summary>
        [Fact]
        public async Task GetUserRatingsAsync_ReturnsActiveUserRatings()
        {
            // Arrange
            var userName = "TestUser";
            var posts = PrepareMockPostsData(userName);
            await this._mockDbContext.Posts.AddRangeAsync(posts);

            var ratings = PrepareMockPostsRatingsData(userName, posts);
            await this._mockDbContext.PostRatings.AddRangeAsync(ratings);
            await this._mockDbContext.SaveChangesAsync();

            // Act
            var result = await this._profilesDataService.GetUserRatingsAsync(userName);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(3, result.Count);
            Assert.Contains(result, r => r.PostName == "Post1");
            Assert.Contains(result, r => r.PostName == "Post2");
            Assert.All(result, r => Assert.Equal(1, r.CurrentRatingValue));
        }

        /// <summary>
        /// Tests that GetUserRatingsAsync returns empty list when no ratings found.
        /// </summary>
        [Fact]
        public async Task GetUserRatingsAsync_NoRatingsFound_ReturnsEmptyList()
        {
            // Arrange
            var userName = "TestUser";

            // Act
            var result = await this._profilesDataService.GetUserRatingsAsync(userName);

            // Assert
            Assert.NotNull(result);
            Assert.Empty(result);
        }
    }
}