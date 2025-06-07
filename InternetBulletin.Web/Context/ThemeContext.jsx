import { createContext } from "react";
import { PageConstants } from "@helpers/ibbs.constants";

/**
 * The Theme Context for the application
 */
const ThemeContext = createContext({
	themeMode: PageConstants.LightConstant,
	toggleThemeMode: () => { },
});

export default ThemeContext;
