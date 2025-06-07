import { useEffect, useState } from "react";
import {
	Card,
	Table,
	TableBody,
	TableCell,
	TableCellLayout,
	TableRow,
	Text,
	Title1,
} from "@fluentui/react-components";

import { useStyles } from "./styles";
import { MyProfilePageConstants } from "@helpers/ibbs.constants";

/**
 * UserDetailsComponent displays the user's personal information in a formatted layout.
 * It receives user details as props and renders them in a structured way.
 *
 * @component
 * @param {Object} props - Component props
 * @param {string} props.displayName - The user's display name
 * @param {string} props.emailAddress - The user's email address
 * @param {string} props.userName - The user's username
 *
 * @returns {JSX.Element} A formatted display of user details
 *
 * @example
 * // Usage in ProfileComponent
 * <UserDetailsComponent
 *   displayName="John Doe"
 *   emailAddress="john.doe@example.com"
 *   userName="johndoe"
 * />
 */
function UserDetailsComponent({ displayName, emailAddress, userName }) {
	const styles = useStyles();

	const [userDetailsData, setUserDetailsData] = useState({
		displayName: "",
		emailAddress: "",
		userName: "",
	});

	useEffect(() => {
		if (displayName !== "" && userDetailsData.displayName !== displayName) {
			setUserDetailsData((prevState) => ({
				...prevState,
				displayName: displayName,
			}));
		}

		if (
			emailAddress !== "" &&
			userDetailsData.emailAddress !== emailAddress
		) {
			setUserDetailsData((prevState) => ({
				...prevState,
				emailAddress: emailAddress,
			}));
		}

		if (userName !== "" && userDetailsData.userName !== userName) {
			setUserDetailsData((prevState) => ({
				...prevState,
				userName: userName,
			}));
		}
	}, [displayName, emailAddress, userName]);

	return (
		<Card className={styles.card} appearance="filled-alternative">
			<Title1 className={styles.heading}>
				{MyProfilePageConstants.Headings.AsPerMyKnowledge}
			</Title1>
			<div className={styles.scrollableItems}>
				<Table
					aria-label="User details"
					className={styles.detailsTable}
					size="medium"
				>
					<TableBody className="row">
						<TableRow>
							<TableCell className={styles.rowCell}>
								<TableCellLayout>
									<Text size={400} weight={"bold"}>
										Name
									</Text>
								</TableCellLayout>
							</TableCell>
							<TableCell>
								<TableCellLayout>
									<Text size={400}>
										{userDetailsData.displayName}
									</Text>
								</TableCellLayout>
							</TableCell>
						</TableRow>

						<TableRow>
							<TableCell className={styles.rowCell}>
								<TableCellLayout>
									<Text size={400} weight={"bold"}>
										Email
									</Text>
								</TableCellLayout>
							</TableCell>
							<TableCell>
								<TableCellLayout>
									<Text size={400}>
										{userDetailsData.emailAddress}
									</Text>
								</TableCellLayout>
							</TableCell>
						</TableRow>

						<TableRow>
							<TableCell className={styles.rowCell}>
								<TableCellLayout>
									<Text size={400} weight={"bold"}>
										UserName
									</Text>
								</TableCellLayout>
							</TableCell>
							<TableCell>
								<TableCellLayout>
									<Text size={400}>
										{userDetailsData.userName}
									</Text>
								</TableCellLayout>
							</TableCell>
						</TableRow>
					</TableBody>
				</Table>
			</div>
		</Card>
	);
}

export default UserDetailsComponent;
