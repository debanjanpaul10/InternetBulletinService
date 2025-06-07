import { CarouselCard, Image, Button } from "@fluentui/react-components";

import { useStyles } from "@components/AboutUs/Components/BannerCard/styles";
import { AboutUsPageConstants } from "@helpers/ibbs.constants";

/**
 * A card component that displays a banner with an image, heading, description, and a link button.
 * @component
 * @param {Object} props - Component props
 * @param {Object} props.data - The banner data object
 * @param {string} props.data.image - URL of the banner image
 * @param {string} props.data.heading - The main heading text
 * @param {string} props.data.description - The descriptive text below the heading
 * @param {string} props.data.link - URL for the button link
 * @param {number} props.index - The index of the banner in a carousel
 * @returns {JSX.Element} A carousel card with banner content
 */
function BannerCardComponent({ data, index }) {
	const styles = useStyles();

	return (
		<CarouselCard
			className={styles.bannerCard}
			aria-label={`${index + 1} of ${1}`}
			id={`test-${index}`}
		>
			<Image fit="cover" src={data.image} role="presentation" />
			<div className={styles.cardContainer}>
				<div className={styles.title}>{data.heading}</div>
				<div className={styles.subtext}>{data.description}</div>
				<div>
					<Button className={styles.linkButton} size="small" shape="square" onClick={() => window.open(data.link, "_blank", "noopener,noreferrer")}>
						{AboutUsPageConstants.ButtonTexts.WebsiteNav}
					</Button>
				</div>
			</div>
		</CarouselCard>
	);
}

export default BannerCardComponent;
