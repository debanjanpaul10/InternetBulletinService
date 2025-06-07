import PostRatingDtoModel from "@models/PostRatingDto";
import UpdatePostDtoModel from "@models/UpdatePostDto";
import {
	AddNewPostApiAsync,
	DeletePostApiAsync,
	GetAllPostsApiAsync,
	GetPostApiAsync,
	PostRewriteStoryWithAiApiAsync,
	UpdatePostApiAsync,
	UpdateRatingApiAsync,
} from "@services/ibbs.apiservice";
import { ToggleErrorToaster } from "@store/Common/Actions";
import {
	ADD_NEW_POST_DATA,
	DELETE_POST_DATA,
	GET_ALL_POSTS_DATA,
	GET_EDIT_POST_DATA,
	GET_POST_DATA,
	IS_CREATE_POST_LOADING,
	POST_DATA_FAIL,
	REWRITE_STORY_AI,
	START_SPINNER,
	STOP_SPINNER,
	TOGGLE_EDIT_POST_DIALOG,
	TOGGLE_EDIT_POST_LOADER,
	TOGGLE_REWRITE_LOADER,
	TOGGLE_VOTING_LOADER,
	UPDATE_POST_RATING,
} from "@store/Posts/ActionTypes";

/**
 * Stores the toggle event for rewrite text loading.
 * @param {boolean} isLoading The is loading boolean flag.
 * @returns {Object} The action type and payload data.
 */
export const ToggleRewriteLoader = (isLoading) => {
	return {
		type: TOGGLE_REWRITE_LOADER,
		payload: isLoading
	};
};

/**
 * Saves the loader start status to redux store.
 * @returns {Object} The action type
 */
export const StartLoader = () => {
	return {
		type: START_SPINNER,
	};
};

/**
 * Saves the loader stop status to redux store.
 * @returns {Object} The action type
 */
export const StopLoader = () => {
	return {
		type: STOP_SPINNER,
	};
};

/**
 * Gets the post data from api.
 * @param {string} postId The user data.
 * @param {string} accessToken The access token.
 * @returns {Promise} The promise from the api response.
 */
export const GetPostAsync = (postId, accessToken) => {
	return async (dispatch) => {
		try {
			dispatch(StartLoader());
			const response = await GetPostApiAsync(postId, accessToken);
			if (response?.statusCode === 200) {
				dispatch(GetPostSuccess(response.data));
			}
		} catch (error) {
			console.error(error);
			dispatch(PostDataFailure(error.data));
			dispatch(
				ToggleErrorToaster({
					shouldShow: true,
					errorMessage: error,
				})
			);
		} finally {
			dispatch(StopLoader());
		}
	};
};

/**
 * Saves the post data to redux store.
 * @param {Object} data The api response.
 * @returns {Object} The action type and payload data.
 */
const GetPostSuccess = (data) => {
	return {
		type: GET_POST_DATA,
		payload: data,
	};
};

/**
 * Gets all posts data.
 * @returns {Promise} The promise from the api response.
 */
export const GetAllPostsAsync = (accessToken) => {
	return async (dispatch) => {
		try {
			dispatch(StartLoader());
			const response = await GetAllPostsApiAsync(accessToken);
			if (response?.statusCode === 200) {
				dispatch(GetAllPostsSuccess(response.data));
			}
		} catch (error) {
			console.error(error);
			dispatch(PostDataFailure(error.data ?? error.title));
			dispatch(
				ToggleErrorToaster({
					shouldShow: true,
					errorMessage: error.data ?? error.title,
				})
			);
		} finally {
			dispatch(StopLoader());
		}
	};
};

/**
 * Saves the all posts data to redux store
 * @param {Object} data The api response.
 * @returns {Object} The action type and payload data.
 */
const GetAllPostsSuccess = (data) => {
	return {
		type: GET_ALL_POSTS_DATA,
		payload: data,
	};
};

/**
 * Adds an new post data.
 * @param {Object} userData The user data object.
 * @param {string} accessToken The access token.
 * @returns {Promise} The promise from the api response.
 */
export const AddNewPostAsync = (postData, accessToken) => {
	return async (dispatch) => {
		try {
			dispatch(HandleCreatePostPageLoader(true));
			const response = await AddNewPostApiAsync(postData, accessToken);
			if (response?.statusCode === 200) {
				dispatch(AddNewPostSuccess(response?.data));
			}
		} catch (error) {
			console.error(error);
			dispatch(
				ToggleErrorToaster({
					shouldShow: true,
					errorMessage: error,
				})
			);
		} finally {
			dispatch(HandleCreatePostPageLoader(false));
		}
	};
};

/**
 * Saves the new post data to redux store.
 * @param {Object} data The api response.
 * @returns {Object} The action type and payload data.
 */
const AddNewPostSuccess = (data) => {
	return {
		type: ADD_NEW_POST_DATA,
		payload: data,
	};
};

/**
 * Saves the posts failure data to redux store.
 * @param {Object} data The api response.
 * @returns {Object} The action type and payload data.
 */
export const PostDataFailure = (data) => {
	return {
		type: POST_DATA_FAIL,
		payload: data,
	};
};

/**
 * Rewrites the story with AI.
 * @param {string} requestDto The story rewrite request dto.
 * @param {string} accessToken The access token.
 * @returns {Promise} The promise from the api response.
 */
