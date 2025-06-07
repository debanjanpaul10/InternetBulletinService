import React, { useEffect, useState } from "react";
import { useSelector } from "react-redux";

import PostBody from "@components/Posts/Components/PostBody";
import NoPostsContainer from "@components/Posts/Components/NoPosts";

/**
 * @component
 * PostsContainer component to display a list of posts.
 *
 * @returns {JSX.Element} The posts container jsx element.
 */
function PostsContainer() {
	const AllPostsStoreData = useSelector(
		(state) => state.PostsReducer.allPostsData
	);
	const IsPostsDataLoading = useSelector(
		(state) => state.PostsReducer.isPostsDataLoading
	);

	const [allPosts, setAllPosts] = useState([]);

	useEffect(() => {
		if (allPosts !== AllPostsStoreData) {
			let sortedData = [...AllPostsStoreData].sort(
				(a, b) =>
					new Date(b.postCreatedDate) - new Date(a.postCreatedDate)
			);
			setAllPosts(sortedData);
		}
	}, [AllPostsStoreData]);

	if (IsPostsDataLoading) {
		return <div className="container"></div>;
	}

	return (
		<div className="container">
			{allPosts.length > 0 ? (
				<div className="row">
					{allPosts.map((post, index) => (
						<div className="col-md-6 mb-4" key={index}>
							<PostBody post={post} />
						</div>
					))}
				</div>
			) : (
				<NoPostsContainer />
			)}
		</div>
	);
}

export default PostsContainer;
