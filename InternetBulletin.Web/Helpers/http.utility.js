import axios from "axios";
import { UrlConstants } from "@helpers/config.constants";

/**
 * @class
 * The Http Utility class.
 */
class HttpUtility {
	/**
	 * The Web api base url endpoint.
	 */
	static WebApiEndpoint = UrlConstants.WebApiUrls.AzureWebApiUrl;

	/**
	 * The Get API data function.
	 * @param {string} apiUrl The api url.
	 * @param {string} accessToken The access token.
	 *
	 * @returns {Promise} The promise of the api response.
	 */
	static GetAsync = async (apiUrl, accessToken) => {
		try {
			const url = this.WebApiEndpoint + apiUrl;
			const response = await axios.get(url, {
				headers: {
					Authorization: `Bearer ${accessToken}`,
				},
			});

			if (response !== null || response.data !== "") {
				return response.data;
			}

			return "";
		}
		catch (error) {
			console.error(error);
			return Promise.reject(
				error.response ? error.response.data : error.message
			);
		}
	};

	/**
	 * The Post API data function.
	 * @param {string} apiUrl The api url.
	 * @param {Object} data The post data object.
	 * @param {string} accessToken The access token.
	 *
	 * @returns {Promise} The promise of the api response.
	 */
	static PostAsync = async (apiUrl, data, accessToken) => {
		try {
			const url = this.WebApiEndpoint + apiUrl;

			const response = await axios.post(url, data, {
				headers: {
					Authorization: `Bearer ${accessToken}`,
				},
			});

			if (response !== null || response.data !== "") {
				return response.data;
			}
		}
		catch (error) {
			console.error(error);
			return Promise.reject(
				error.response ? error.response.data : error.message
			);
		}
	};
}

export default HttpUtility;
