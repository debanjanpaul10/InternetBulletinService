// *********************************************************************************
//	<copyright file="AboutUsAppInfoDataDTO.cs" company="Personal">
//		Copyright (c) 2025 Personal
//	</copyright>
// <summary>The About us and application information data dto.</summary>
// *********************************************************************************

namespace InternetBulletin.Shared.DTOs.ApplicationInfo
{
	/// <summary>
	/// The About us and application information data dto.
	/// </summary>
	public class AboutUsAppInfoDataDTO
	{
		/// <summary>
		/// The application information data.
		/// </summary>
		public ApplicationInformation ApplicationInformationData { get; set; } = new();

		/// <summary>
		/// The application technologies data.
		/// </summary>
		public List<ApplicationTechnologies> ApplicationTechnologiesData { get; set; } = [];
	}
}
