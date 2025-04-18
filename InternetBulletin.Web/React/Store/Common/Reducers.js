import {
	GET_CONFIGURATION_VALUE,
	TOGGLE_ERROR_TOASTER,
	TOGGLE_SUCCESS_TOASTER,
} from "@store/Common/ActionTypes";

const initialState = {
	configurationKey: "",
	successToaster: {},
	errorToaster: {},
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
		default: {
			return state;
		}
	}
};

export default CommonReducer;
