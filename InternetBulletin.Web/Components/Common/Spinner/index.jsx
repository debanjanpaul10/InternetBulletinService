import React, { useContext } from "react";

import AppLogo from "@assets/Images/IBBS_logo.png";
import ThemeContext from "@context/ThemeContext";
import { useStyles } from "./styles";

/**
 * @component
 * The Spinner Component used when pages are loading. Is a blocking action.
 *
 * @param {Object} props - The component props.
 * @param {boolean} props.isLoading - The boolean for loading screen.
 *
 * @returns {JSX.Element}
 */
function Spinner({ isLoading }) {
	const { themeMode } = useContext(ThemeContext);
	const styles = useStyles();
	const spinnerStyles = themeMode === "dark" ? styles.spinnerPageDark : styles.spinnerPageLight;

	return (
		isLoading && (
			<div className={spinnerStyles}>
				<img
					src={AppLogo}
					height={"100px"}
					className="heartbeat"
					alt="Loading..."
				/>
			</div>
		)
	);
}

export default Spinner;
