import {
    makeStyles,
    tokens,
    typographyStyles,
} from "@fluentui/react-components";

const useStyles = makeStyles({
    bannerCard: {
        alignContent: "center",
        borderRadius: tokens.borderRadiusLarge,
        height: "400px",
        textAlign: "left",
        position: "relative",
    },
    cardContainer: {
        display: "flex",
        flexDirection: "column",
        gap: "8px",
        position: "absolute",
        left: "5%",
        top: "25%",
        background: tokens.colorNeutralBackground1,
        padding: "18px",
        maxWidth: "270px",
        width: "50%",
        boxShadow: tokens.shadow28Brand,
    },
    title: {
        ...typographyStyles.title1,
    },
    subtext: {
        ...typographyStyles.body1,
    },
    linkButton: {
        color: tokens.colorPaletteSeafoamForeground2,
        fontSize: tokens.fontSizeBase300,
    },
});

export { useStyles };
