// *********************************************************************************
//	<copyright file="TestsHelper.cs" company="Personal">
//		Copyright (c) 2025 Personal
//	</copyright>
// <summary>The Tests helper Class.</summary>
// *********************************************************************************

namespace InternetBulletin.UnitTests.Helpers
{
    using InternetBulletin.Shared.DTOs.AI;
    using InternetBulletin.Shared.DTOs.Posts;
    using InternetBulletin.Shared.DTOs.Users;
    using InternetBulletin.Data.Entities;

    /// <summary>
    /// Tests helper.
    /// </summary>
    public static class TestsHelper
    {
        /// <summary>
        /// The user name.
        /// </summary>
        public readonly static string UserName = "user1234";

        /// <summary>
        /// Prepares graph user d t o data.
        /// </summary>
        /// <param name="userName">The user name.</param>
        public static GraphUserDTO PrepareGraphUserDTOData(string userName)
        {
            return new()
            {
                Id = "123",
                DisplayName = "Test User",
                EmailAddress = "test@example.com",
                UserName = userName
            };
        }

        /// <summary>
        /// Prepares user posts data.
        /// </summary>
        /// <param name="userName">The user name.</param>
        public static List<Post> PrepareUserPostsData(string userName)
        {
            return
            [
                new()
                {
                    PostId = Guid.NewGuid(),
                    PostTitle = "Test Post 1",
                    PostCreatedDate = DateTime.UtcNow,
                    PostOwnerUserName = userName,
                    Ratings = 5
                },
                new()
                {
                    PostId = Guid.NewGuid(),
                    PostTitle = "Test Post 2",
                    PostCreatedDate = DateTime.UtcNow.AddDays(-2),
                    PostOwnerUserName = userName,
                    Ratings = 1
                },
                new()
                {
                    PostId = Guid.NewGuid(),
                    PostTitle = "Test Post 3",
                    PostCreatedDate = DateTime.UtcNow.AddDays(-5),
                    PostOwnerUserName = userName,
                    Ratings = 0
                }
            ];
        }

        /// <summary>
        /// Prepares user post ratings data.
        /// </summary>
        public static List<UserPostRatingDTO> PrepareUserPostRatingsData()
        {
            return
            [
                new()
                {
                    PostName = "Sample Post 2",
                    CurrentRatingValue = new Random().Next(0, 1),
                    RatedOn = DateTime.UtcNow
                },
                new()
                {
                    PostName = "Sample Post 1",
                    CurrentRatingValue = new Random().Next(0, 1),
                    RatedOn = DateTime.UtcNow.AddDays(-1)
                },
                new()
                {
                    PostName = "Sample Post 3",
                    CurrentRatingValue = new Random().Next(0, 1),
                    RatedOn = DateTime.UtcNow.AddDays(-2)
                },

            ];
        }

        /// <summary>
        /// Prepares new post data d t o.
        /// </summary>
        /// <param name="postIdGuid">The post id guid.</param>
        /// <param name="ratingValue">The rating value.</param>
        public static Post PrepareNewPostDataDTO(string postIdGuid, int ratingValue)
        {
            return new()
            {
                PostId = Guid.Parse(postIdGuid),
                Ratings = ratingValue
            };
        }

        /// <summary>
        /// Prepares new post rating data d t o.
        /// </summary>
        /// <param name="postIdGuid">The post id guid.</param>
        /// <param name="ratingValue">The rating value.</param>
        public static PostRating PrepareNewPostRatingDataDTO(string postIdGuid, int ratingValue)
        {
            return new()
            {
                PostId = Guid.Parse(postIdGuid),
                RatingValue = ratingValue
            };
        }

        /// <summary>
        /// Prepares all post ratings for user data.
        /// </summary>
        /// <param name="userName">The user name.</param>
        public static List<PostRating> PrepareAllPostRatingsForUserData(string userName)
        {
            return [
                new()
                {
                    PostId = Guid.NewGuid(), UserName = userName
                },
                new()
                {
                    PostId = Guid.NewGuid(), UserName = userName
                },
                new()
                {
                    PostId = Guid.NewGuid(), UserName = userName
                },
            ];
        }

        /// <summary>
        /// Prepares new add post data d t o.
        /// </summary>
        public static AddPostDTO PrepareNewAddPostDataDTO()
        {
            return new() { PostTitle = "Test Post", PostContent = "Test Content" };
        }

        /// <summary>
        /// Prepares new update post data d t o.
        /// </summary>
        /// <param name="postIdGuid">The post id guid.</param>
        public static UpdatePostDTO PrepareNewUpdatePostDataDTO(string postIdGuid)
        {
            return new UpdatePostDTO { PostId = Guid.Parse(postIdGuid) };
        }

        /// <summary>
        /// Prepares post with ratings d t o.
        /// </summary>
        public static List<PostWithRatingsDTO> PreparePostWithRatingsDTO()
        {
            return
            [
                new()
                {
                    IsActive = true,
                    PostContent = "SAMPLE 1",
                    PostTitle = "TITLE 1",
                    PostId = Guid.NewGuid(),
                },
                new()
                {
                    IsActive = true,
                    PostContent = "SAMPLE 2",
                    PostTitle = "TITLE 2",
                    PostId = Guid.NewGuid(),
                },
                new()
                {
                    IsActive = true,
                    PostContent = "SAMPLE 3",
                    PostTitle = "TITLE 3",
                    PostId = Guid.NewGuid(),
                },
            ];
        }

