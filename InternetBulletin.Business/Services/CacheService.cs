// *********************************************************************************
//	<copyright file="CacheService.cs" company="Personal">
//		Copyright (c) 2025 Personal
//	</copyright>
// <summary>Cache service class.</summary>
// *********************************************************************************

namespace InternetBulletin.Business.Services
{
    using InternetBulletin.Business.Contracts;
    using InternetBulletin.Shared.Constants;
    using Microsoft.Extensions.Caching.Memory;
    using Microsoft.Extensions.Logging;

    /// <summary>
    /// Cache service class.
    /// </summary>
    /// <param name="memoryCache">The memory cache service.</param>
    /// <param name="logger">The logger.</param>
    /// <seealso cref="ICacheService"/>
    public class CacheService(IMemoryCache memoryCache, ILogger<CacheService> logger) : ICacheService
    {
        /// <summary>
        /// The memory cache.
        /// </summary>
        private readonly IMemoryCache _memoryCache = memoryCache;

        /// <summary>
        /// The logger.
        /// </summary>
        private readonly ILogger<CacheService> _logger = logger;

        /// <summary>
        /// Gets cached data.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <typeparam name="T"></typeparam>
        /// <exception cref="ArgumentNullException">ArgumentNullException error.</exception>
        public T? GetCachedData<T>(string key)
        {
            if (string.IsNullOrEmpty(key))
            {
                var ex = new ArgumentNullException(key);
                this._logger.LogError(ex, LoggingConstants.LogHelperMethodFailed, nameof(GetCachedData), DateTime.UtcNow, ex.Message);
                throw ex;
            }
            try
            {
                this._logger.LogInformation(LoggingConstants.LogHelperMethodStart, nameof(GetCachedData), DateTime.UtcNow, key);
                if (this._memoryCache.TryGetValue(key, out T? value))
                {
                    this._logger.LogInformation(LoggingConstants.CacheKeyFoundMessageConstant, key);
                    return value;
                }

                this._logger.LogInformation(LoggingConstants.CacheKeyNotFoundMessageConstant, key);
                return value;
            }
            catch (Exception ex)
            {
                this._logger.LogError(ex, LoggingConstants.LogHelperMethodFailed, nameof(GetCachedData), DateTime.UtcNow, ex.Message);
                throw;
            }
            finally
            {
                this._logger.LogInformation(LoggingConstants.LogHelperMethodEnded, nameof(GetCachedData), DateTime.UtcNow, key);
            }
        }

        /// <summary>
        /// Removes cached data.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <exception cref="ArgumentNullException">ArgumentNullException error.</exception>
        public bool RemoveCachedData(string key)
        {
            if (string.IsNullOrEmpty(key))
            {
                var ex = new ArgumentNullException(nameof(key), ExceptionConstants.KeyNameIsNullMessageConstant);
                this._logger.LogError(ex, LoggingConstants.LogHelperMethodFailed, nameof(RemoveCachedData), DateTime.UtcNow, ex.Message);
                throw ex;
            }

            try
            {
                this._logger.LogInformation(LoggingConstants.LogHelperMethodStart, nameof(RemoveCachedData), DateTime.UtcNow, key);
                if (this._memoryCache.TryGetValue(key, out _)) // Check if key exists
                {
                    this._memoryCache.Remove(key);
                    this._logger.LogInformation("Successfully removed cache for key: {CacheKey}", key);
                    return true;
                }

                this._logger.LogInformation(LoggingConstants.CacheKeyNotFoundMessageConstant, key);
                return false;
            }
            catch (Exception ex)
            {
                this._logger.LogError(ex, LoggingConstants.LogHelperMethodFailed, nameof(RemoveCachedData), DateTime.UtcNow, ex.Message);
                throw;
            }
            finally
            {
                this._logger.LogInformation(LoggingConstants.LogHelperMethodEnded, nameof(RemoveCachedData), DateTime.UtcNow, key);
            }
        }

        /// <summary>
        /// Sets cache data.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="value">The value.</param>
        /// <param name="expirationTime">The expiration time.</param>
        /// <typeparam name="T"></typeparam>
        /// <exception cref="ArgumentNullException">ArgumentNullException error.</exception>
        public bool SetCacheData<T>(string key, T value, TimeSpan expirationTime)
        {
            if (string.IsNullOrEmpty(key))
            {
                var ex = new ArgumentNullException(nameof(key), ExceptionConstants.KeyNameIsNullMessageConstant);
                this._logger.LogError(ex, LoggingConstants.LogHelperMethodFailed, nameof(SetCacheData), DateTime.UtcNow, ex.Message);
                throw ex;
            }

            try
            {
                this._logger.LogInformation(LoggingConstants.LogHelperMethodStart, nameof(SetCacheData), DateTime.UtcNow, key);
                this._memoryCache.Set(key, value, expirationTime);
                return true;
            }
            catch (Exception ex)
            {
                this._logger.LogError(ex, LoggingConstants.LogHelperMethodFailed, nameof(SetCacheData), DateTime.UtcNow, ex.Message);
                throw;
            }
            finally
            {
                this._logger.LogInformation(LoggingConstants.LogHelperMethodEnded, nameof(SetCacheData), DateTime.UtcNow, key);
            }
        }
    }
}
