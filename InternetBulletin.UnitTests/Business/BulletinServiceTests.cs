// *********************************************************************************
//	<copyright file="BulletinServiceTests.cs" company="Personal">
//		Copyright (c) 2025 Personal
//	</copyright>
// <summary>The Bulletin Service Tests</summary>
// *********************************************************************************

namespace InternetBulletin.UnitTests.Business
{
    using InternetBulletin.Shared.DTOs.ApplicationInfo;
    using MongoDB.Driver;

    /// <summary>
    /// The Bulletin Service Tests
    /// </summary>
    public class BulletinServiceTests
    {
        /// <summary>
        /// The mock of MongoClient.
        /// </summary>
        private readonly Mock<IMongoClient> _mockMongoClient;

        /// <summary>
        /// The mock of mongodb
        /// </summary>
        private readonly Mock<IMongoDatabase> _mockMongoDb;

        /// <summary>
        /// The mock of configuration.
        /// </summary>
        private readonly Mock<IConfiguration> _mockConfiguration;

        /// <summary>
        /// The mock of cache service.
        /// </summary>
        private readonly Mock<ICacheService> _mockCacheService;

        /// <summary>
        /// The bulletin service.
        /// </summary>
        private readonly BulletinService _bulletinService;

        /// <summary>
        /// Initializes a new instance of <see cref="BulletinServiceTests"/>
        /// </summary>
        public BulletinServiceTests()
        {
            this._mockMongoClient = new Mock<IMongoClient>();
            this._mockConfiguration = new Mock<IConfiguration>();
            this._mockMongoDb = new Mock<IMongoDatabase>();
            this._mockCacheService = new Mock<ICacheService>();

            this._mockConfiguration.Setup(x => x[ConfigurationConstants.MongoDatabaseNameConstant]).Returns("MongoDBName");
            this._mockMongoClient.Setup(x => x.GetDatabase(It.IsAny<string>(), It.IsAny<MongoDatabaseSettings>())).Returns(this._mockMongoDb.Object);
            this._bulletinService = new BulletinService(this._mockMongoClient.Object, this._mockConfiguration.Object, this._mockCacheService.Object);
        }

        /// <summary>
        /// Tests the get about us data and returns cached data when available.
        /// </summary>
        [Fact]
        public async Task GetAboutUsDataAsync_ReturnsCachedData_WhenAvailable()
        {
            // Arrange
            var expectedData = new AboutUsAppInfoDataDTO
            {
                ApplicationInformationData = new ApplicationInformation(),
                ApplicationTechnologiesData = []
            };

            this._mockCacheService.Setup(x => x.GetCachedData<AboutUsAppInfoDataDTO>(CacheKeys.AboutUsDataCacheKey)).Returns(expectedData);

            // Act
            var result = await this._bulletinService.GetAboutUsDataAsync();

            // Assert
            Assert.Equal(expectedData, result);
            this._mockCacheService.Verify(x => x.GetCachedData<AboutUsAppInfoDataDTO>(CacheKeys.AboutUsDataCacheKey), Times.Once);
            this._mockMongoDb.Verify(x => x.GetCollection<ApplicationInformation>(It.IsAny<string>(), It.IsAny<MongoCollectionSettings>()), Times.Never);
            this._mockMongoDb.Verify(x => x.GetCollection<ApplicationTechnologies>(It.IsAny<string>(), It.IsAny<MongoCollectionSettings>()), Times.Never);
        }

