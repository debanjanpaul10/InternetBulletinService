// *********************************************************************************
//	<copyright file="AIServiceTests.cs" company="Personal">
//		Copyright (c) 2025 Personal
//	</copyright>
// <summary>The AI Service Tests class.</summary>
// *********************************************************************************

namespace InternetBulletin.UnitTests.Business
{
    using InternetBulletin.Shared.DTOs.AI;
    using InternetBulletin.UnitTests.Helpers;
    using Newtonsoft.Json;
    using static InternetBulletin.UnitTests.Helpers.TestsHelper;

    /// <summary>
    /// The AI Service Tests class.
    /// </summary>
    public class AIServiceTests
    {
        /// <summary>
        /// The mock of http client helper.
        /// </summary>
        private readonly Mock<IHttpClientHelper> _mockHttpClientHelper;

        /// <summary>
        /// The mock of ai data service.
        /// </summary>
        private readonly Mock<IAIDataService> _mockAiDataService;

        /// <summary>
        /// The ai service.
        /// </summary>
        private readonly AIService _aiService;

        /// <summary>
        /// Initializes a new instance of <see cref="AIServiceTests"/> class.
        /// </summary>
        public AIServiceTests()
        {
            this._mockAiDataService = new Mock<IAIDataService>();
            this._mockHttpClientHelper = new Mock<IHttpClientHelper>();

            this._aiService = new AIService(this._mockHttpClientHelper.Object, this._mockAiDataService.Object);
        }

        /// <summary>
        /// Tests that RewriteWithAIAsync returns the string representation of HttpContent
        /// when the HTTP call is successful and content is not null.
        /// Note: HttpContent.ToString() typically returns the type name, not the body.
        /// </summary>
        [Fact]
        public async Task RewriteWithAIAsync_SuccessfulResponseWithNonNullContent_ReturnsContentToString()
        {
            // Arrange
            var story = PrepareMockRewriteRequestDTO();
            var expectedRewrittenStoryRepresentation = PrepareRewriteResponseDTO();
            var mockHttpResponse = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(JsonConvert.SerializeObject(expectedRewrittenStoryRepresentation))
            };

            this._mockHttpClientHelper
                .Setup(x => x.GetIbbsAiResponseAsync(story, It.IsAny<string>()))
                .ReturnsAsync(mockHttpResponse);

            // Act
            var result = await this._aiService.RewriteWithAIAsync(UserName, story);

            // Assert
            Assert.Equal(expectedRewrittenStoryRepresentation.RewrittenStory, result);
        }

