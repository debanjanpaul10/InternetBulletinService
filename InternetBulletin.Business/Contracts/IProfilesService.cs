// *********************************************************************************
//	<copyright file="IProfilesService.cs" company="Personal">
//		Copyright (c) 2025 Personal
//	</copyright>
// <summary>The Profiles Service Interface.</summary>
// *********************************************************************************

namespace InternetBulletin.Business.Contracts
{
    using InternetBulletin.Shared.DTOs;

    /// <summary>
    /// Profiles service interface.
    /// </summary>
    public interface IProfilesService
    {
        /// <summary>
        /// Gets user profile data async.
        /// </summary>
        /// <param name="userId">The user id.</param>
        Task<UserProfileDto> GetUserProfileDataAsync(int userId);
    }
}