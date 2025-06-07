/**
 * PostRatingDtoModel class to represent the post rating dto.
 */
class PostRatingDtoModel {

    /**
     * Gets or sets the post id.
     * @type {string}
     */
    postId = "";

    /**
     * Gets or sets the incremented or decremented value
     * @type {boolean}
     */
    isIncrement = false;

    /**
     * Creates an instance of PostRatingDtoModel
     * @param {string} PostId The post id.
     * @param {boolean} IsIncrement The incremented or decremented value.
     */
    constructor(PostId, IsIncrement) {
        this.postId = PostId;
        this.isIncrement = IsIncrement;
    }

}

export default PostRatingDtoModel;