import { useState } from "react";
import {
    Button,
    Dialog,
    DialogActions,
    DialogBody,
    DialogContent,
    DialogSurface,
    DialogTitle,
    DialogTrigger,
} from "@fluentui/react-components";
import { useNavigate } from "react-router-dom";

import {
    CreatePostPageConstants,
    HeaderPageConstants,
} from "@helpers/ibbs.constants";
import { useStyles } from "./styles";

/**
 * A React component that provides a confirmation dialog for canceling the current operation.
 * This component is typically used in forms or creation flows to confirm user's intention to cancel
 * and navigate away from the current page.
 * 
 * @component
 * @returns {JSX.Element} A button that triggers a confirmation dialog with options to confirm or cancel navigation
 * 
 * @example
 * <CancelModalComponent />
 */
function CancelModalComponent() {
    const styles = useStyles();
    const navigate = useNavigate();
    const { DialogContentConstants } = CreatePostPageConstants;

    const [isDialogOpen, setIsDialogOpen] = useState(false);

    /**
     * Handles the cancel click event to show confirmation dialog.
     * Sets the dialog state to open when the cancel button is clicked.
     */
    const handleCancelClick = () => {
        setIsDialogOpen(true);
    };

    /**
     * Handles the confirmation of cancellation and navigates to home page.
     * Closes the dialog and navigates to the home page when user confirms cancellation.
     */
    const handleConfirmCancel = () => {
        setIsDialogOpen(false);
        navigate(HeaderPageConstants.Headings.Home.Link);
    };

    return (
        <Dialog
            open={isDialogOpen}
            onOpenChange={(e, data) => setIsDialogOpen(data.open)}
        >
            <DialogTrigger>
                <Button
                    className={styles.cancelButton}
                    onClick={handleCancelClick}
                >
                    {"Cancel"}
                </Button>
            </DialogTrigger>
            <DialogSurface>
                <DialogBody>
                    <DialogTitle>
                        {DialogContentConstants.HeadingMessage}
                    </DialogTitle>
                    <DialogContent>
                        {DialogContentConstants.ConfirmationMessage}
                    </DialogContent>
                    <DialogActions>
                        <Button
                            className={styles.createButton}
                            onClick={() => setIsDialogOpen(false)}
                        >
                            {DialogContentConstants.NoButtonText}
                        </Button>
                        <Button
                            className={styles.cancelButton}
                            onClick={handleConfirmCancel}
                        >
                            {DialogContentConstants.YesButtonText}
                        </Button>
                    </DialogActions>
                </DialogBody>
            </DialogSurface>
        </Dialog>
    );
}

export default CancelModalComponent;
