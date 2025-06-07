import { useState, useEffect } from "react";
import {
    Card,
    LargeTitle,
    Text,
    Title1,
    Title2,
    Title3,
    Tree,
    TreeItem,
    TreeItemLayout,
} from "@fluentui/react-components";
import {
    AddSquareRegular,
    ArrowCircleRightRegular,
    SubtractSquareRegular,
} from "@fluentui/react-icons";

import { useStyles } from "./styles";

/**
 * @component DescriptionComponent
 * @description A comprehensive component that displays detailed information about the Internet Bulletin Board System (IBBS).
 * The component is structured with multiple sections including introduction, key features, usage instructions,
 * technical details, and upcoming features. Each section is organized in an interactive tree structure
 * using Fluent UI components for better user experience and navigation.
 *
 * @section Structure
 * - Introduction and What is IBBS: Basic information cards
 * - Key Features: Interactive tree with user management, content management, and administrative features
 * - How to Use: Step-by-step guide with getting started, browsing, creating content, and interaction instructions
 * - Technical Excellence: Details about architecture, infrastructure, design, and security
 * - Upcoming Features: Planned features categorized by user management, content management, and communication tools
 *
 * @state {string[]} openItems - Array of tree item IDs that are currently expanded
 * @state {Object} descriptionData - Object containing api values.
 *
 * @example
 * <DescriptionComponent />
 *
 * @requires @fluentui/react-components
 * @requires @fluentui/react-icons
 * @requires ./styles
 *
 * @returns {JSX.Element} A structured component displaying IBBS information
 */
function DescriptionComponent({ applicationDescriptionData }) {
    const styles = useStyles();

    const [openItems, setOpenItems] = useState([]);
    const [descriptionData, setDescriptionData] = useState({});

    useEffect(() => {
        if (
            applicationDescriptionData !== null &&
            applicationDescriptionData !== undefined &&
            Object.values(applicationDescriptionData).length > 0 &&
            applicationDescriptionData !== descriptionData
        ) {
            setDescriptionData(applicationDescriptionData);
        }
    }, [applicationDescriptionData]);

    /**
     * Handle the open change event for the tree items
     * @param {Event} _ The event object
     * @param {Object} data The data object containing open state and value
     */
    const handleOpenChange = (_, data) => {
        setOpenItems((curr) =>
            data.open
                ? [...curr, data.value]
                : curr.filter((value) => value !== data.value)
        );
    };

    /**
     * Handles the rendering of the tree sections.
     * @param {Object} data The data passed on
     * @param {string} sectionKey The section key name.
     * @param {number} startIndex The index of the tree item.
     * @returns {JSX.Element} The tree section rendered element.
     */
    const renderTreeSection = (data, sectionKey, startIndex) => {
        return (
            <Tree
                aria-label={sectionKey}
                className={sectionKey}
                openItems={openItems}
                onOpenChange={handleOpenChange}
            >
                <Card className={styles.treeCard}>
                    <TreeItem
                        itemType="branch"
                        value={`tree-item-${startIndex}`}
                    >
                        <TreeItemLayout
                            expandIcon={
                                openItems.includes(
                                    `tree-item-${startIndex}`
                                ) ? (
                                    <SubtractSquareRegular
                                        fontSize={30}
                                        className="mb-1"
                                    />
                                ) : (
                                    <AddSquareRegular
                                        fontSize={30}
                                        className="mb-1"
                                    />
                                )
                            }
                        >
                            <Title2 className={styles.titles}>
                                {data[sectionKey].title}
                            </Title2>
                        </TreeItemLayout>

                        <Tree>
                            {data[sectionKey].sections.map((section, index) => (
                                <TreeItem
                                    key={index}
                                    itemType="branch"
                                    value={`tree-item-${
                                        startIndex + index + 1
                                    }`}
                                >
                                    <TreeItemLayout
                                        expandIcon={
                                            openItems.includes(
                                                `tree-item-${
                                                    startIndex + index + 1
                                                }`
                                            ) ? (
                                                <SubtractSquareRegular
                                                    fontSize={25}
                                                />
                                            ) : (
                                                <AddSquareRegular
                                                    fontSize={25}
                                                />
                                            )
                                        }
                                    >
                                        <Title3>{section.title}</Title3>
                                    </TreeItemLayout>
                                    <Tree>
                                        <TreeItem itemType="leaf">
                                            <TreeItemLayout>
                                                {section.items ? (
                                                    <ul>
                                                        {section.items.map(
                                                            (
                                                                item,
                                                                itemIndex
                                                            ) => (
                                                                <li
                                                                    key={
                                                                        itemIndex
                                                                    }
                                                                    className={
                                                                        styles.listItem
                                                                    }
                                                                >
                                                                    <Text
                                                                        className={
                                                                            styles.textSize
                                                                        }
                                                                    >
                                                                        <ArrowCircleRightRegular />{" "}
                                                                        {item}
                                                                    </Text>
                                                                </li>
                                                            )
                                                        )}
                                                    </ul>
                                                ) : (
                                                    <Text
                                                        className={
                                                            styles.textSize
                                                        }
                                                    >
                                                        <ArrowCircleRightRegular />{" "}
                                                        {section.content}
                                                    </Text>
                                                )}
                                            </TreeItemLayout>
                                        </TreeItem>
                                    </Tree>
                                </TreeItem>
                            ))}
                        </Tree>
                    </TreeItem>
                </Card>
            </Tree>
        );
    };

    return (
        Object.values(descriptionData).length > 0 && (
            <div className={`row ${styles.descriptionContainer}`}>
                <LargeTitle className={styles.titles}>
                    {descriptionData.title}
                </LargeTitle>

                <div className="row">
                    <div className="col-6 col-sm-6">
                        <Card className={styles.card}>
                            <Title1 className={styles.introTitle}>
                                {descriptionData.introduction.title}
                            </Title1>
                            <p>{descriptionData.introduction.content}</p>
                        </Card>
                    </div>
                    <div className="col-6 col-sm-6">
                        <Card className={styles.card}>
                            <Title1 className={styles.introTitle}>
                                {descriptionData.whatIsIBBS.title}
                            </Title1>
                            <p>{descriptionData.whatIsIBBS.content}</p>
                        </Card>
                    </div>
                </div>

                <div className="row">
                    <div className="col-6 col-sm-6">
                        {renderTreeSection(descriptionData, "keyFeatures", 1)}
                    </div>
                    <div className="col-6 col-sm-6">
                        {renderTreeSection(descriptionData, "howToUse", 5)}
                    </div>
                </div>

                <div className="row">
                    <div className="col-6">
                        {renderTreeSection(
                            descriptionData,
                            "technicalExcellence",
                            10
                        )}
                    </div>
                    <div className="col-6">
                        {renderTreeSection(
                            descriptionData,
                            "upcomingFeatures",
                            13
                        )}
                    </div>
                </div>
            </div>
        )
    );
}

export default DescriptionComponent;
