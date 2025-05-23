// *********************************************************************************
//	<copyright file="TestsHelper.cs" company="Personal">
//		Copyright (c) 2025 Personal
//	</copyright>
// <summary>The Tests helper Class.</summary>
// *********************************************************************************

namespace InternetBulletin.UnitTests
{
    using InternetBulletin.Shared.DTOs.Posts;
    using InternetBulletin.Shared.DTOs.Users;
    using Microsoft.Graph.Groups.Item.MembersWithLicenseErrors;
    using Microsoft.Graph.Models;
    using Post = InternetBulletin.Data.Entities.Post;

    /// <summary>
    /// Tests helper.
    /// </summary>
    public static class TestsHelper
    {
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
        /// Prepares the graph API response.
        /// </summary>
        /// <param name="userName">The user name.</param>
        /// <returns>The graph API response.</returns>
        public static UserCollectionResponse PrepareGraphApiResponseForSingleUser(string userName)
        {
            return new UserCollectionResponse
            {
                Value =
                [
                    new()
                    {
                        DisplayName = "User 1",
                        Id = Guid.NewGuid().ToString(),
                        AdditionalData = new Dictionary<string, object>
                        {
                            { IbbsConstants.UserNameExtensionConstant, userName }
                        }
                    },
                ]
            };
        }

        /// <summary>
        /// Prepares the graph API response.
        /// </summary>
        /// <param name="userName">The user name.</param>
        /// <returns>The graph API response.</returns>
        public static UserCollectionResponse PrepareGraphApiResponse()
        {
            return new UserCollectionResponse
            {
                Value =
                [
                    new()
                    {
                        DisplayName = "User 1",
                        Id = Guid.NewGuid().ToString(),
                        AdditionalData = new Dictionary<string, object>
                        {
                            { IbbsConstants.UserNameExtensionConstant, "user1" }
                        }
                    },
                    new()
                    {
                        DisplayName = "User 2",
                        Id = Guid.NewGuid().ToString(),
                        AdditionalData = new Dictionary<string, object>
                        {
                            { IbbsConstants.UserNameExtensionConstant, "user2" }
                        }
                    },
                    new()
                    {
                        DisplayName = "User 3",
                        Id = Guid.NewGuid().ToString(),
                        AdditionalData = new Dictionary<string, object>
                        {
                            { IbbsConstants.UserNameExtensionConstant, "user3" }
                        }
                    }
                ]
            };
        }

        /// <summary>
        /// Prepares the empty graph API response.
        /// </summary>
        /// <returns>The empty graph API response.</returns>
        public static UserCollectionResponse PrepareEmptyGraphApiResponse()
        {
            return new UserCollectionResponse
            {
                Value = []
            };
        }
    }
}


