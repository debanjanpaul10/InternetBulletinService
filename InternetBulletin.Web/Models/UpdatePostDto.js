/**
 * UpdatePostDtoModel class to represent the data for updating an existing post.
 */
class UpdatePostDtoModel {
	/**
	 * Gets or sets the post identifier.
	 * @type {string}
	 */
	postId = "";

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
	 * Gets or sets the post rating.
	 * @type {number}
	 */
	postRating = 0;

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
	 * Creates an instance of UpdatePostDtoModel.
	 * @param {string} PostId The post identifier.
	 * @param {string} PostTitle The post title.
	 * @param {string} PostContent The content of the post.
	 * @param {number} PostRating The post rating.
	 * @param {boolean} IsNSFW The NSFW rating.
	 * @param {string} GenreTag The genre tag.
	 */
	constructor(PostId, PostTitle, PostContent, PostRating, IsNSFW, GenreTag) {
		this.postId = PostId;
		this.postTitle = PostTitle;
		this.postContent = PostContent;
		this.postRating = PostRating;
		this.isNSFW = IsNSFW;
		this.genreTag = GenreTag;
	}
}

export default UpdatePostDtoModel;