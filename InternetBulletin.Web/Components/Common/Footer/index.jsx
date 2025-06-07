import { Button } from "@fluentui/react-components";
import { MailRegular } from "@fluentui/react-icons";
import {
	faFacebook,
	faGithub,
	faLinkedin,
} from "@fortawesome/free-brands-svg-icons";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";

import { FooterConstants } from "@helpers/ibbs.constants";

/**
 * FooterComponent - A React component that renders a footer section with social media and contact links.
 *
 * The component displays a row of buttons that link to:
 * - Email contact
 * - Facebook profile
 * - GitHub repository
 * - LinkedIn profile
 *
 * Each button uses either Fluent UI icons or FontAwesome icons and opens external links in a new tab.
 * The links are configured through the FooterConstants helper.
 *
 * @returns {JSX.Element} A div containing social media and contact buttons
 */
function FooterComponent() {
	const { EmailLink, FaceBookLink, GithubLink, LinkedinLink } =
		FooterConstants;

	return (
		<div className="row mt-4 mb-4">
			<div className="col-12 d-flex justify-content-center gap-3">
				<Button
					as="a"
					appearance="subtle"
					href={EmailLink}
					icon={<MailRegular />}
				/>
				<Button
					icon={<FontAwesomeIcon icon={faFacebook} />}
					appearance="subtle"
					as="a"
					href={FaceBookLink}
					target="_blank"
				/>
				<Button
					icon={<FontAwesomeIcon icon={faGithub} />}
					appearance="subtle"
					as="a"
					href={GithubLink}
					target="_blank"
				/>
				<Button
					icon={<FontAwesomeIcon icon={faLinkedin} />}
					appearance="subtle"
					as="a"
					href={LinkedinLink}
					target="_blank"
				/>
			</div>
		</div>
	);
}

export default FooterComponent;
