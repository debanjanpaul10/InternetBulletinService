import { makeStyles, tokens } from "@fluentui/react-components";

const useStyles = makeStyles({
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
});

export { useStyles };
