import { makeStyles, tokens } from "@fluentui/react-components";

const useStyles = makeStyles({
	card: {
		margin: "20px auto",
		width: "100%",
		maxWidth: "100%",
		padding: "20px",
		boxShadow: tokens.shadow28,
		borderRadius: "8px",
		backgroundColor: tokens.colorNeutralCardBackground,
		"&:hover": {
			backgroundColor: tokens.colorNeutralCardBackgroundHover,
		},
	},
	cardPreview: {
		marginTop: "auto",
		padding: "0px 10px 0px 10px",
	},
	postContent: {
		fontSize: tokens.fontSizeBase300,
		lineHeight: "1.6",
		overflow: "hidden",
		textOverflow: "ellipsis",
	},
	createButton: {
		color: tokens.colorBrandForegroundLink,
		margin: "10px 0px 10px 0px",
		cursor: "pointer",
		fontSize: tokens.fontSizeBase300,
		textDecoration: "none",
		"&:hover": {
			color: tokens.colorBrandForegroundLinkHover,
		},
	},
	cancelButton: {
		color: tokens.colorStatusDangerBackground3,
		margin: "10px 0px 10px 0px",
		cursor: "pointer",
		fontSize: tokens.fontSizeBase300,
		textDecoration: "none",
		"&:hover": {
			color: tokens.colorStatusDangerBackground3Hover,
		},
	},
	aiEditButton: {
		margin: "10px 0px 10px 0px",
		cursor: "pointer",
		fontSize: tokens.fontSizeBase300,
		textDecoration: "none",
		padding: "0.75em",
	},
	addNewHeading: {
		fontFamily: "Architects Daughter",
		textAlign: "center",
		display: "flex",
		justifyContent: "center",
		alignItems: "center",
	},
});

export { useStyles };
