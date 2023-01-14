import React, {useEffect, useState} from 'react';
import './App.css';
import {createBrowserRouter, RouterProvider} from "react-router-dom";
import {RegistrationPage} from "./pages/RegistrationPage";
import {AuthorizationPage} from "./pages/AuthorizationPage";
import {SelectionPage} from "./pages/SelectionPage";
import {GamePage} from "./pages/GamePage";
import {RatingCreationPage} from "./pages/RatingCreationPage";
import {HubConnection, HubConnectionBuilder} from "@aspnet/signalr";


const App = () => {
    


    const configureConnection = async () => {
        const token = localStorage.getItem("access_token");
        const connection = new HubConnectionBuilder()
            .withUrl(process.env.REACT_APP_ORIGIN_WEB_API + '/gaming', {
                accessTokenFactory: () => token ?? ''
            })
            .build();

        await connection.start();

        return connection;
    }
    
    const router = createBrowserRouter([
        { path: "/", element: <RatingCreationPage />},
        { path: '/signup', element: <RegistrationPage /> },
        { path: '/authorization', element: <AuthorizationPage /> },
        { path: "/selection", element: <SelectionPage/>},
        { path: "/game", element: <GamePage />}
    ])
    
    return (
        <RouterProvider router={router} />
    )
}

export default App;
