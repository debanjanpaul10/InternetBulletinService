import { makeStyles, tokens } from "@fluentui/react-components";

const useStyles = makeStyles({
    aboutUsHeading: {
        fontFamily: "Architects Daughter",
        textAlign: "center",
        display: "flex",
        justifyContent: "center",
        alignItems: "center",
    },
    carouselNavButton: {
        backgroundColor: tokens.colorNeutralBackground1,
        marginTop: "-15px",
    },
    subHeading: {
        textAlign: "center",
        display: "flex",
        justifyContent: "center",
        alignItems: "center",
        marginTop: "10px",
    },
    container: {
        maxHeight: "90vh",
        overflow: "hidden",
    },
    carouselContainer: {
        maxHeight: "60vh",
        overflow: "hidden",
    },
    aboutUsContainer: {
        height: "100vh",
        overflow: "auto",
        display: "flex",
        flexDirection: "column",
    },
    heart: {
        color: tokens.colorStatusDangerBackground3,
    },
});

export { useStyles };
