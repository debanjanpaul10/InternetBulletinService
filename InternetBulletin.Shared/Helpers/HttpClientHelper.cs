// *********************************************************************************
//	<copyright file="HttpClientHelper.cs" company="Personal">
//		Copyright (c) 2025 Personal
//	</copyright>
// <summary>The Http Client Helper Services Class.</summary>
// *********************************************************************************

namespace InternetBulletin.Shared.Helpers
{
    using System.Diagnostics.CodeAnalysis;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Text;
    using InternetBulletin.Shared.Constants;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.Logging;
    using Newtonsoft.Json;
    using static InternetBulletin.Shared.Constants.ConfigurationConstants;

    /// <summary>
    /// Http client helper interface.
    /// </summary>
    public interface IHttpClientHelper
    {
        /// <summary>
        /// Gets the ibbs ai response asynchronous.
        /// </summary>
        /// <typeparam name="T">The input data.</typeparam>
        /// <param name="data">The data.</param>
        /// <param name="apiUrl">The api url.</param>
        /// <returns>The response from IBBS.AI</returns>
        Task<HttpResponseMessage> GetIbbsAiResponseAsync<T>(T data, string apiUrl);
    }

    /// <summary>
    /// Http client helper service.
    /// </summary>
    /// <param name="logger">The Logger</param>
    /// <param name="configuration">The configuration.</param>
    /// <param name="httpClientFactory">The http client factory.</param>
    /// <seealso cref="IHttpClientHelper"/>
    [ExcludeFromCodeCoverage]
    public class HttpClientHelper(ILogger<HttpClientHelper> logger, IConfiguration configuration, IHttpClientFactory httpClientFactory) : IHttpClientHelper
    {
        /// <summary>
        /// The logger.
        /// </summary>
        private readonly ILogger<HttpClientHelper> _logger = logger;

        /// <summary>
        /// The configuration.
        /// </summary>
        private readonly IConfiguration _configuration = configuration;

        /// <summary>
        /// The http client factory.
        /// </summary>
        private readonly IHttpClientFactory _httpClientFactory = httpClientFactory;

        /// <summary>
        /// Gets the ibbs ai response asynchronous.
        /// </summary>
        /// <typeparam name="T">The input data.</typeparam>
        /// <param name="data">The data.</param>
        /// <param name="apiUrl">The api url.</param>
        /// <returns>The response from IBBS.AI</returns>
        public async Task<HttpResponseMessage> GetIbbsAiResponseAsync<T>(T data, string apiUrl)
        {
            try
            {
                this._logger.LogInformation(string.Format(LoggingConstants.LogHelperMethodStart, nameof(GetIbbsAiResponseAsync), DateTime.UtcNow, data?.GetType().Name ?? NullStringConstant));

                var client = this._httpClientFactory.CreateClient(IbbsConstants.IbbsAIConstant);
                ArgumentException.ThrowIfNullOrEmpty(apiUrl);

                await PrepareHttpClientFactoryAsync(client, TokenHelper.GetIbbsAiTokenAsync(this._configuration, this._logger));

                var inputJson = JsonConvert.SerializeObject(data);
                var contentData = new StringContent(content: inputJson, encoding: Encoding.UTF8, ApplicationJsonConstant);

                var response = await client.PostAsync(apiUrl, contentData).ConfigureAwait(false);
                if (!response.IsSuccessStatusCode)
                {
                    return response.EnsureSuccessStatusCode();
                }

                return response;
            }
            catch (Exception ex)
            {
                this._logger.LogInformation(string.Format(LoggingConstants.LogHelperMethodFailed, nameof(GetIbbsAiResponseAsync), DateTime.UtcNow, ex.Message));
                throw;
            }
            finally
            {
                this._logger.LogInformation(string.Format(LoggingConstants.LogHelperMethodEnded, nameof(GetIbbsAiResponseAsync), DateTime.UtcNow, data?.GetType().Name ?? NullStringConstant));
            }
        }

        #region PRIVATE Methods

        /// <summary>
        /// Prepares http client factory async.
        /// </summary>
        /// <param name="client">The client.</param>
        /// <param name="tokenTask">The task to get the token.</param>
        private static async Task PrepareHttpClientFactoryAsync(HttpClient client, Task<string> tokenTask)
        {
            var token = await tokenTask.ConfigureAwait(false);
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(BearerConstant, token);
        }

        #endregion
    }

}