        /// <summary>
        /// Tests that RewriteWithAIAsync throws an exception when the HTTP call returns a non-success status code.
        /// </summary>
        [Fact]
        public async Task RewriteWithAIAsync_HttpClientReturnsNonSuccessStatusCode_ThrowsException()
        {
            // Arrange
            var story = PrepareMockRewriteRequestDTO();
            var mockHttpResponse = new HttpResponseMessage(HttpStatusCode.BadRequest)
            {
                Content = new StringContent("Error content")
            };

            this._mockHttpClientHelper.Setup(x => x.GetIbbsAiResponseAsync(It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync(mockHttpResponse);

            // Act & Assert
            var exception = await Assert.ThrowsAsync<NullReferenceException>(() => this._aiService.RewriteWithAIAsync(UserName, story));
            Assert.Equal(new NullReferenceException().Message, exception.Message);
        }

        /// <summary>
        /// Tests that RewriteWithAIAsync throws an exception when the HTTP client helper returns a null HttpResponseMessage.
        /// </summary>
        [Fact]
        public async Task RewriteWithAIAsync_HttpClientReturnsNullResponse_ThrowsException()
        {
            // Arrange
            var story = PrepareMockRewriteRequestDTO();
            this._mockHttpClientHelper.Setup(x => x.GetIbbsAiResponseAsync(It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync((HttpResponseMessage)null!);

            // Act & Assert
            var exception = await Assert.ThrowsAsync<NullReferenceException>(() => this._aiService.RewriteWithAIAsync(UserName, story));
            Assert.NotNull(exception.Message);
        }

        /// <summary>
        /// Tests that RewriteWithAIAsync propagates an exception thrown by the IHttpClientHelper.
        /// </summary>
        [Fact]
        public async Task RewriteWithAIAsync_HttpClientHelperThrowsException_PropagatesException()
        {
            // Arrange
            var story = PrepareMockRewriteRequestDTO();
            var expectedException = new HttpRequestException("Simulated network error");
            this._mockHttpClientHelper
                .Setup(x => x.GetIbbsAiResponseAsync(story, RouteConstants.RewriteTextApi_Route))
                .ThrowsAsync(expectedException);

            // Act & Assert
            var actualException = await Assert.ThrowsAsync<HttpRequestException>(() => this._aiService.RewriteWithAIAsync(UserName, story));
            Assert.Same(expectedException, actualException);
        }

        /// <summary>
        /// Tests that GenerateTagForStoryAsync returns the tag when the HTTP call is successful.
        /// </summary>
        [Fact]
        public async Task GenerateTagForStoryAsync_SuccessfulResponse_ReturnsTag()
        {
            // Arrange
            var story = PrepareMockRewriteRequestDTO();
            var expectedTag = "Adventure";
            var mockResponse = TestsHelper.PrepareMockTagResponseDTO(expectedTag);
            var mockHttpResponse = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(JsonConvert.SerializeObject(mockResponse))
            };

            this._mockHttpClientHelper
                .Setup(x => x.GetIbbsAiResponseAsync(story, RouteConstants.GenerateTagApi_Route))
                .ReturnsAsync(mockHttpResponse);

            this._mockAiDataService
                .Setup(x => x.SaveAiUsageDataAsync(It.IsAny<AiUsageDTO>()))
                .ReturnsAsync(true);

            // Act
            var result = await this._aiService.GenerateTagForStoryAsync(UserName, story);

            // Assert
            Assert.Equal(expectedTag, result);
        }

        /// <summary>
        /// Tests that GenerateTagForStoryAsync throws an exception when the HTTP call fails.
        /// </summary>
        [Fact]
        public async Task GenerateTagForStoryAsync_HttpClientReturnsNullResponse_ThrowsException()
        {
            // Arrange
            var story = PrepareMockRewriteRequestDTO();
            this._mockHttpClientHelper
                .Setup(x => x.GetIbbsAiResponseAsync(It.IsAny<string>(), It.IsAny<string>()))
                .ReturnsAsync((HttpResponseMessage)null!);

            // Act & Assert
            var exception = await Assert.ThrowsAsync<NullReferenceException>(() => this._aiService.GenerateTagForStoryAsync(UserName, story));
            Assert.NotNull(exception.Message);
        }

        /// <summary>
        /// Tests that ModerateContentDataAsync returns the content rating when the HTTP call is successful.
        /// </summary>
        [Fact]
        public async Task ModerateContentDataAsync_SuccessfulResponse_ReturnsContentRating()
        {
            // Arrange
            var story = PrepareMockRewriteRequestDTO();
            var expectedRating = "Safe";
            var mockResponse = PrepareModerationContentResponseDTO(expectedRating);
            var mockHttpResponse = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(JsonConvert.SerializeObject(mockResponse))
            };

            this._mockHttpClientHelper
                .Setup(x => x.GetIbbsAiResponseAsync(story, RouteConstants.ModerateContentApi_Route))
                .ReturnsAsync(mockHttpResponse);

            this._mockAiDataService
                .Setup(x => x.SaveAiUsageDataAsync(It.IsAny<AiUsageDTO>()))
                .ReturnsAsync(true);

            // Act
            var result = await this._aiService.ModerateContentDataAsync(UserName, story);

            // Assert
            Assert.Equal(expectedRating, result);
        }

        /// <summary>
        /// Tests that ModerateContentDataAsync throws an exception when the HTTP call fails.
        /// </summary>
        [Fact]
        public async Task ModerateContentDataAsync_HttpClientReturnsNullResponse_ThrowsException()
        {
            // Arrange
            var story = PrepareMockRewriteRequestDTO();
            this._mockHttpClientHelper
                .Setup(x => x.GetIbbsAiResponseAsync(It.IsAny<string>(), It.IsAny<string>()))
                .ReturnsAsync((HttpResponseMessage)null!);

            // Act & Assert
            var exception = await Assert.ThrowsAsync<NullReferenceException>(() => this._aiService.ModerateContentDataAsync(UserName, story));
            Assert.NotNull(exception.Message);
        }
    }
}