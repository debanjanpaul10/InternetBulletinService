import { useEffect, useState } from "react";
import { useDispatch, useSelector } from "react-redux";
import {
	Button,
	DrawerBody,
	DrawerHeader,
	DrawerHeaderTitle,
	OverlayDrawer,
	Tooltip,
	useRestoreFocusSource,
} from "@fluentui/react-components";
import {
	SignOut24Regular,
	AddCircle28Regular,
	PersonAdd28Regular,
	Person28Regular,
	BookOpen28Regular,
	PanelLeftContract28Regular,
} from "@fluentui/react-icons";
import { useMsal } from "@azure/msal-react";
import { useNavigate } from "react-router-dom";

import { useStyles } from "./styles";
import {
	ToggleErrorToaster,
	ToggleSideBar,
	ToggleSuccessToaster,
} from "@store/Common/Actions";
import {
	HeaderPageConstants,
	HomePageConstants,
	LoginPageConstants,
} from "@helpers/ibbs.constants";
import AppLogo from "@assets/Images/IBBS_logo.png";
import { loginRequests } from "@services/auth.config";
import {
	GetAllPostsAsync,
	StartLoader,
	StopLoader,
} from "@store/Posts/Actions";
import { DrawerMotion } from "./motion";

/**
 * SideDrawerComponent - A responsive side navigation drawer component that provides
 * navigation and authentication controls for the IBBS application.
 *
 * @component
 * @description
 * This component renders a side drawer that contains:
 * - Application logo and home navigation
 * - Create post button (for logged-in users)
 * - Login/Logout functionality
 * - User profile access
 *
 * The drawer integrates with Microsoft Authentication Library (MSAL) for authentication
 * and uses Redux for state management.
 *
 * @example
 * ```jsx
 * <SideDrawerComponent />
 * ```
 *
 * @returns {JSX.Element} A Fluent UI OverlayDrawer component with navigation controls
 *
 * @property {Object} styles - Styles object from useStyles hook
 * @property {Function} navigate - React Router navigation function
 * @property {Function} dispatch - Redux dispatch function
 * @property {Object} restoreFocusSourceAttributes - Focus management attributes
 * @property {Object} accounts - MSAL accounts information
 * @property {Object} instance - MSAL instance
 * @property {boolean} IsSideBarOpen - Redux state for drawer open/close
 * @property {boolean} isSideBarOpenState - Local state for drawer open/close
 * @property {Object} currentLoggedInUser - Current user information
 *
 * @see {@link https://react.fluentui.dev/?path=/docs/components-overlaydrawer--default OverlayDrawer Documentation}
 * @see {@link https://github.com/AzureAD/microsoft-authentication-library-for-js MSAL Documentation}
 */
