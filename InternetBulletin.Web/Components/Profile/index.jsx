import { useEffect, useState } from "react";
import { useMsal } from "@azure/msal-react";
import { useDispatch, useSelector } from "react-redux";
import { Skeleton, SkeletonItem } from "@fluentui/react-components";

import { loginRequests } from "@services/auth.config";
import { GetUserProfileDataAsync } from "@store/Users/Actions";
import { useStyles } from "./styles";
import { MyProfilePageConstants } from "@helpers/ibbs.constants";
import UserDetailsComponent from "./Components/UserDetails";
import AlienImage from "@assets/Images/alien-pfp.jpg";
import PageNotFound from "@components/Common/PageNotFound";
import UserPostsComponent from "./Components/UserPosts";
import UserRatingsComponent from "./Components/UserRatings";
import BlurText from "@animations/BlurText";
import Magnet from "@animations/Magnet";

/**
 * @component
 * `ProfileComponent` Renders the user's profile page, displaying their personal information,
 * activity (posts and ratings), and handling authentication states.
 *
 * This component integrates with Azure MSAL for user authentication.
 * On successful login, it fetches and displays the user's display name,
 * email address, username, their posts, and their ratings.
 * It shows a skeleton loading UI while data is being fetched.
 * If the user is not logged in, it renders a PageNotFound component.
 *
 * @returns {JSX.Element} The user profile page, or a PageNotFound component
 *                        if the user is not authenticated.
 *
 * @example
 * // Usage in a route or parent component
 * <ProfileComponent />
 */
function ProfileComponent() {
    const dispatch = useDispatch();
    const styles = useStyles();
    const { instance, accounts } = useMsal();
    const { Headings } = MyProfilePageConstants;

    const UserProfileStoreData = useSelector(
        (state) => state.UserReducer.userProfileData
    );
    const IsUserProfileDataLoadingStoreData = useSelector(
        (state) => state.UserReducer.isUserProfileDataLoading
    );

    const [userStateData, setUserStateData] = useState({});
    const [isUserDataLoading, setIsUserDataLoading] = useState(true);
    const [currentLoggedInUser, setCurrentLoggedInUser] = useState({});

    // #region SIDE EFFECTS

    useEffect(() => {
        const getProfileData = async () => {
            const accessToken = await getAccessToken();
            dispatch(GetUserProfileDataAsync(accessToken));
        };
        isUserLoggedIn() && getProfileData();
    }, []);

    useEffect(() => {
        if (
            UserProfileStoreData !== null &&
            UserProfileStoreData !== undefined &&
            Object.values(UserProfileStoreData).length > 0 &&
            UserProfileStoreData !== userStateData
        ) {
            setUserStateData(UserProfileStoreData);
        }
    }, [UserProfileStoreData]);

    useEffect(() => {
        if (isUserDataLoading !== IsUserProfileDataLoadingStoreData) {
            setIsUserDataLoading(IsUserProfileDataLoadingStoreData);
        }
    }, [IsUserProfileDataLoadingStoreData]);

    useEffect(() => {
        if (accounts.length > 0) {
            const userName = accounts[0].idTokenClaims["User Name"];
            setCurrentLoggedInUser(userName);
        } else {
            setCurrentLoggedInUser();
        }
    }, [instance, accounts]);

    /**
     * Checks if user is logged in.
     * @returns {boolean} The boolean value for logged in status.
     */
    const isUserLoggedIn = () => {
        return (
            currentLoggedInUser !== null &&
            currentLoggedInUser !== undefined &&
            currentLoggedInUser?.username !== ""
        );
    };

    // #endregion

    /**
     * Gets the access token silently using msal.
     * @returns {string} The access token.
     */
    const getAccessToken = async () => {
        const tokenData = await instance.acquireTokenSilent({
            ...loginRequests,
            account: accounts[0],
        });

        return tokenData.idToken;
    };

    return isUserLoggedIn() ? (
        <div className="container mt-5">
            <div className="row">
                <div className="col-sm-12">
                    <BlurText
                        text={Headings.WelcomeMessage}
                        delay={150}
                        animateBy="words"
                        direction="top"
                        className={styles.profileHeading}
                    />
                </div>

                {/* USER DETAILS */}
                <div className="row position-relative mt-4">
                    {isUserDataLoading || !userStateData?.displayName ? (
                        <Skeleton
                            aria-label="Profile data loading"
                            as="div"
                            className="row"
                        >
                            <div className="col-12 col-sm-2">
                                <SkeletonItem
                                    appearance="translucent"
                                    animation="pulse"
                                    as="div"
                                    shape="circle"
                                    className={styles.userImgSkeleton}
                                ></SkeletonItem>
                            </div>
                            <div className="col-12 col-sm-10">
                                <SkeletonItem
                                    className={styles.userDataSkeleton}
                                    appearance="translucent"
                                    animation="pulse"
                                    as="div"
                                />
                            </div>
                        </Skeleton>
                    ) : (
                        <div className="row">
                            <div className="col-12 col-sm-2">
                                <div className={styles.imageContainer}>
                                    <Magnet disabled={false} magnetStrength={5}>
                                        <img
                                            src={AlienImage}
                                            alt="Profile"
                                            className={styles.profileImage}
                                        />
                                    </Magnet>
                                </div>
                            </div>
                            <div className="col-12 col-sm-10">
                                <div className={styles.userDetailsContainer}>
                                    <UserDetailsComponent
                                        displayName={userStateData.displayName}
                                        emailAddress={
                                            userStateData.emailAddress
                                        }
                                        userName={userStateData.userName}
                                    />
                                </div>
                            </div>
                        </div>
                    )}
                </div>

                {/* USER ACTIVITY */}
                <div className="row position-relative mt-4">
                    {/* USER POSTS */}
                    {isUserDataLoading || !userStateData?.userPosts ? (
                        <div className="col-12 col-sm-6">
                            <Skeleton>
                                <SkeletonItem
                                    className={styles.userDataSkeleton}
                                    appearance="translucent"
                                    animation="pulse"
                                    as="div"
                                />
                            </Skeleton>
                        </div>
                    ) : (
                        <div className="col-12 col-sm-6">
                            <div className={styles.userPostsContainer}>
                                <UserPostsComponent
                                    userPosts={userStateData.userPosts}
                                />
                            </div>
                        </div>
                    )}

                    {/* USER RATINGS */}
                    {isUserDataLoading || !userStateData?.userPostRatings ? (
                        <div className="cl-12 col-sm-6">
                            <Skeleton
                                aria-label="Profile data loading"
                                as="div"
                                className="row"
                            >
                                <SkeletonItem
                                    appearance="translucent"
                                    animation="pulse"
                                    as="div"
                                    className={styles.userImgSkeleton}
                                ></SkeletonItem>
                            </Skeleton>
                        </div>
                    ) : (
                        <div className="col-12 col-sm-6">
                            <div className={styles.userRatingsContainer}>
                                <UserRatingsComponent
                                    userPostRatings={
                                        userStateData.userPostRatings
                                    }
                                />
                            </div>
                        </div>
                    )}
                </div>
            </div>
        </div>
    ) : (
        <PageNotFound />
    );
}

export default ProfileComponent;
