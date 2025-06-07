/**
 * AddPostDtoModel class to represent the data for adding a post.
 */
class AddPostDtoModel {
	/**
	 * Gets or sets the post title.
	 * @type {string}
	 */
	postTitle = "";

	/**
	 * Gets or sets the content of the post.
	 * @type {string}
	 */
	postContent = "";

	/**
	 * Gets or sets the post created by.
	 * @type {string}
	 */
	postCreatedBy = "";

	/**
	 * Creates an instance of AddPostDtoModel.
	 * @param {string} PostTitle The title of the post.
	 * @param {string} PostContent The content of the post.
	 * @param {string} PostCreatedBy The user who created the post.
	 */
	constructor(PostTitle, PostContent, PostCreatedBy) {
		this.postTitle = PostTitle;
		this.postContent = PostContent;
		this.postCreatedBy = PostCreatedBy;
	}
}

export default AddPostDtoModel;
