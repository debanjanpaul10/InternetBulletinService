/**
 * UserProfileDtoModel class to represent the user profile dto model.
 */
class UserProfileDtoModel {

	/**
	 * Gets or sets the user identifier.
	 * @type {string}
	 */
	userName = "";

	/**
	 * Gets or sets the user posts.
	 * @type {Array<Object>}
	 */
	userPosts = [];

	/**
	 * Creates an instance of UserProfileDtoModel
	 * @param {string} UserName The user identifier.
	 * @param {Array<Object>} UserPosts The user posts.
	 */
	constructor(UserName, UserPosts) {
		this.userName = UserName;
		this.userPosts = UserPosts;
	}
}

export default UserProfileDtoModel;
