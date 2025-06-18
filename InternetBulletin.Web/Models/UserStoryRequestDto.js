/**
 * UserStoryRequestDtoModel class to represent the ai request model.
 */
class UserStoryRequestDtoModel {
    /**
     * The story.
     * @type {string}
     */
    story = "";

    /**
     * Creates an instance of UserStoryRequestDtoModel
     * @param {string} Story The story.
     */
    constructor(Story) {
        this.story = Story;
    }
}

export default UserStoryRequestDtoModel;
