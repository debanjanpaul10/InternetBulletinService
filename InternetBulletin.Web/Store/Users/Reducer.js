import {
	GET_USER_PROFILE_DATA,
	TOGGLE_USER_PROFILE_DATA_SPINNER,
} from "./ActionTypes";

const initialState = {
	userProfileData: {},
	isUserProfileDataLoading: true,
};

/**
 * The User Reducer.
 * @param {Object} state The state.
 * @param {Object} action The action.
 * @returns {Object} The updated state.
 */
const UserReducer = (state = initialState, action) => {
	switch (action.type) {
		case GET_USER_PROFILE_DATA: {
			return {
				...state,
				userProfileData: action.payload,
			};
		}
		case TOGGLE_USER_PROFILE_DATA_SPINNER: {
			return {
				...state,
				isUserProfileDataLoading: action.payload,
			};
		}
		default: {
			return state;
		}
	}
};

export { UserReducer };
