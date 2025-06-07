import { makeStyles, tokens } from "@fluentui/react-components";

const useStyles = makeStyles({
    descriptionContainer: {
        maxWidth: "full",
        margin: "0 auto",
        padding: "2rem",
        lineHeight: 1.6,
    },
    titles: {
        textAlign: "center",
        display: "flex",
        justifyContent: "center",
        alignItems: "center",
        marginBottom: "10px",
        color: tokens.colorPaletteSeafoamForeground2,
    },
    introTitle: {
        color: tokens.colorPaletteSeafoamForeground2,
    },
    card: {
        maxWidth: "100%",
        margin: "1rem",
        display: "flex",
        flexDirection: "column",
        boxShadow: tokens.shadow28,
        backgroundColor: tokens.colorNeutralCardBackground,
        "&:hover": {
            backgroundColor: tokens.colorNeutralCardBackgroundHover,
        },
    },
    treeCard: {
        maxWidth: "100%",
        margin: "1rem",
        display: "flex",
        flexDirection: "column",
        boxShadow: tokens.shadow28,
        backgroundColor: tokens.colorNeutralCardBackground,
    },
    listItem: {
        marginLeft: "-40px",
    },
    textSize: {
        fontSize: tokens.fontSizeBase400,
    },
});

export { useStyles };
