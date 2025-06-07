import { ConsoleMessage } from "@helpers/ibbs.constants";

/**
 * Renders a custom console log message with author information
 */
export const ConsoleLogMessage = console.log(
	"%c %s",
	"color:red; font-size: 22pt; font-family: 'Source Code Pro'",
	ConsoleMessage
);

/**
 * Formats the date to date string format.
 * @param {Date} date The unformatted date
 * @returns {string} The formatted date.
 */
export const formatDate = (date) => {
	return new Date(date).toDateString();
};
