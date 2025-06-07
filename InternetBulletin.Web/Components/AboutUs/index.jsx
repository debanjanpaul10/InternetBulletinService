import { useEffect } from "react";
import { useDispatch, useSelector } from "react-redux";
import {
    Carousel,
    CarouselNav,
    CarouselNavButton,
    CarouselNavContainer,
    CarouselSlider,
    CarouselViewport,
    LargeTitle,
    Title3,
} from "@fluentui/react-components";
import { HeartFilled } from "@fluentui/react-icons";

import { useStyles } from "@components/AboutUs/styles";
import BannerCardComponent from "@components/AboutUs/Components/BannerCard";
import {
    AboutUsPageConstants,
    MyProfilePageConstants,
} from "@helpers/ibbs.constants";
import DescriptionComponent from "@components/AboutUs/Components/Description";
import { GetApplicationInformationDataAsync } from "@store/Common/Actions";
import Spinner from "@components/Common/Spinner";

/**
 * AboutUsComponent - A React component that displays the About Us section of the application.
 *
 * This component renders:
 * - A main heading using LargeTitle component
 * - A subheading with a heart icon
 * - A carousel that displays banner cards with information from AboutUsData
 *
 * The carousel features:
 * - Automatic sliding with 4-second intervals
 * - Navigation buttons with tooltips
 * - Circular navigation
 * - Single item display at a time
 *
 * @component
 * @returns {JSX.Element} The rendered About Us section with carousel
 */
function AboutUsComponent() {
    const dispatch = useDispatch();
    const styles = useStyles();

    const IsAboutUsLoading = useSelector(
        (state) => state.CommonReducer.isAboutUsLoading
    );
    const ApplicationInformationStoreData = useSelector(
        (state) => state.CommonReducer.applicationInformation
    );

    useEffect(() => {
        dispatch(GetApplicationInformationDataAsync());
    }, []);

    /**
     * The autoplay button props
     */
    const autoPlayProps = {
        "aria-label": "Enable Autoplay",
        checked: true,
        hidden: true,
    };

    return (
        <div className="container">
            {IsAboutUsLoading ? (
                <Spinner isLoading={IsAboutUsLoading} />
            ) : (
                <>
                    <div className="row">
                        <div className="col-sm-12 mt-4">
                            <LargeTitle className={styles.aboutUsHeading}>
                                {MyProfilePageConstants.Headings.AboutUsMessage}
                            </LargeTitle>
                        </div>
                    </div>
                    <div className="row">
                        <div className="col-12">
                            <Title3 className={styles.subHeading}>
                                {AboutUsPageConstants.Subtitle}&nbsp;
                                <HeartFilled className={styles.heart} />
                                &nbsp; and
                            </Title3>
                        </div>
                    </div>

                    <div className="row mt-4">
                        <Carousel
                            groupSize={1}
                            circular
                            autoplayInterval={4000}
                        >
                            <CarouselViewport>
                                <CarouselSlider>
                                    {ApplicationInformationStoreData?.applicationTechnologiesData?.map(
                                        (data, index) => (
                                            <BannerCardComponent
                                                key={`image-${index}`}
                                                index={index}
                                                data={data}
                                            />
                                        )
                                    )}
                                </CarouselSlider>
                            </CarouselViewport>
                            <CarouselNavContainer
                                layout="inline"
                                autoplay={autoPlayProps}
                                nextTooltip={{
                                    content: "Next slide",
                                    relationship: "label",
                                }}
                                prevTooltip={{
                                    content: "Previous slide",
                                    relationship: "label",
                                }}
                            >
                                <CarouselNav
                                    className={styles.carouselNavButton}
                                >
                                    {(index) => (
                                        <CarouselNavButton
                                            aria-label={`Carousel nav button ${index}`}
                                        />
                                    )}
                                </CarouselNav>
                            </CarouselNavContainer>
                        </Carousel>
                    </div>

                    <div className="row mt-4">
                        <DescriptionComponent
                            applicationDescriptionData={
                                ApplicationInformationStoreData?.applicationInformationData
                            }
                        />
                    </div>
                </>
            )}
        </div>
    );
}

export default AboutUsComponent;
