import { makeStyles, tokens } from "@fluentui/react-components";

const useStyles = makeStyles({
	card: {
		margin: "20px auto",
		width: "100%",
		maxWidth: "100%",
		padding: "20px",
		boxShadow: tokens.shadow28,
		borderRadius: "8px",
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
	button: {
		margin: "10px 0px 10px 0px",
		cursor: "pointer",
		fontSize: tokens.fontSizeBase300,
		textDecoration: "none",
	},
	aiButton: {
		margin: "30px 0px 10px 0px",
		cursor: "pointer",
		fontSize: tokens.fontSizeBase300,
		textDecoration: "none",
	},
	editButton: {
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
	rewriteTextSkeleton: {
		width: "100%",
		borderRadius: tokens.borderRadiusLarge,
	},
	rewriteTextBox: {
		height: "300px",
		"& .ql-container": {
			height: "calc(100% - 42px)",
		},
		"& .ql-editor": {
			minHeight: "100%",
			"&::-webkit-scrollbar": {
				width: "8px",
			},
			"&::-webkit-scrollbar-track": {
				background: "var(--colorNeutralBackground1)",
			},
			"&::-webkit-scrollbar-thumb": {
				background: "var(--colorNeutralForeground3)",
				borderRadius: "4px",
				"&:hover": {
					background: "var(--colorNeutralForeground2)",
				},
			},
		},
	}
});
export { useStyles };
