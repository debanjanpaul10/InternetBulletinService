import React, { useEffect, useState } from "react";
import { useDispatch, useSelector } from "react-redux";
import { NavLink, useLocation } from "react-router-dom";
import Cookies from "js-cookie";

import {
	CookiesConstants,
	HeaderPageConstants,
	HomePageConstants,
} from "@helpers/Constants";
import AppLogo from "../../../Images/IBBS_logo.png";
import { RemoveCurrentLoggedInUserData } from "@store/Users/Actions";

/**
 * @component
 * Header component that renders the navigation bar.
 *
 * @returns {JSX.Element} The rendered component.
 */
function Header() {
	const dispatch = useDispatch();
	const location = useLocation();

	const activeStyle = { color: "#F15B2A" };
	const { Headings } = HeaderPageConstants;
	const { ButtonTitles } = HeaderPageConstants;

	const UserStoreData = useSelector((state) => state.UsersReducer.userData);

	const [isDarkMode, setIsDarkMode] = useState(false);
	const [currentLoggedInUser, setCurrentLoggedInUser] = useState({});

	useEffect(() => {
		const savedDarkModeSettings =
			Cookies.get(CookiesConstants.DarkMode.Name) === "true";
		setIsDarkMode(savedDarkModeSettings);
		document.body.classList.toggle("dark-mode", savedDarkModeSettings);

		const currentLoggedInUserCookies = Cookies.get(
			CookiesConstants.LoggedInUser.Name
		);
		if (
			currentLoggedInUserCookies !== "" &&
			currentLoggedInUserCookies !== undefined
		) {
			setCurrentLoggedInUser(JSON.parse(currentLoggedInUserCookies));
		}
	}, []);

	useEffect(() => {
		if (
			UserStoreData &&
			Object.keys(UserStoreData).length > 0 &&
			currentLoggedInUser !== UserStoreData
		) {
			setCurrentLoggedInUser(UserStoreData);
		}
	}, [UserStoreData, currentLoggedInUser]);

	/**
	 * Handles the dark mode - light moddle toggle.
	 */
	const toggleDarkMode = () => {
		const newDarkMode = !isDarkMode;
		setIsDarkMode(newDarkMode);
		document.body.classList.toggle("dark-mode", newDarkMode);
		Cookies.set(CookiesConstants.DarkMode.Name, newDarkMode, {
			expires: CookiesConstants.DarkMode.Timeout,
		});
	};

	/**
	 * Handles the icon rendering.
	 * @param {bool} isDarkMode The boolean flag for dark mode.
	 * @returns {string} The icon props classes.
	 */
	const handleShowIcon = (isDarkMode) => {
		var extraIcon = isDarkMode
			? "fa fa-sun-o lightgrey"
			: "fa fa-moon-o lightgrey";
		return `buttonStyle ${extraIcon} p-2 mt-2`;
	};

	/**
	 * Handles the user logout event.
	 */
	const handleLogout = () => {
		dispatch(RemoveCurrentLoggedInUserData());
		Cookies.remove(CookiesConstants.LoggedInUser.Name);
		setCurrentLoggedInUser({});
	};

	/**
	 * Checks if user logged in.
	 * @returns {boolean} The boolean value of user login.
	 */
	const isUserLoggedIn = () => {
		return Object.keys(currentLoggedInUser).length > 0;
	};

	return (
		<nav className="navbar navbar-expand-lg navbar-dark bg-dark">
			<div className="d-flex w-100">
				<div className="navbar-nav mr-auto">
					<NavLink
						to={Headings.Home.Link}
						className="nav-link"
						title={ButtonTitles.HomeButton}
					>
						<img src={AppLogo} height={"30px"} />
						&nbsp; {HomePageConstants.Headings.IBBS}
					</NavLink>
				</div>

				<div className="navbar-nav mx-auto">
					{isUserLoggedIn() &&
						location.pathname !== Headings.CreatePost.Link && (
							<NavLink
								to={Headings.CreatePost.Link}
								activeStyle={activeStyle}
								className="nav-link create-link"
								title={ButtonTitles.Create}
							>
								<i className="fa fa-plus-circle icon-large"></i>{" "}
								&nbsp;
								<span className="create-text">
									{ButtonTitles.Create}
								</span>
							</NavLink>
						)}
				</div>

				<div className="navbar-nav ml-auto">
					{!isUserLoggedIn() ? (
						<NavLink
							to={Headings.Login.Link}
							activeStyle={activeStyle}
							className="nav-link buttonStyle"
							title={ButtonTitles.Login}
						>
							{Headings.Login.Name}
						</NavLink>
					) : (
						<NavLink
							className="nav-link buttonStyle"
							onClick={handleLogout}
							title={ButtonTitles.Logout}
							to={"/"}
						>
							{Headings.Logout.Name}
						</NavLink>
					)}

					<NavLink
						to={Headings.Register.Link}
						activeStyle={activeStyle}
						className="nav-link"
						title={ButtonTitles.Register}
					>
						{Headings.Register.Name}
					</NavLink>

					<i
						onClick={toggleDarkMode}
						className={handleShowIcon(isDarkMode)}
						title={
							isDarkMode
								? ButtonTitles.TurnOnLight
								: ButtonTitles.TurnOnDark
						}
					></i>
				</div>
			</div>
		</nav>
	);
}

export default Header;
