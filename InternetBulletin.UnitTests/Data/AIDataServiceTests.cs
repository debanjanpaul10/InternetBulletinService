// *********************************************************************************
//	<copyright file="AIDataServiceTests.cs" company="Personal">
//		Copyright (c) 2025 Personal
//	</copyright>
// <summary>The AI Data Service Tests class.</summary>
// *********************************************************************************

namespace InternetBulletin.UnitTests.Data
{
    using InternetBulletin.Data;
    using InternetBulletin.Data.DataServices;
    using InternetBulletin.Shared.DTOs.AI;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Logging;
    using Moq;

    /// <summary>
    /// The AI Data Service Tests class.
    /// </summary>
    public class AIDataServiceTests
    {
        /// <summary>
        /// The mock logger.
        /// </summary>
        private readonly Mock<ILogger<AIDataService>> _mockLogger;

        /// <summary>
        /// The mock db context.
        /// </summary>
        private readonly SqlDbContext _mockDbContext;

        /// <summary>
        /// The AI data service.
        /// </summary>
        private readonly AIDataService _aiDataService;

        /// <summary>
        /// The database name for in-memory database.
        /// </summary>
        private readonly string _databaseName;

        /// <summary>
        /// Initializes a new instance of the <see cref="AIDataServiceTests"/> class.
        /// </summary>
        public AIDataServiceTests()
        {
            this._mockLogger = new Mock<ILogger<AIDataService>>();
            this._databaseName = Guid.NewGuid().ToString();
            var options = new DbContextOptionsBuilder<SqlDbContext>()
                .UseInMemoryDatabase(databaseName: this._databaseName)
                .Options;
            this._mockDbContext = new SqlDbContext(options);
            this._aiDataService = new AIDataService(this._mockDbContext, this._mockLogger.Object);
        }

        /// <summary>
        /// Tests that SaveAiUsageDataAsync successfully saves data and returns true.
        /// </summary>
        [Fact]
        public async Task SaveAiUsageDataAsync_ValidData_SavesSuccessfully()
        {
            // Arrange
            var aiUsageData = new AiUsageDTO
            {
                TotalTokensConsumed = 100,
                CandidatesTokenCount = 50,
                PromptTokenCount = 50,
                Usage = "TestUsage",
                UsageTime = DateTime.UtcNow,
                UserName = "TestUser"
            };

            // Act
            var result = await this._aiDataService.SaveAiUsageDataAsync(aiUsageData);

            // Assert
            Assert.True(result);
            var savedData = await this._mockDbContext.AiUsages.FirstOrDefaultAsync();
            Assert.NotNull(savedData);
            Assert.Equal(aiUsageData.TotalTokensConsumed, savedData.TotalTokensConsumed);
            Assert.Equal(aiUsageData.CandidatesTokenCount, savedData.CandidatesTokenCount);
            Assert.Equal(aiUsageData.PromptTokenCount, savedData.PromptTokenCount);
            Assert.Equal(aiUsageData.Usage, savedData.Usage);
            Assert.Equal(aiUsageData.UserName, savedData.UserName);
        }

        /// <summary>
        /// Tests that SaveAiUsageDataAsync throws exception when database operation fails.
        /// </summary>
        [Fact]
        public async Task SaveAiUsageDataAsync_DatabaseError_ThrowsException()
        {
            // Arrange
            var aiUsageData = new AiUsageDTO
            {
                TotalTokensConsumed = 100,
                CandidatesTokenCount = 50,
                PromptTokenCount = 50,
                Usage = "TestUsage",
                UsageTime = DateTime.UtcNow,
                UserName = "TestUser"
            };

            // Simulate database error by disposing the context
            await this._mockDbContext.DisposeAsync();

            // Act & Assert
            await Assert.ThrowsAsync<ObjectDisposedException>(
                () => this._aiDataService.SaveAiUsageDataAsync(aiUsageData));
        }

    }
}