export const RewriteStoryWithAiAsync = (requestDto, accessToken) => {
	return async (dispatch) => {
		try {
			dispatch(ToggleRewriteLoader(true));
			const response = await PostRewriteStoryWithAiApiAsync(requestDto, accessToken);
			if (response?.statusCode === 200) {
				dispatch(RewriteStoryWithAiSuccess(response?.data));
			}
		} catch (error) {
			console.error(error);
			dispatch(
				ToggleErrorToaster({
					shouldShow: true,
					errorMessage: error.title,
				})
			);
		} finally {
			dispatch(ToggleRewriteLoader(false));
		}
	};
};

/**
 * Saves the AI response data to redux store.
 * @param {Object} data The api response.
 * @returns {Object} The action type and payload data.
 */
export const RewriteStoryWithAiSuccess = (data) => {
	return {
		type: REWRITE_STORY_AI,
		payload: data,
	};
};

/**
 * Saves the create post page loader status to redux store.
 * @param {boolean} isLoading The loader boolean flag.
 * @returns {Object} The action type and payload data.
 */
export const HandleCreatePostPageLoader = (isLoading) => {
	return {
		type: IS_CREATE_POST_LOADING,
		payload: isLoading,
	};
};

/**
 * Deletes a post data asynchronously.
 * @param {string} postId The post id.
 * @param {string} accessToken The access token.
 * @returns {Promise} Gets the API response.
 */
export const DeletePostAsync = (postId, accessToken) => {
	return async (dispatch) => {
		try {
			dispatch(StartLoader());
			const response = await DeletePostApiAsync(postId, accessToken);
			if (response?.statusCode === 200) {
				dispatch(DeletePostAsyncSuccess(response?.data));
				dispatch(GetAllPostsAsync(accessToken));
			}
		} catch (error) {
			console.error(error);
			dispatch(PostDataFailure(error.data));
			dispatch(
				ToggleErrorToaster({
					shouldShow: true,
					errorMessage: error,
				})
			);
		} finally {
			dispatch(StopLoader());
		}
	};
};

/**
 * Saves the delete post api response to redux store.
 * @param {boolean} data The API response.
 * @returns {Object} The action type and payload data.
 */
const DeletePostAsyncSuccess = (data) => {
	return {
		type: DELETE_POST_DATA,
		payload: data,
	};
};

/**
 * Updates an existing post asynchronously.
 * @param {UpdatePostDtoModel} updatePostData The update post data model.
 * @param {string} accessToken The access token.
 * @returns {Promise} The promise of the api response.
 */
export const UpdatePostAsync = (updatePostData, accessToken) => {
	return async (dispatch) => {
		try {
			dispatch(ToggleEditPostSpinner(true));
			const response = await UpdatePostApiAsync(
				updatePostData,
				accessToken
			);
			if (response?.statusCode === 200) {
				dispatch(GetAllPostsAsync(accessToken));
			}
		} catch (error) {
			console.error(error);
			dispatch(PostDataFailure(error.data));
			dispatch(
				ToggleErrorToaster({
					shouldShow: true,
					errorMessage: error,
				})
			);
		} finally {
			dispatch(ToggleEditPostSpinner(false));
			dispatch(ToggleEditPostDialog(false));
		}
	};
};

/**
 * Updates the rating of post asynchronously.
 * @param {PostRatingDtoModel} postRatingDtoModel The post rating dto model.
 * @param {string} accessToken The function to get id token claims.
 *
 * @returns {Promise} The promise of the api response.
 */
export const UpdateRatingAsync = (postRatingDtoModel, accessToken) => {
	return async (dispatch) => {
		try {
			dispatch(ToggleRatingLoader(true));
			const response = await UpdateRatingApiAsync(
				postRatingDtoModel,
				accessToken
			);
			if (response?.statusCode === 200) {
				dispatch(UpdateRatingAsyncSuccess(response?.data));
				dispatch(GetAllPostsAsync(accessToken));
			}
		} catch (error) {
			console.error(error);
			dispatch(PostDataFailure(error.data));
			dispatch(
				ToggleErrorToaster({
					shouldShow: true,
					errorMessage: error,
				})
			);
		} finally {
			dispatch(ToggleRatingLoader(false));
		}
	};
};

/**
 * Saves the rating loader toggle event data to redux store.
 * @param {boolean} isLooading The boolean value for loading status.
 * @returns {Object} The action type and payload data.
 */
const ToggleRatingLoader = (isLoading) => {
	return {
		type: TOGGLE_VOTING_LOADER,
		payload: isLoading,
	};
};

/**
 * Saves the rating update data to redux store.
 * @param {Object} data The post rating update data dto.
 * @returns {Object} The action type and payload data.
 */
const UpdateRatingAsyncSuccess = (data) => {
	return {
		type: UPDATE_POST_RATING,
		payload: data,
	};
};

/**
 * Stores the edit post dialog toggle state to redux store.
 * @param {boolean} isOpen The is open boolean flag.
 * @returns {Object} The action type and payload data.
 */
export const ToggleEditPostDialog = (isOpen) => {
	return {
		type: TOGGLE_EDIT_POST_DIALOG,
		payload: isOpen,
	};
};

/**
 * Saves the edit post data to redux store.
 * @param {Object} data The edit post data.
 * @returns {Object} The action type and payload data.
 */
export const GetEditPostData = (data) => {
	return {
		type: GET_EDIT_POST_DATA,
		payload: data,
	};
};

/**
 * Stores the edit post spinner status to redux store.
 * @param {boolean} isLoading The is loading boolean flag.
 * @returns {Object} The action type and payload data.
 */
export const ToggleEditPostSpinner = (isLoading) => {
	return {
		type: TOGGLE_EDIT_POST_LOADER,
		payload: isLoading,
	};
};
