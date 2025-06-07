/**
 * RewriteRequestDtoModel class to represent the rewrite request model.
 */
class RewriteRequestDtoModel {
    /**
     * The story.
     * @type {string}
     */
    story = "";

    /**
     * Creates an instance of RewriteRequestDtoModel
     * @param {string} Story The story.
     */
    constructor(Story) {
        this.story = Story;
    }
}

export default RewriteRequestDtoModel;