        /// <summary>
        /// Tests the get about us data and returns database data when cache has no data.
        /// </summary>
        [Fact]
        public async Task GetAboutUsDataAsync_ReturnsDataFromDatabaseAndSetsCache_WhenCacheIsEmpty()
        {
            // Arrange
            var expectedAppInfo = new ApplicationInformation { Id = Guid.NewGuid().ToString(), Title = "Test App" };
            var expectedAppTechs = new List<ApplicationTechnologies> { new() { Id = Guid.NewGuid().ToString(), Heading = "Tech1" } };

            var mockAppInfoCollection = new Mock<IMongoCollection<ApplicationInformation>>();
            var mockAppTechsCollection = new Mock<IMongoCollection<ApplicationTechnologies>>();

            var mockAppInfoCursor = new Mock<IAsyncCursor<ApplicationInformation>>();
            mockAppInfoCursor.Setup(x => x.Current).Returns([expectedAppInfo]);
            mockAppInfoCursor.Setup(x => x.MoveNext(It.IsAny<CancellationToken>())).Returns(true);
            mockAppInfoCursor.Setup(x => x.MoveNextAsync(It.IsAny<CancellationToken>())).ReturnsAsync(true);

            var mockAppTechsCursor = new Mock<IAsyncCursor<ApplicationTechnologies>>();
            mockAppTechsCursor.Setup(x => x.Current).Returns(expectedAppTechs);
            mockAppTechsCursor.Setup(x => x.MoveNext(It.IsAny<CancellationToken>())).Returns(false);
            mockAppTechsCursor.Setup(x => x.MoveNextAsync(It.IsAny<CancellationToken>())).ReturnsAsync(false);

            mockAppInfoCollection.Setup(c => c.FindAsync(It.IsAny<FilterDefinition<ApplicationInformation>>(), It.IsAny<FindOptions<ApplicationInformation, ApplicationInformation>>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(mockAppInfoCursor.Object);

            mockAppTechsCollection.Setup(c => c.FindAsync(It.IsAny<FilterDefinition<ApplicationTechnologies>>(), It.IsAny<FindOptions<ApplicationTechnologies, ApplicationTechnologies>>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(mockAppTechsCursor.Object);

            this._mockMongoDb.Setup(db => db.GetCollection<ApplicationInformation>(It.IsAny<string>(), It.IsAny<MongoCollectionSettings>())).Returns(mockAppInfoCollection.Object);
            this._mockMongoDb.Setup(db => db.GetCollection<ApplicationTechnologies>(It.IsAny<string>(), It.IsAny<MongoCollectionSettings>())).Returns(mockAppTechsCollection.Object);

            AboutUsAppInfoDataDTO cachedData = null!;
            this._mockCacheService.Setup(x => x.GetCachedData<AboutUsAppInfoDataDTO>(CacheKeys.AboutUsDataCacheKey)).Returns(() => cachedData);
            this._mockCacheService.Setup(x => x.SetCacheData(CacheKeys.AboutUsDataCacheKey, It.IsAny<AboutUsAppInfoDataDTO>(), It.IsAny<TimeSpan>()))
                .Callback<string, AboutUsAppInfoDataDTO, TimeSpan>((key, data, expiry) => cachedData = data);

            // Act
            var result = await this._bulletinService.GetAboutUsDataAsync();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(expectedAppInfo, result.ApplicationInformationData);
            this._mockCacheService.Verify(x => x.GetCachedData<AboutUsAppInfoDataDTO>(CacheKeys.AboutUsDataCacheKey), Times.Once);
            this._mockCacheService.Verify(x => x.SetCacheData(CacheKeys.AboutUsDataCacheKey, result, It.IsAny<TimeSpan>()), Times.Once);
            this._mockMongoDb.Verify(x => x.GetCollection<ApplicationInformation>(It.IsAny<string>(), It.IsAny<MongoCollectionSettings>()), Times.Once);
            this._mockMongoDb.Verify(x => x.GetCollection<ApplicationTechnologies>(It.IsAny<string>(), It.IsAny<MongoCollectionSettings>()), Times.Once);
        }

        /// <summary>
        /// Tests the get about us data and throws exception.
        /// </summary>
        [Fact]
        public async Task GetAboutUsDataAsync_ThrowsException_WhenDatabaseCollectionsAreNull()
        {
            // Arrange
            this._mockCacheService.Setup(x => x.GetCachedData<AboutUsAppInfoDataDTO>(CacheKeys.AboutUsDataCacheKey)).Returns((AboutUsAppInfoDataDTO)null!);
            this._mockMongoDb.Setup(db => db.GetCollection<ApplicationInformation>(It.IsAny<string>(), It.IsAny<MongoCollectionSettings>())).Returns((IMongoCollection<ApplicationInformation>)null!);
            this._mockMongoDb.Setup(db => db.GetCollection<ApplicationTechnologies>(It.IsAny<string>(), It.IsAny<MongoCollectionSettings>())).Returns((IMongoCollection<ApplicationTechnologies>)null!);

            // Act & Assert
            var exception = await Assert.ThrowsAsync<Exception>(() => this._bulletinService.GetAboutUsDataAsync());
            Assert.Equal(ExceptionConstants.SomethingWentWrongMessageConstant, exception.Message);
            this._mockCacheService.Verify(x => x.GetCachedData<AboutUsAppInfoDataDTO>(CacheKeys.AboutUsDataCacheKey), Times.Once);
            this._mockMongoDb.Verify(x => x.GetCollection<ApplicationInformation>(It.IsAny<string>(), It.IsAny<MongoCollectionSettings>()), Times.Once);
            this._mockMongoDb.Verify(x => x.GetCollection<ApplicationTechnologies>(It.IsAny<string>(), It.IsAny<MongoCollectionSettings>()), Times.Once);
        }
    }
}
