import React from 'react';
import './App.css';
import {createBrowserRouter, RouterProvider} from "react-router-dom";
import {RegistrationPage} from "./pages/RegistrationPage";
import {AuthorizationPage} from "./pages/AuthorizationPage";
import {SelectionPage} from "./pages/SelectionPage";
import {GamePage} from "./pages/GamePage";
import {RatingCreationPage} from "./pages/RatingCreationPage";


const App = () => {
    
    const router = createBrowserRouter([
        { path: '/signup', element: <RegistrationPage /> },
        { path: '/signin', element: <AuthorizationPage /> },
        { path: "/selection", element: <SelectionPage/>},
        { path: "/game/:id", element: <GamePage/>},
        { path: "/", element: <RatingCreationPage/>},

    ])
    
    return (
        <RouterProvider router={router} />
    )
}

export default App;
