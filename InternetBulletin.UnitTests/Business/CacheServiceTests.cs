// *********************************************************************************
//	<copyright file="CacheServiceTests.cs" company="Personal">
//		Copyright (c) 2025 Personal
//	</copyright>
// <summary>The Cache Service Tests Class.</summary>
// *********************************************************************************

namespace InternetBulletin.UnitTests.Business
{
    /// <summary>
    /// Cache service tests.
    /// </summary>
    public class CacheServiceTests
    {
        /// <summary>
        /// The _memory cache mock.
        /// </summary>
        private readonly Mock<IMemoryCache> _memoryCacheMock;

        /// <summary>
        /// The _mock logger.
        /// </summary>
        private readonly Mock<ILogger<CacheService>> _mockLogger;

        /// <summary>
        /// The _cache service.
        /// </summary>
        private readonly CacheService _cacheService;

        /// <summary>
        /// Initializes a new instance of the <see cref="CacheServiceTests"/> class.
        /// </summary>
        public CacheServiceTests()
        {
            this._memoryCacheMock = new Mock<IMemoryCache>();
            this._mockLogger = new Mock<ILogger<CacheService>>();
            this._cacheService = new CacheService(this._memoryCacheMock.Object, this._mockLogger.Object);
        }

        #region GetCachedData Tests

        /// <summary>
        /// Gets cached data_ null key_ throws argument null exception.
        /// </summary>
        [Fact]
        public void GetCachedData_NullKey_ThrowsArgumentNullException()
        {
            // Arrange
            string? key = null;

            // Act & Assert
            var ex = Assert.Throws<ArgumentNullException>(() => this._cacheService.GetCachedData<object>(key!));
            Assert.Equal(key, ex.ParamName);
        }

        /// <summary>
        /// Gets cached data_ empty key_ throws argument null exception.
        /// </summary>
        [Fact]
        public void GetCachedData_EmptyKey_ThrowsArgumentNullException()
        {
            // Arrange
            var key = string.Empty;

            // Act & Assert
            var ex = Assert.Throws<ArgumentNullException>(() => this._cacheService.GetCachedData<object>(key));
            Assert.Equal(key, ex.ParamName);
        }

        /// <summary>
        /// Gets cached data_ key exists_ returns cached value.
        /// </summary>
        [Fact]
        public void GetCachedData_KeyExists_ReturnsCachedValue()
        {
            // Arrange
            var key = "testKey";
            var expectedValue = "testValue";
            object? value = expectedValue; // Assign to object? to match TryGetValue signature
            this._memoryCacheMock.Setup(m => m.TryGetValue(key, out value)).Returns(true);

            // Act
            var result = this._cacheService.GetCachedData<string>(key);

            // Assert
            Assert.Equal(expectedValue, result);
        }

        /// <summary>
        /// Gets cached data_ key does not exist_ returns default.
        /// </summary>
        [Fact]
        public void GetCachedData_KeyDoesNotExist_ReturnsDefault()
        {
            // Arrange
            var key = "testKey";
            object? value = null; // Assign to object? to match TryGetValue signature
            this._memoryCacheMock.Setup(m => m.TryGetValue(key, out value)).Returns(false);

            // Act
            var result = this._cacheService.GetCachedData<string>(key);

            // Assert
            Assert.Null(result); // Default for string is null

        }

        /// <summary>
        /// Gets cached data_ memory cache throws exception_ rethrows exception.
        /// </summary>
        [Fact]
        public void GetCachedData_MemoryCacheThrowsException_RethrowsException()
        {
            // Arrange
            var key = "testKey";
            var exception = new InvalidOperationException("Cache error");
            this._memoryCacheMock.Setup(m => m.TryGetValue(key, out It.Ref<object?>.IsAny)).Throws(exception);

            // Act & Assert
            var ex = Assert.Throws<InvalidOperationException>(() => this._cacheService.GetCachedData<object>(key));
            Assert.Equal(exception.Message, ex.Message);
        }

