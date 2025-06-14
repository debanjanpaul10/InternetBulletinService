// *********************************************************************************
//	<copyright file="UsersDataServiceTests.cs" company="Personal">
//		Copyright (c) 2025 Personal
//	</copyright>
// <summary>The Users Data Service Tests class.</summary>
// *********************************************************************************

namespace InternetBulletin.UnitTests.Data
{
    using InternetBulletin.Data;
    using InternetBulletin.Data.DataServices;
    using InternetBulletin.Data.Entities;
    using InternetBulletin.Shared.DTOs.Users;
    using Microsoft.EntityFrameworkCore;

    /// <summary>
    /// The Users Data Service Tests class.
    /// </summary>
    public class UsersDataServiceTests
    {
        /// <summary>
        /// The mock db context.
        /// </summary>
        private readonly SqlDbContext _mockDbContext;

        /// <summary>
        /// The users data service.
        /// </summary>
        private readonly UsersDataService _usersDataService;

        /// <summary>
        /// The database name for in-memory database.
        /// </summary>
        private readonly string _databaseName;

        /// <summary>
        /// Initializes a new instance of the <see cref="UsersDataServiceTests"/> class.
        /// </summary>
        public UsersDataServiceTests()
        {
            this._databaseName = Guid.NewGuid().ToString();
            var options = new DbContextOptionsBuilder<SqlDbContext>()
                .UseInMemoryDatabase(databaseName: this._databaseName)
                .Options;
            this._mockDbContext = new SqlDbContext(options);
            this._usersDataService = new UsersDataService(this._mockDbContext);
        }

        /// <summary>
        /// Tests that SaveUsersDataAsync saves new users successfully.
        /// </summary>
        [Fact]
        public async Task SaveUsersDataAsync_SavesNewUsers_Successfully()
        {
            // Arrange
            var usersData = PrepareListofGraphUserData();

            // Act
            var result = await this._usersDataService.SaveUsersDataAsync(usersData);
            var savedUsers = await this._mockDbContext.Users.ToListAsync();

            // Assert
            Assert.True(result);
            Assert.Equal(2, savedUsers.Count);
            Assert.All(savedUsers, user => Assert.True(user.IsActive));
            Assert.Contains(savedUsers, u => u.UserName == "testuser1");
            Assert.Contains(savedUsers, u => u.UserName == "testuser2");
        }

        /// <summary>
        /// Tests that SaveUsersDataAsync handles empty user list correctly.
        /// </summary>
        [Fact]
        public async Task SaveUsersDataAsync_EmptyUserList_ReturnsTrue()
        {
            // Arrange
            var usersData = new List<GraphUserDTO>();

            // Act
            var result = await this._usersDataService.SaveUsersDataAsync(usersData);
            var savedUsers = await this._mockDbContext.Users.ToListAsync();

            // Assert
            Assert.True(result);
            Assert.Empty(savedUsers);
        }

        /// <summary>
        /// Tests that SaveUsersDataAsync skips existing users.
        /// </summary>
        [Fact]
        public async Task SaveUsersDataAsync_SkipsExistingUsers_Successfully()
        {
            // Arrange
            var existingUserId = Guid.NewGuid().ToString();
            var existingUser = new User
            {
                Id = existingUserId,
                DisplayName = "Existing User",
                EmailAddress = "existing@example.com",
                UserName = "existinguser",
                IsActive = true,
                DateCreated = DateTime.UtcNow
            };
            await this._mockDbContext.Users.AddAsync(existingUser);
            await this._mockDbContext.SaveChangesAsync();

            var usersData = PrepareExistingGraphUserData(existingUserId);

            // Act
            var result = await this._usersDataService.SaveUsersDataAsync(usersData);
            var savedUsers = await this._mockDbContext.Users.ToListAsync();

            // Assert
            Assert.True(result);
            Assert.Equal(2, savedUsers.Count);
            var existingUserInDb = savedUsers.First(u => u.Id == existingUserId);
            Assert.Equal("Existing User", existingUserInDb.DisplayName); // Should not be updated
            Assert.Contains(savedUsers, u => u.UserName == "newuser"); // New user should be added
        }
    }
}