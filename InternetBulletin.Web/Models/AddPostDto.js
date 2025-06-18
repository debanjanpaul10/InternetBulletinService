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
	 * Gets or sets the IsNSFW flag.
	 * @type {boolean}
	 */
	isNSFW = false;

	/**
	 * Gets or sets the genre tag.
	 * @type {string}
	 */
	genreTag = "";

	/**
	 * Creates an instance of AddPostDtoModel.
	 * @param {string} PostTitle The title of the post.
	 * @param {string} PostContent The content of the post.
	 * @param {string} PostCreatedBy The user who created the post.
	 * @param {boolean} IsNSFW The NSFW rating.
	 * @param {string} GenreTag The genre tag.
	 */
	constructor(PostTitle, PostContent, PostCreatedBy, IsNSFW, GenreTag) {
		this.postTitle = PostTitle;
		this.postContent = PostContent;
		this.postCreatedBy = PostCreatedBy;
		this.isNSFW = IsNSFW;
		this.genreTag = GenreTag;
	}
}

export default AddPostDtoModel;