function SideDrawerComponent() {
	const styles = useStyles();
	const navigate = useNavigate();
	const dispatch = useDispatch();
	const restoreFocusSourceAttributes = useRestoreFocusSource();
	const { accounts, instance } = useMsal();

	const { Headings, ButtonTitles } = HeaderPageConstants;

	const IsSideBarOpen = useSelector(
		(state) => state.CommonReducer.isSideBarOpen
	);

	const [isSideBarOpenState, setIsSideBarOpenState] = useState(false);
	const [currentLoggedInUser, setCurrentLoggedInUser] = useState({});

	useEffect(() => {
		if (IsSideBarOpen !== isSideBarOpenState) {
			setIsSideBarOpenState(IsSideBarOpen);
		}
	}, [IsSideBarOpen]);

	useEffect(() => {
		if (accounts.length > 0) {
			const userName = accounts[0].idTokenClaims?.extension_UserName;
			setCurrentLoggedInUser(userName);
		} else {
			setCurrentLoggedInUser();
		}
	}, [instance, accounts]);

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
	 * Checks if user logged in.
	 * @returns {boolean} The boolean value of user login.
	 */
	const isUserLoggedIn = () => {
		return (
			currentLoggedInUser !== null &&
			currentLoggedInUser !== undefined &&
			currentLoggedInUser?.username !== ""
		);
	};

	const handleSideBarClose = () => {
		dispatch(ToggleSideBar(false));
	};

	/**
	 * Handles the home page redirection.
	 */
	const handleHomePageRedirect = () => {
		navigate(Headings.Home.Link);
		dispatch(ToggleSideBar(false));
	};

	/**
	 * Handles the login event.
	 */
	const handleLoginEvent = () => {
		dispatch(StartLoader());
		instance
			.loginRedirect(loginRequests)
			.then(async () => {
				await handleLoginSuccess();
			})
			.catch((error) => {
				dispatch(
					ToggleErrorToaster({
						shouldShow: true,
						errorMessage: error,
					})
				);
				console.error(error);
			})
			.finally(() => {
				dispatch(StopLoader());
			});

		dispatch(ToggleSideBar(false));
	};

	/**
	 * Handles the successful login event.
	 */
	async function handleLoginSuccess() {
		dispatch(
			ToggleSuccessToaster({
				shouldShow: true,
				successMessage: LoginPageConstants.LoginSuccess,
			})
		);

		let token = "";
		if (accounts.length > 0) {
			token = await getAccessToken();
		}
		dispatch(GetAllPostsAsync(token));
	}

	/**
	 * Handles the user logout event.
	 */
	const handleLogout = () => {
		dispatch(StartLoader());
		instance
			.logoutRedirect({
				postLogoutRedirectUri: window.location.origin,
			})
			.then(() => {
				dispatch(
					ToggleSuccessToaster({
						shouldShow: true,
						successMessage: LoginPageConstants.LogoutSuccess,
					})
				);
			})
			.catch((error) => {
				dispatch(
					ToggleErrorToaster({
						shouldShow: true,
						errorMessage: error,
					})
				);
				console.error(error);
			})
			.finally(() => {
				dispatch(StopLoader());
			});
		dispatch(ToggleSideBar(false));
	};

	/**
	 * Handles the profile click redirection.
	 */
	const handleProfileRedirect = async () => {
		navigate(Headings.MyProfile.Link);
		dispatch(ToggleSideBar(false));
	};

	/**
	 * Handles the Add new post page redirection.
	 */
	const handleAddNewPostPageRedirect = () => {
		navigate(Headings.CreatePost.Link);
		dispatch(ToggleSideBar(false));
	};

	/**
	 * Handles the About us page redirection.
	 */
	const handleAboutUsPageRedirect = () => {
		navigate(Headings.AboutUs.Link);
		dispatch(ToggleSideBar(false));
	};

	return (
		<OverlayDrawer
			{...restoreFocusSourceAttributes}
			as="aside"
			open={isSideBarOpenState}
			onOpenChange={(_, { open }) => setIsSideBarOpenState(open)}
			surfaceMotion={{
				children: (_, props) => <DrawerMotion {...props} />,
			}}
			size="medium"
		>
			<DrawerHeader className={styles.drawerHeader}>
				<DrawerHeaderTitle
					action={
						<Button
							appearance="subtle"
							aria-label="Close"
							onClick={handleSideBarClose}
						>
							<PanelLeftContract28Regular />
						</Button>
					}
				>
					{/* HOME BUTTON */}
					<Button
						onClick={handleHomePageRedirect}
						className={styles.homeButton}
						appearance="subtle"
					>
						<img src={AppLogo} height={"30px"} />
						&nbsp; {HomePageConstants.Headings.IBBS}
					</Button>
				</DrawerHeaderTitle>
			</DrawerHeader>

			<DrawerBody className={styles.drawerBody}>
				{/* CREATE NEW POST */}
				{isUserLoggedIn() && (
					<div className="row">
						{location.pathname !== Headings.CreatePost.Link && (
							<Tooltip
								content={ButtonTitles.Create}
								relationship="label"
								positioning="after"
							>
								<Button
									onClick={handleAddNewPostPageRedirect}
									className={styles.button}
									appearance="transparent"
								>
									<AddCircle28Regular />
									<span>{Headings.CreatePost.Name}</span>
								</Button>
							</Tooltip>
						)}
					</div>
				)}

				{/* MY PROFILE */}
				{isUserLoggedIn() && (
					<div className="row">
						{location.pathname !== Headings.MyProfile.Link && (
							<Tooltip
								content={ButtonTitles.MyProfile}
								relationship="label"
								positioning="after"
							>
								<Button
									className={styles.button}
									onClick={handleProfileRedirect}
									appearance="transparent"
								>
									<Person28Regular />
									<span>{Headings.MyProfile.Name}</span>
								</Button>
							</Tooltip>
						)}
					</div>
				)}

				{/* ABOUT US */}
				<div className="row">
					{location.pathname !== Headings.AboutUs.Link && (
						<Tooltip
							content={ButtonTitles.AboutUs}
							relationship="label"
							positioning="after"
						>
							<Button
								onClick={handleAboutUsPageRedirect}
								className={styles.button}
								appearance="transparent"
							>
								<BookOpen28Regular />
								<span>{Headings.AboutUs.Name}</span>
							</Button>
						</Tooltip>
					)}
				</div>

				{/* LOGIN AND LOGOUT */}
				<div className="row">
					{!isUserLoggedIn() ? (
						<Tooltip
							content={ButtonTitles.Login}
							relationship="label"
							positioning="after"
						>
							<Button
								className={styles.button}
								onClick={handleLoginEvent}
								appearance="transparent"
							>
								<PersonAdd28Regular />
								<span>{Headings.Login.Name}</span>
							</Button>
						</Tooltip>
					) : (
						<Tooltip
							content={ButtonTitles.Logout}
							relationship="label"
							positioning="after"
						>
							<Button
								className={styles.button}
								onClick={handleLogout}
								appearance="transparent"
							>
								<SignOut24Regular
									className={styles.signoutImg}
								/>
								<span>{Headings.Logout.Name}</span>
							</Button>
						</Tooltip>
					)}
				</div>
			</DrawerBody>
		</OverlayDrawer>
	);
}

export default SideDrawerComponent;
