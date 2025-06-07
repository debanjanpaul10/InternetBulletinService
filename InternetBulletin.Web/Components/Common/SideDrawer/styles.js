import { makeStyles, tokens } from "@fluentui/react-components";

const useStyles = makeStyles({
	content: {
		flex: "1",
		padding: "16px",
		display: "grid",
		justifyContent: "flex-start",
		alignItems: "flex-start",
		gridRowGap: tokens.spacingVerticalXXL,
		gridAutoRows: "max-content",
		height: "100%",
	},
	homeButton: {
		margin: "0px 5px !important",
	},
	field: {
		display: "grid",
		gridRowGap: tokens.spacingVerticalS,
	},
	drawerHeader: {
		padding: "16px",
		borderBottom: "1px active",
	},
	drawerBody: {
		padding: "16px",
		height: "calc(100% - 60px)",
		overflowY: "auto",
	},
	createLink: {
		width: "100%",
	},
	button: {
		padding: "20px",
		display: "flex",
		alignItems: "center",
		justifyContent: "flex-start",
		width: "100%",
		gap: "8px",
		":hover": {
			backgroundColor: tokens.colorNeutralBackground3Hover,
			transform: "translateX(4px)",
		},
		":active": {
			backgroundColor: tokens.colorBrandBackground2,
		},
		borderBottom: "1px active",
	},
	signoutImg: {
		width: "28px !important",
		height: "28px !important",
	},
});

export { useStyles };
