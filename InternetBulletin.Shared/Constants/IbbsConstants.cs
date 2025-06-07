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
        public const string UserNameExtensionConstant = "extension_3f9fbaaf3aad4b3cae589417139c7220_UserName";

        /// <summary>
        /// The email address constant.
        /// </summary>
        public const string EmailAddressConstant = "emailAddress";

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
        /// The story helper enum.
        /// </summary>
        /// <remarks>To be done: after adding more features</remarks>
        HelpStory = 2
    }
}


