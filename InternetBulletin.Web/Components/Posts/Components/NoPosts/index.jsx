import React from "react";
import { NoPostsMessageConstant } from "@helpers/ibbs.constants";
import { useStyles } from "./styles";
import { LargeTitle } from "@fluentui/react-components";

/**
 * @component
 * Displays a message when there are no posts.
 *
 * @returns {JSX.Element} The No Posts container JSX element.
 */
function NoPostsContainer() {
	const styles = useStyles();

	return (
		<div className={styles.noPostsContainer}>
			<LargeTitle className={styles.noPostsHeading}>
				{NoPostsMessageConstant.Heading}
			</LargeTitle>
		</div>
	);
}

export default NoPostsContainer;
