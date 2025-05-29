import { useState, useEffect, useMemo } from "react";
import { useDispatch, useSelector } from "react-redux";
import {
	Dialog,
	DialogSurface,
	Button,
	Card,
	CardHeader,
	Label,
	CardPreview,
	Spinner,
	tokens,
} from "@fluentui/react-components";
import { useMsal } from "@azure/msal-react";
import ReactQuill from "react-quill-new";

import AiButton from "@assets/Images/ai-icon.svg";
import {
	RewriteStoryWithAiAsync,
	ToggleEditPostDialog,
	UpdatePostAsync,
} from "@store/Posts/Actions";
import { useStyles } from "@components/Posts/Components/EditPost/styles";
import { CreatePostPageConstants } from "@helpers/ibbs.constants";
import UpdatePostDtoModel from "@models/UpdatePostDto";
import { loginRequests } from "@services/auth.config";
import RewriteRequestDtoModel from "@models/RewriteRequestDto";

/**
 * Renders a dialog interface for editing a post, including form fields, validation, and AI-assisted rewriting.
 *
 * Integrates authentication, Redux state management, and rich text editing to allow users to update post content within a modal dialog.
 *
 * @returns {JSX.Element} The edit post dialog component.
 */
function EditPostComponent() {
	const dispatch = useDispatch();
	const styles = useStyles();
	const { instance, accounts } = useMsal();

	const IsEditPostDialogOpen = useSelector(
		(state) => state.PostsReducer.isEditModalOpen
	);
	const EditPostData = useSelector(
		(state) => state.PostsReducer.editPostData
	);
	const IsEditPostDataLoading = useSelector(
		(state) => state.PostsReducer.isEditPostDataLoading
	);

	const [isDialogOpen, setIsDialogOpen] = useState(false);
	const [isEditPostLoading, setIsEditPostLoading] = useState(false);
	const [postData, setPostData] = useState({
		postTitle: "",
		postContent: "",
		postId: "",
	});
	const [errors, setErrors] = useState({
		postTitle: "",
		postContent: "",
	});

	// #region SIDE EFFECTS

	useEffect(() => {
		if (
			EditPostData !== null &&
			EditPostData !== undefined &&
			Object.values(EditPostData).length > 0 &&
			EditPostData !== postData
		) {
			setPostData(EditPostData);
		}
	}, [EditPostData]);

	useEffect(() => {
		if (IsEditPostDialogOpen !== isDialogOpen) {
			setIsDialogOpen(IsEditPostDialogOpen);
		}
	}, [IsEditPostDialogOpen]);

	useEffect(() => {
		if (IsEditPostDataLoading !== isEditPostLoading) {
			setIsEditPostLoading(IsEditPostDataLoading);
		}
	}, [IsEditPostDataLoading]);

	// #endregion

	/**
	 * Gets the access token silently using msal.
	 * @returns {string} The access token.
	 */
	const getAccessToken = async () => {
		const tokenData = await instance.acquireTokenSilent({
			...loginRequests,
			account: accounts[0],
		});

		return tokenData.accessToken;
	};

	/**
	 * Handles the key down event.
	 * @param {Event} event The key down event.
	 */
	const handleKeyDown = (event) => {
		if (event.key === "Enter") {
			event.preventDefault();
		}
	};

	/**
	 * Handles the form change event.
	 * @param {Event} event The form change event.
	 */
	const handleFormChange = (event) => {
		event.persist();
		const target = event.target;
		setPostData({
			...postData,
			[target.name]: target.value,
		});
	};

	/**
	 * Handles the react quill content change event.
	 */
	const handleContentChange = useMemo(() => (content) => {
		setPostData({
			...postData,
			Content: content,
		});
	});

	/**
	 * The modules for React Quill
	 */
	const modules = useMemo(
		() => ({
			toolbar: {
				container: [
					[{ header: "1" }, { header: "2" }],
					["bold", "italic", "underline", "blockquote"],
					[{ list: "ordered" }, { list: "bullet" }],
					["link"],
					["clean"],
				],
			},
		}),
		[]
	);

	/**
	 * Handles the update post event.
	 * @param {Event} event The update post event.
	 */
	const handleUpdatePost = async (event) => {
		event.preventDefault();

		const validations = CreatePostPageConstants.validations;
		errors.Title =
			postData.postTitle === "" ? validations.TitleRequired : "";
		errors.postContent =
			postData.postContent === "" ? validations.ContentRequired : "";
		setErrors({ ...errors });

		if (errors.postContent === "" && errors.postTitle === "") {
			const updatePostData = new UpdatePostDtoModel(
				postData.postId,
				postData.postTitle,
				postData.postContent,
				0
			);
			const accessToken = await getAccessToken();
			dispatch(UpdatePostAsync(updatePostData, accessToken));
		}
	};

	/**
	 * Handles the AI rewrite text event.
	 * @param {Event} event The rewrite event.
	 */
	const handleAiRewrite = async (event) => {
		event.preventDefault();
		const strippedContent = postData.Content.replace(
			/<[^>]*>?/gm,
			""
		).trim();
		if (strippedContent !== "") {
			var requestDto = new RewriteRequestDtoModel(postData.Content);
			dispatch(RewriteStoryWithAiAsync(requestDto));
		}
	};

	/**
	 * Handles the edit modal close event.
	 */
	const handleModalClose = () => {
		setIsDialogOpen(false);
		dispatch(ToggleEditPostDialog(false));
	};

	return (
		<Dialog open={isDialogOpen}>
			<DialogSurface>
				<div style={{ position: "relative" }}>
					{isEditPostLoading && (
						<div
							style={{
								position: "absolute",
								top: 0,
								left: 0,
								right: 0,
								bottom: 0,
								display: "flex",
								justifyContent: "center",
								alignItems: "center",
								zIndex: 1000,
							}}
						>
							<Spinner size="large" />
						</div>
					)}
					<form onKeyDown={handleKeyDown} className="addPost">
						<Card appearance="subtle">
							<CardHeader
								header={
									<div className="col sm-12 mb-3 mb-sm-0">
										<div className="row p-2">
											<Label for="Title" className="mb-2">
												Title
											</Label>
											<input
												type="text"
												name="postTitle"
												onChange={handleFormChange}
												value={postData.postTitle}
												className="form-control"
												id="Title"
												placeholder={
													CreatePostPageConstants
														.Headings
														.TitleBarPlaceholder
												}
											/>
											{errors.postTitle && (
												<span className="alert alert-danger ml-10 mt-3">
													{errors.postTitle}
												</span>
											)}
										</div>
									</div>
								}
							/>
							<CardPreview className={styles.cardPreview}>
								<div className="form-group row mt-3">
									<div className="col sm-12 mb-3 mb-sm-0 p-3">
										<ReactQuill
											value={postData.postContent}
											onChange={handleContentChange}
											id="postContent"
											className="text-editor"
											placeholder={
												CreatePostPageConstants.Headings
													.ContentBoxPlaceholder
											}
											modules={modules}
										/>
										{errors.postContent && (
											<span className="alert alert-danger ml-10 mt-3">
												{errors.postContent}
											</span>
										)}
										<Button
											type="button"
											className={styles.button}
											onClick={handleAiRewrite}
										>
											<img
												src={AiButton}
												style={{ height: "20px" }}
											/>{" "}
											Rewrite with AI
										</Button>
									</div>

									<div className="text-center">
										<Button
											type="submit"
											onClick={handleUpdatePost}
											className={styles.editButton}
										>
											{"Edit"}
										</Button>
										&nbsp;
										<Button
											onClick={handleModalClose}
											className={styles.cancelButton}
										>
											{"Close"}
										</Button>
									</div>
								</div>
							</CardPreview>
						</Card>
					</form>
				</div>
			</DialogSurface>
		</Dialog>
	);
}

export default EditPostComponent;
