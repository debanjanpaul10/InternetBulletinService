import { GetUserProfilesDataApiAsync } from "@services/ibbs.apiservice";
import {
	GET_USER_PROFILE_DATA,
	TOGGLE_USER_PROFILE_DATA_SPINNER,
} from "./ActionTypes";

/**
 * Stores the profile data spinner toggle state to redux store.
 * @param {boolean} isLoading The is loading boolean flag.
 * @returns {Object} The action type and payload data.
 */
export const ToggleProfileDataSpinner = (isLoading) => {
	return {
		type: TOGGLE_USER_PROFILE_DATA_SPINNER,
		paylad: isLoading,
	};
};

/**
 * Gets the user profile data from api.
 * @param {string} accessToken The access token
 * @returns {Promise} The promise of the api response.
 */
export const GetUserProfileDataAsync = (accessToken) => {
	return async (dispatch) => {
		try {
			dispatch(ToggleProfileDataSpinner(true));
			const response = await GetUserProfilesDataApiAsync(accessToken);
			if (response?.statusCode === 200) {
				dispatch(GetUserProfileDataSuccess(response?.data));
			}
		} catch (error) {
			console.error(error);
		} finally {
			dispatch(ToggleProfileDataSpinner(false));
		}
	};
};

/**
 * Stores the user profile data to redux store.
 * @param {Object} data The user profile data.
 * @returns {Object} The action type and payload data.
 */
const GetUserProfileDataSuccess = (data) => {
	return {
		type: GET_USER_PROFILE_DATA,
		payload: data,
	};
};
