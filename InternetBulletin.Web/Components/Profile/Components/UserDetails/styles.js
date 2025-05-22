import { makeStyles } from "@fluentui/react-components";

export const useStyles = makeStyles({
  card: {
    maxWidth: "100%",
    margin: "1rem",
    maxHeight: "300px",
  },
  detailsContainer: {
    width: "100%",
    display: "flex",
    justifyContent: "flex-start",
    alignItems: "center",
    paddingLeft: "5%",
  },
  profileSection: {
    display: "flex",
    alignItems: "center",
    gap: "2rem",
    maxWidth: "800px",
    width: "100%",
  },
  userInfo: {
    flex: 1,
    margin: "20px 0",
  },
  detailRow: {
    marginBottom: "1rem",
    "&:last-child": {
      marginBottom: 0,
    },
  },
  heading: {
    fontFamily: "Architects Daughter",
    textAlign: "center",
    display: "flex",
    justifyContent: "center",
    alignItems: "center",
    marginBottom: "10px",
  },
});
