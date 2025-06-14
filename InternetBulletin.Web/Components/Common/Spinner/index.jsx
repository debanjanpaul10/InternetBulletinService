import React from "react";

import AppLogo from "@assets/Images/IBBS_logo.png";
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
    const styles = useStyles();

    return (
        isLoading && (
            <div className={styles.spinnerPageDark}>
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