        #endregion

        #region RemoveCachedData Tests

        /// <summary>
        /// Removes cached data_ null key_ throws argument null exception.
        /// </summary>
        [Fact]
        public void RemoveCachedData_NullKey_ThrowsArgumentNullException()
        {
            // Arrange
            string? key = null;

            // Act & Assert
            var ex = Assert.Throws<ArgumentNullException>(() => this._cacheService.RemoveCachedData(key!));
            Assert.Equal("key", ex.ParamName);
        }

        /// <summary>
        /// Removes cached data_ empty key_ throws argument null exception.
        /// </summary>
        [Fact]
        public void RemoveCachedData_EmptyKey_ThrowsArgumentNullException()
        {
            // Arrange
            var key = string.Empty;

            // Act & Assert
            var ex = Assert.Throws<ArgumentNullException>(() => this._cacheService.RemoveCachedData(key));
            Assert.Equal("key", ex.ParamName);
        }

        /// <summary>
        /// Removes cached data_ key exists_ removes and returns true.
        /// </summary>
        [Fact]
        public void RemoveCachedData_KeyExists_RemovesAndReturnsTrue()
        {
            // Arrange
            var key = "testKey";
            object? value = "testValue";
            this._memoryCacheMock.Setup(m => m.TryGetValue(key, out value)).Returns(true);

            // Act
            var result = this._cacheService.RemoveCachedData(key);

            // Assert
            Assert.True(result);
            this._memoryCacheMock.Verify(m => m.Remove(key), Times.Once);
        }

        /// <summary>
        /// Removes cached data_ key does not exist_ returns false.
        /// </summary>
        [Fact]
        public void RemoveCachedData_KeyDoesNotExist_ReturnsFalse()
        {
            // Arrange
            var key = "testKey";
            object? value = null;
            this._memoryCacheMock.Setup(m => m.TryGetValue(key, out value)).Returns(false);

            // Act
            var result = this._cacheService.RemoveCachedData(key);

            // Assert
            Assert.False(result);
            this._memoryCacheMock.Verify(m => m.Remove(key), Times.Never);
        }

        #endregion

        #region SetCacheData Tests

        /// <summary>
        /// Sets cache data_ null key_ throws argument null exception.
        /// </summary>
        [Fact]
        public void SetCacheData_NullKey_ThrowsArgumentNullException()
        {
            // Arrange
            string? key = null;
            var value = "testValue";
            var expiration = TimeSpan.FromMinutes(5);

            // Act & Assert
            var ex = Assert.Throws<ArgumentNullException>(() => this._cacheService.SetCacheData(key!, value, expiration));
            Assert.Equal("key", ex.ParamName);
        }

        /// <summary>
        /// Sets cache data_ empty key_ throws argument null exception.
        /// </summary>
        [Fact]
        public void SetCacheData_EmptyKey_ThrowsArgumentNullException()
        {
            // Arrange
            var key = string.Empty;
            var value = "testValue";
            var expiration = TimeSpan.FromMinutes(5);

            // Act & Assert
            var ex = Assert.Throws<ArgumentNullException>(() => this._cacheService.SetCacheData(key, value, expiration));
            Assert.Equal("key", ex.ParamName);
        }

        /// <summary>
        /// Sets cache data_ valid input_ sets cache and returns true.
        /// </summary>
        [Fact]
        public void SetCacheData_ValidInput_SetsCacheAndReturnsTrue()
        {
            // Arrange
            var key = "testKey";
            var value = "testValue";
            var expiration = TimeSpan.FromMinutes(5);
            var cacheEntry = Mock.Of<ICacheEntry>();
            this._memoryCacheMock.Setup(m => m.CreateEntry(key)).Returns(cacheEntry);

            // Act
            var result = this._cacheService.SetCacheData(key, value, expiration);

            // Assert
            Assert.True(result);
            this._memoryCacheMock.Verify(m => m.CreateEntry(key), Times.Once);
        }

        #endregion

    }
}