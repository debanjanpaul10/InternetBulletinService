import { makeStyles, tokens } from "@fluentui/react-components";

const useStyles = makeStyles({
    card: {
        margin: "20px auto",
        width: "720px",
        maxWidth: "100%",
        padding: "20px",
        borderRadius: "8px",
        boxShadow: tokens.shadow28,
        backgroundColor: tokens.colorNeutralCardBackground,
        "&:hover": {
            backgroundColor: tokens.colorNeutralCardBackgroundHover,
        },
    },
    cardHeader: {
        marginBottom: "10px",
    },
    headerContainer: {
        display: "flex",
        justifyContent: "space-between",
        alignItems: "flex-start",
        width: "100%",
        gap: "10px",
    },
    headerTitle: {
        fontSize: tokens.fontSizeBase600,
        marginBottom: "10px",
        flexGrow: 1,
        lineHeight: tokens.lineHeightBase500,
        wordBreak: "break-word",
        minWidth: 0,
    },
    cardPreview: {
        marginTop: "auto",
        padding: "10px 10px 0px 10px",
    },
    postContent: {
        fontSize: tokens.fontSizeBase300,
        lineHeight: tokens.lineHeightBase300,
        overflow: "hidden",
        textOverflow: "ellipsis",
        borderBottom: "1px solid " + tokens.colorNeutralStroke1,
    },
    textSizeButton: {
        marginTop: "-10px",
        color: tokens.colorPaletteSeafoamForeground2,
        cursor: "pointer",
        fontSize: tokens.fontSizeBase300,
        textDecoration: "none",
    },
    headerButtons: {
        display: "flex",
        flexShrink: 0,
        gap: "8px",
    },
    editButton: {
        color: tokens.colorPaletteYellowBackground3,
        "&:hover": {
            color: tokens.colorPaletteYellowForeground2,
        },
    },
    deleteButton: {
        color: tokens.colorPaletteRedBackground3,
        "&:hover": {
            color: tokens.colorPaletteRedForeground2,
        },
    },
});

export { useStyles };
