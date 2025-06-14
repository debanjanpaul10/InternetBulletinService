import { useEffect, useState } from "react";
import {
    FluentProvider,
    Spinner,
    webDarkTheme,
} from "@fluentui/react-components";
import Cookies from "js-cookie";

import IBBS from "@components/IBBS";
import ThemeContext from "@context/ThemeContext";
import { ConsoleLogMessage } from "@helpers/common.utility";
import { CookiesConstants, PageConstants } from "@helpers/ibbs.constants";

/**
 * @component
 * `Main` component that handles theme management and application initialization.
 *
 * @description
 * This component serves as the main wrapper for the application, managing:
 * - Theme state (light/dark mode)
 * - MSAL authentication initialization
 * - Theme persistence using cookies
 *
 * @param {Object} props - Component props
 * @param {Object} props.msalInstance - MSAL authentication instance for handling authentication
 *
 * @state {string} themeMode - Current theme mode ('light' or 'dark')
 * @state {boolean} initialized - Whether MSAL has been initialized
 *
 * @effect
 * On mount:
 * - Loads saved theme preference from cookies
 * - Initializes MSAL authentication
 *
 * @returns {JSX.Element} The main application wrapper with theme context and MSAL provider
 */
function Main({ msalInstance }) {
    const { DarkModeConstant, LightConstant, DarkConstant } = PageConstants;
    const [themeMode, setThemeMode] = useState(DarkConstant);
    const [initialized, setInitialized] = useState(false);

    useEffect(() => {
        const savedDarkModeSettings =
            Cookies.get(CookiesConstants.DarkMode.Name) === "true";
        setThemeMode(savedDarkModeSettings ? DarkConstant : LightConstant);

        document.body.classList.toggle(DarkModeConstant, savedDarkModeSettings);

        msalInstance.initialize().then(() => setInitialized(true));
    }, []);

    /**
     * Toggles the theme mode.
     */
    const toggleThemeMode = () => {
        const newThemeMode =
            themeMode === LightConstant ? DarkConstant : LightConstant;
        setThemeMode(newThemeMode);

        document.body.classList.toggle(
            DarkModeConstant,
            newThemeMode === DarkConstant
        );
        Cookies.set(
            CookiesConstants.DarkMode.Name,
            newThemeMode === DarkConstant,
            {
                expires: CookiesConstants.DarkMode.Timeout,
            }
        );
    };

    return (
        <ThemeContext.Provider value={{ themeMode, toggleThemeMode }}>
            <Spinner isLoading={!initialized} />
            {initialized && (
                <FluentProvider theme={webDarkTheme}>
                    <IBBS />
                </FluentProvider>
            )}
            {ConsoleLogMessage}
        </ThemeContext.Provider>
    );
}

export default Main;
