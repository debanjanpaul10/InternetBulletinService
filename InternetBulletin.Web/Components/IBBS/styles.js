import { makeStyles } from "@fluentui/react-components";

const useStyles = makeStyles({
	headerNav: {
		position: "fixed",
		top: 0,
		left: 0,
		right: 0,
		zIndex: 1000,
	},
	bodyContent: {
		marginTop: "64px",
		overflow: "auto",
		minHeight: "calc(100vh)",
	},
	footerContent: {
		width: "90%",
		height: "60px",
		display: "flex",
		justifyContent: "center",
		margin: "0 auto",
		borderTop: "1px solid",
	},
});

export { useStyles };
