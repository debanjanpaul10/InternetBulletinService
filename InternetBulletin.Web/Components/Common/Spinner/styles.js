import { makeStyles } from "@fluentui/react-components";

const useStyles = makeStyles({
    spinnerPageDark: {
        position: "fixed",
        top: 0,
        left: 0,
        width: "100%",
        height: "100%",
        display: "flex",
        justifyContent: "center",
        alignItems: "center",
        zIndex: 9999,
        backgroundColor: "rgba(0, 0, 0, 0.5 )",
    },
    spinnerPageLight: {
        position: "fixed",
        top: 0,
        left: 0,
        width: "100%",
        height: "100%",
        display: "flex",
        justifyContent: "center",
        alignItems: "center",
        zIndex: 9999,
        backgroundColor: "rgba(255, 255, 255, 0.5)"
    }
});

export { useStyles };