import {
	GET_APPLICATION_INFORMATION,
	GET_CONFIGURATION_VALUE,
	TOGGLE_ABOUT_US_SPINNER,
	TOGGLE_ERROR_TOASTER,
	TOGGLE_SIDE_BAR_STATUS,
	TOGGLE_SUCCESS_TOASTER,
} from "@store/Common/ActionTypes";

const initialState = {
	configurationKey: "",
	successToaster: {},
	errorToaster: {},
	isSideBarOpen: false,
	applicationInformation: {},
	isAboutUsLoading: true,
};

/**
 * The common reducer.
 * @param {Object} state The state.
 * @param {Object} action The action data.
 * @returns {Object} The updated redux store object.
 */
const CommonReducer = (state = initialState, action) => {
	switch (action.type) {
		case GET_CONFIGURATION_VALUE: {
			return {
				...state,
				configurationKey: action.payload,
			};
		}
		case TOGGLE_SUCCESS_TOASTER: {
			return {
				...state,
				successToaster: action.payload,
			};
		}
		case TOGGLE_ERROR_TOASTER: {
			return {
				...state,
				errorToaster: action.payload,
			};
		}
		case TOGGLE_SIDE_BAR_STATUS: {
			return {
				...state,
				isSideBarOpen: action.payload,
			};
		}
		case GET_APPLICATION_INFORMATION: {
			return {
				...state,
				applicationInformation: action.payload,
			}
		}
		case TOGGLE_ABOUT_US_SPINNER: {
			return {
				...state,
				isAboutUsLoading: action.payload,
			}
		}
		default: {
			return state;
		}
	}
};

export { CommonReducer };
