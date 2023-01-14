import React, {useEffect, useState} from 'react';
import {Board} from "../components/board";
import {Alert, Button} from "@mui/material";
import {HubConnection, HubConnectionBuilder} from "@aspnet/signalr";
import jwt_decode from "jwt-decode";
import {Claims} from "./RatingCreationPage";
import {useNavigate} from "react-router-dom";
import {Game} from "../components/game-card";
import axios from "axios";


interface GamePageProps {
    connection: HubConnection | undefined;
}

export const GamePage = ({connection}: GamePageProps) => {
    
    const [userName, setUserName] = useState('');
    const [game, setGame] = useState<Game>();
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
        
        axios.post<Game>(process.env.REACT_APP_ORIGIN_WEB_API + '/FOR_GAME', {})
            .then(response => {
                setGame(response.data);
            });
    }, [])

// Победивший игрок должен получить в личный зачёт +3 очка рейтинга, проигравший -1 очко рейтинга.
    let rating: number = 0;
    
    const handleJoin = () => {
        if (0) {
            
        } else {
            return;
        }
    }

    return (
        <>
            <p>Rating: {rating.toString()}</p>
            <div className={"tic-tac-toe-container"}>
                <Board game={game!} squaresInRow={3}></Board>
            </div>
            <div className={"restart-button"}>
                <Button onClick={handleJoin} color={"inherit"} variant="outlined" fullWidth={true}>
                    JOIN
                </Button>
            </div>

            <Button style={{marginTop: "2rem"}} variant={"outlined"} fullWidth={true} color={"secondary"} onClick={() => {navigate(`/selection`, {replace: true});}}>
                To selection
            </Button>

            
            <Alert style={{display: "none"}} severity="success">User {userName} win!</Alert>
        </>
    );
};
//показывать алерт когда приходит уведомление signalr о победе