import { makeStyles, tokens } from "@fluentui/react-components";

const useStyles = makeStyles({
    themeToggleButton: {
        width: "50px",
        minWidth: "50px",
        padding: "0px",
    },
    sideBarButton: {
        padding: "0px !important",
        "&:hover": {
            backgroundColor: tokens.colorPaletteSeafoamBackground2,
        },
    },
    navbar: {
        padding: "10px 15px",
        "@media (max-width: 768px)": {
            padding: "15px",
        },
        backgroundColor: tokens.colorPaletteSeafoamBackground2,
    },
    navContent: {
        display: "flex",
        width: "100%",
        alignItems: "center",
    },
    homeButton: {
        "&:hover": {
            backgroundColor: tokens.colorPaletteSeafoamBackground2,
        },
    },
});

export default useStyles;