        /// <summary>
        /// Prepares posts data for user.
        /// </summary>
        public static List<Post> PreparePostsDataForUser()
        {
            return
            [
                new()
                {
                    PostId = Guid.NewGuid(),
                    PostContent = "SAMPLE 1",
                    PostTitle = "TITLE 1"
                },
                new()
                {
                    PostId = Guid.NewGuid(),
                    PostContent = "SAMPLE 2",
                    PostTitle = "TITLE 2"
                },
                new()
                {
                    PostId = Guid.NewGuid(),
                    PostContent = "SAMPLE 3",
                    PostTitle = "TITLE 3"
                },
            ];
        }

        /// <summary>
        /// Prepares rewrite request dto for user.
        /// </summary>
        public static UserStoryRequestDTO PrepareMockRewriteRequestDTO()
        {
            return new UserStoryRequestDTO
            {
                Story = "Some Sample Story"
            };
        }

        /// <summary>
        /// Prepares the rewrite response DTO.
        /// </summary>
        public static RewriteResponseDTO PrepareRewriteResponseDTO()
        {
            return new RewriteResponseDTO()
            {
                CandidatesTokenCount = new Random().Next(1, 100),
                PromptTokenCount = new Random().Next(1, 100),
                RewrittenStory = "This is the rewritten story",
                TotalTokensConsumed = new Random().Next(101, 200)
            };
        }

        /// <summary>
        /// Prepares the mock tag response dto.
        /// </summary>
        /// <param name="expectedTag">The expected mock tag.</param>
        public static TagResponseDTO PrepareMockTagResponseDTO(string expectedTag)
        {
            return new TagResponseDTO()
            {
                UserStoryTag = expectedTag,
                TotalTokensConsumed = new Random().Next(1, 100),
                CandidatesTokenCount = new Random().Next(1, 100),
                PromptTokenCount = new Random().Next(101, 200)
            };
        }


        /// <summary>
        /// Prepares the moderation content response dto.
        /// </summary>
        /// <param name="expectedRating">The expected mock rating</param>
        public static ModerationContentResponseDTO PrepareModerationContentResponseDTO(string expectedRating)
        {
            return new ModerationContentResponseDTO
            {
                ContentRating = expectedRating,
                TotalTokensConsumed = 100,
                CandidatesTokenCount = 50,
                PromptTokenCount = 50
            };
        }

        /// <summary>
        /// Prepares a new list of graph user data.
        /// </summary>
        public static List<GraphUserDTO> PrepareListofGraphUserData()
        {
            return
            [
                new() {
                    Id = Guid.NewGuid().ToString(),
                    DisplayName = "Test User 1",
                    EmailAddress = "test1@example.com",
                    UserName = "testuser1"
                },
                new() {
                    Id = Guid.NewGuid().ToString(),
                    DisplayName = "Test User 2",
                    EmailAddress = "test2@example.com",
                    UserName = "testuser2"
                }
            ];
        }

        /// <summary>
        /// Prepares the existing user data.
        /// </summary>
        /// <param name="existingUserId">The existing user id.</param>
        public static List<GraphUserDTO> PrepareExistingGraphUserData(string existingUserId)
        {
            return
            [
                new() {
                    Id = existingUserId,
                    DisplayName = "Updated Name",
                    EmailAddress = "updated@example.com",
                    UserName = "updateduser"
                },
                new() {
                    Id = Guid.NewGuid().ToString(),
                    DisplayName = "New User",
                    EmailAddress = "new@example.com",
                    UserName = "newuser"
                }
            ];
        }

        /// <summary>
        /// Prepares the mock of posts data.
        /// </summary>
        /// <param name="userName">The user name.</param>
        public static List<Post> PrepareMockPostsData(string userName)
        {
            return
            [
                new() { PostId = Guid.NewGuid(), PostTitle = "Post1", PostOwnerUserName = userName, IsActive = true },
                new() { PostId = Guid.NewGuid(), PostTitle = "Post2", PostOwnerUserName = userName, IsActive = true },
                new() { PostId = Guid.NewGuid(), PostTitle = "Post3", PostOwnerUserName = "OtherUser", IsActive = true },
                new() { PostId = Guid.NewGuid(), PostTitle = "Post4", PostOwnerUserName = userName, IsActive = false }
            ];
        }

        /// <summary>
        /// Prepares the mock of posts ratings data.
        /// </summary>
        /// <param name="userName">The user name.</param>
        /// <param name="posts">The list of posts</param>
        public static List<PostRating> PrepareMockPostsRatingsData(string userName, List<Post> posts)
        {
            return
            [
                new() { PostId = posts[0].PostId, UserName = userName, RatingValue = 1, IsActive = true, RatedOn = DateTime.UtcNow },
                new() { PostId = posts[1].PostId, UserName = userName, RatingValue = 1, IsActive = true, RatedOn = DateTime.UtcNow },
                new() { PostId = posts[0].PostId, UserName = "OtherUser", RatingValue = 1, IsActive = true },
                new() { PostId = posts[1].PostId, UserName = userName, RatingValue = 0, IsActive = true },
                new() { PostId = posts[2].PostId, UserName = userName, RatingValue = 1, IsActive = true }
            ];
        }
    }
}


