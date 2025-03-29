import {
	GET_ALL_POSTS_DATA,
	GET_POST_DATA,
	START_SPINNER,
	STOP_SPINNER,
	REWRITE_STORY_AI,
	IS_CREATE_POST_LOADING,
} from "@store/Posts/ActionTypes";

const initialState = {
	postData: {},
	allPostsData: [],
	isPostsDataLoading: false,
	aiRewrittenStory: "",
	isCreatePostLoading: false,
};

/**
 * The Posts Reducer.
 * @param {Object} state The state.
 * @param {Object} action The action.
 * @returns {Object} The updated state.
 */
const PostsReducer = (state = initialState, action) => {
	switch (action.type) {
		case GET_POST_DATA: {
			return {
				...state,
				postData: action.payload,
			};
		}
		case GET_ALL_POSTS_DATA: {
			return {
				...state,
				allPostsData: [...action.payload],
			};
		}
		case START_SPINNER: {
			return {
				...state,
				isPostsDataLoading: true,
			};
		}
		case STOP_SPINNER: {
			return {
				...state,
				isPostsDataLoading: false,
			};
		}
		case REWRITE_STORY_AI: {
			return {
				...state,
				aiRewrittenStory: action.payload,
			};
		}
		case IS_CREATE_POST_LOADING: {
			return {
				...state,
				isCreatePostLoading: action.payload,
			};
		}
		default: {
			return state;
		}
	}
};

export default PostsReducer;
