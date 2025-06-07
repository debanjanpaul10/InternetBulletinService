/**
 * UserLoginDtoModel class to represent the user login data.
 */
class UserLoginDtoModel {
	/** The user email.
	 * @type {string}
	 */
	userEmail = "";

	/** The user password.
	 * @type {string}
	 */
	userPassword = "";

	/**
	 * Creates an instance of UserLoginDtoModel
	 * @param {string} UserEmail The user email.
	 * @param {string} UserPassword The user password.
	 */
	constructor(UserEmail, UserPassword) {
		this.userEmail = UserEmail;
		this.userPassword = UserPassword;
	}
}

export default UserLoginDtoModel;
