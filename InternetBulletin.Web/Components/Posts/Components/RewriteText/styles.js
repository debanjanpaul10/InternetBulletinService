import { makeStyles, tokens } from "@fluentui/react-components";

const useStyles = makeStyles({
    rewriteButton: {
        marginTop: "8px",
        marginBottom: "8px",
        boxShadow: tokens.shadow16,
    },
    dialogContent: {
        minWidth: "500px",
        maxWidth: "800px",
        maxHeight: "600px",
        overflowY: "auto",
    },
    dialogActions: {
        display: "flex",
        justifyContent: "flex-end",
        gap: "8px",
        marginTop: "16px",
    },
    textContent: {
        whiteSpace: "pre-wrap",
        marginBottom: "16px",
    },
    cancelButton: {
        color: tokens.colorStatusDangerBackground3,
        cursor: "pointer",
        fontSize: tokens.fontSizeBase300,
        textDecoration: "none",
        "&:hover": {
            color: tokens.colorStatusDangerBackground3Hover,
        },
        boxShadow: tokens.shadow16,
    },
    acceptChangeButton: {
        cursor: "pointer",
        fontSize: tokens.fontSizeBase300,
        textDecoration: "none",
        boxShadow: tokens.shadow16,
    }
});

export { useStyles }