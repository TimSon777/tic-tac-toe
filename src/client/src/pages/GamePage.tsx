import React, {useEffect, useState} from 'react';
import {Board} from "../components/board";
import {Alert, Button} from "@mui/material";
import {HubConnection, HubConnectionBuilder} from "@aspnet/signalr";
import jwt_decode from "jwt-decode";
import {Claims} from "./RatingCreationPage";
import {useNavigate} from "react-router-dom";


interface User {
    rating: number;
    username: string;
}

interface GamePageProps {
    connection: HubConnection | undefined;
}

export const GamePage = ({connection}: GamePageProps) => {
    
    const [userName, setUserName] = useState('');
    const navigate = useNavigate();

    useEffect(() => {
        let jwtToken = localStorage.getItem("access_token") as string;
        console.log("jwtToken: " + jwtToken);
        if (jwtToken) {
            let decode = jwt_decode(jwtToken) as Claims;
            let username = decode["http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier"];
            setUserName(username);
        } else {
            navigate(`/signup`, {replace: true});
        }
    }, [])

// Победивший игрок должен получить в личный зачёт +3 очка рейтинга, проигравший -1 очко рейтинга.
    let rating: number = 0;
    let user: User = {
        rating: 0,
        username: 'name'
    };

    const handleJoin = () => {
        if (user.rating >= rating) {
            
        } else {
            return;
        }
    }

    return (
        <>
           
            <p>Rating: {rating.toString()}</p>
            <div className={"tic-tac-toe-container"}>
                <Board squaresInRow={3}></Board>
            </div>
            <div className={"restart-button"}>
                <Button onClick={handleJoin} color={"inherit"} variant="outlined" fullWidth={true}>
                    JOIN
                </Button>
            </div>

            <Alert severity="success">User {user.username} win!</Alert>

            <Button variant={"outlined"} fullWidth={true} color={"secondary"} onClick={() => {navigate(`/selection`, {replace: true});}}>
                To selection
            </Button>

        </>
    );
};