// *********************************************************************************
//	<copyright file="IbbsConstants.cs" company="Personal">
//		Copyright (c) 2025 Personal
//	</copyright>
// <summary>Ibbs constants.</summary>
// *********************************************************************************

namespace InternetBulletin.Shared.Constants
{
    /// <summary>
    /// Ibbs constants.
    /// </summary>
    public static class IbbsConstants
    {
        /// <summary>
        /// The user name extension constant.
        /// </summary>
        public const string UserNameExtensionConstant = "extension_4bf572bc06514b24bd1897cd627697d0_UserName";

        /// <summary>
        /// The id constant.
        /// </summary>
        public const string IdConstant = "id";

        /// <summary>
        /// The display name constant.
        /// </summary>
        public const string DisplayNameConstant = "displayName";

        /// <summary>
        /// The identities constant.
        /// </summary>
        public const string IdentitiesConstant = "identities";

        /// <summary>
        /// The Email address constant
        /// </summary>
        public const string EmailAddressConstant = "mail";

        /// <summary>
        /// The ibbs a i constant.
        /// </summary>
        public const string IbbsAIConstant = "IBBS.AI";

        /// <summary>
        /// The Application Information Collection
        /// </summary>
        public const string ApplicationInformationCollectionConstant = "ApplicationInformation";

        /// <summary>
        /// The application technologies collection constant.
        /// </summary>
        public const string ApplicationTechnologiesCollectionConstant = "ApplicationTechnologies";
    }

    /// <summary>
    /// The enum for AI Usage details
    /// </summary>
    public enum AiUsages
    {
        /// <summary>
        /// The rewrite story enum.
        /// </summary>
        RewriteStory = 1,

        /// <summary>
        /// The moderate content enum.
        /// </summary>
        ModerateContent = 2,

        /// <summary>
        /// The genre tag enum.
        /// </summary>
        GenreTag = 3,
    }
}


