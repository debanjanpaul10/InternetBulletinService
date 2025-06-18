import {
    GET_ALL_POSTS_DATA,
    GET_POST_DATA,
    START_SPINNER,
    STOP_SPINNER,
    REWRITE_STORY_AI,
    IS_CREATE_POST_LOADING,
    DELETE_POST_DATA,
    UPDATE_POST_RATING,
    TOGGLE_VOTING_LOADER,
    TOGGLE_EDIT_POST_DIALOG,
    GET_EDIT_POST_DATA,
    TOGGLE_EDIT_POST_LOADER,
    TOGGLE_REWRITE_LOADER,
    HANDLE_POST_AI_MODERATION,
} from "@store/Posts/ActionTypes";

const initialState = {
    postData: {},
    allPostsData: [],
    isPostsDataLoading: false,
    aiRewrittenStory: "",
    isCreatePostLoading: false,
    isPostDataDeleted: false,
    updatedRatingData: {},
    isVotingLoaderOn: false,
    isEditModalOpen: false,
    editPostData: {},
    isEditPostDataLoading: false,
    isRewriteLoading: false,
    aiModerationData: {},
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
        case DELETE_POST_DATA: {
            return {
                ...state,
                isPostDataDeleted: action.payload,
            };
        }
        case UPDATE_POST_RATING: {
            return {
                ...state,
                updatedRatingData: action.payload,
            };
        }
        case TOGGLE_VOTING_LOADER: {
            return {
                ...state,
                isVotingLoaderOn: action.payload,
            };
        }
        case TOGGLE_EDIT_POST_DIALOG: {
            return {
                ...state,
                isEditModalOpen: action.payload,
            };
        }
        case GET_EDIT_POST_DATA: {
            return {
                ...state,
                editPostData: action.payload,
            };
        }
        case TOGGLE_EDIT_POST_LOADER: {
            return {
                ...state,
                isEditPostDataLoading: action.payload,
            };
        }
        case TOGGLE_REWRITE_LOADER: {
            return {
                ...state,
                isRewriteLoading: action.payload,
            };
        }
        case HANDLE_POST_AI_MODERATION: {
            return {
                ...state,
                aiModerationData: action.payload,
            };
        }
        default: {
            return state;
        }
    }
};

export { PostsReducer };
