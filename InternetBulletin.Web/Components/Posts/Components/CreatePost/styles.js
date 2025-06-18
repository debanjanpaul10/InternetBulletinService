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
    },
    cardHeaderText: {
        boxShadow: tokens.shadow16,
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
        boxShadow: tokens.shadow16,
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
        boxShadow: tokens.shadow16,
    },
    aiEditButton: {
        margin: "10px 0px 10px 0px",
        cursor: "pointer",
        fontSize: tokens.fontSizeBase300,
        textDecoration: "none",
        padding: "0.75em",
        boxShadow: tokens.shadow16,
    },
    addNewHeading: {
        fontFamily: "Concert One",
        textAlign: "center",
        display: "flex",
        justifyContent: "center",
        alignItems: "center",
        fontSize: tokens.fontSizeHero1000,
        lineHeight: 1.0,
    },
    moderateWithAiButton: {
        boxShadow: tokens.shadow16,
    },
    moderateWithAiButtonText: {
        color: tokens.colorPaletteSeafoamBackground2,
    },
    nsfwTag: {
        color: tokens.colorStatusDangerBackground3,
        "&:hover": {
            color: tokens.colorStatusDangerBackground3Hover,
        },
        boxShadow: tokens.shadow16,
    },
    genreTag: {
        color: tokens.colorBrandForegroundLink,
        "&:hover": {
            color: tokens.colorBrandForegroundLinkHover,
        },
        boxShadow: tokens.shadow16,
    },
    textEditor: {
        boxShadow: tokens.shadow16,
    }
});

export { useStyles };
