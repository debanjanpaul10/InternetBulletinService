import React from "react";
import ReactDOM from "react-dom/client";
import { BrowserRouter as Router } from "react-router-dom";
import { configureStore } from "@reduxjs/toolkit";
import { Provider } from "react-redux";
import "font-awesome/css/font-awesome.min.css";
import "bootstrap/dist/css/bootstrap.css";
import "@fontsource/architects-daughter";
import { MsalProvider } from "@azure/msal-react";
import { PublicClientApplication } from "@azure/msal-browser";

import "@styles/App.css";
import "@styles/App_Dark.css";
import { PostsReducer } from "@store/Posts/Reducers";
import { CommonReducer } from "@store/Common/Reducers";
import { msalConfig } from "@services/auth.config";
import { UserReducer } from "@store/Users/Reducer";
import Main from "@components/Main";

/**
 * Configures the redux store.
 */
const store = configureStore({
	reducer: {
		PostsReducer: PostsReducer,
		CommonReducer: CommonReducer,
		UserReducer: UserReducer,
	},
});

const root = ReactDOM.createRoot(document.getElementById("root"));
const msalInstance = new PublicClientApplication(msalConfig);

root.render(
	<MsalProvider instance={msalInstance}>
		<Router>
			<Provider store={store}>
				<Main msalInstance={msalInstance} />
			</Provider>
		</Router>
	</MsalProvider>
);
