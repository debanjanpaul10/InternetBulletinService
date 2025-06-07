import { UrlConstants } from "../Helpers/config.constants";

const b2cPolicies = {
	names: {
		signUpSignIn: "B2C_1_susi",
		editProfile: "B2C_1_edit_profile",
		passwordReset: "B2C_1_pass_reset",
	},
	authorities: {
		signUpSignIn: {
			authority:
				"https://debanjanlab.b2clogin.com/debanjanlab.onmicrosoft.com/B2C_1_susi",
		},
		editProfile: {
			authority:
				"https://debanjanlab.b2clogin.com/debanjanlab.onmicrosoft.com/B2C_1_edit_profile",
		},
		passwordReset: {
			authority:
				"https://debanjanlab.b2clogin.com/debanjanlab.onmicrosoft.com/B2C_1_pass_reset",
		},
	},
	authorityDomain: "debanjanlab.b2clogin.com",
};

const msalConfig = {
	auth: {
		clientId: "12912858-246d-4231-a73a-00f2242379d3",
		authority: b2cPolicies.authorities.signUpSignIn.authority,
		redirectUri: UrlConstants.WebUrls.AzureWebUrl,
		knownAuthorities: [b2cPolicies.authorityDomain],
	},
};

const loginRequests = {
	scopes: [
		"https://debanjanlab.onmicrosoft.com/59977ff4-0747-41e8-bd9f-b2719b11865f/Posts.Read",
		"https://debanjanlab.onmicrosoft.com/59977ff4-0747-41e8-bd9f-b2719b11865f/Posts.Write",
	],
};

export { msalConfig, b2cPolicies, loginRequests };
