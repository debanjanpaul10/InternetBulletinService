import React from "react";
import { LargeTitle } from "@fluentui/react-components";

import { ErrorPageConstants } from "@helpers/ibbs.constants";
import { useStyles } from "./styles";

/**
 * @component
 * The Page Not Found error component that is shown when an
 * invalid url is accessed.
 * @returns {JSX.Element} The page not found component JSX element.
 */
function PageNotFound() {
	const styles = useStyles();

	return (
		<div className="container">
			<div className="row">
				<div className="col-sm-12 mt-5">
					<LargeTitle className={styles.notFoundHeader}>
						{ErrorPageConstants.PageNotFoundMessage}
					</LargeTitle>
				</div>
			</div>
		</div>
	);
}

export default PageNotFound;
