import React, {useEffect, useState} from 'react';
import {Board} from "../components/board";
import {Alert, Button} from "@mui/material";
import {HubConnection, HubConnectionBuilder} from "@aspnet/signalr";
import jwt_decode from "jwt-decode";
import {Claims} from "./RatingCreationPage";
import {useNavigate} from "react-router-dom";
import axios from "axios";

interface Game {
    sign: string;
    mateUsername: string;
    gameStatus: string;
    board: string[][];
}

interface GamePageProps {
    connection: HubConnection | undefined;
}

export const GamePage = ({connection}: GamePageProps) => {
    
    const [userName, setUserName] = useState('');
    const [game, setGame] = useState<Game>();
    const navigate = useNavigate();

    useEffect(() => {
        let jwtToken = localStorage.getItem("access_token") as string;
        console.log(userName + " " + localStorage.getItem("initiatorUserName"));

        if (jwtToken) {

            connection!.on('IsConnected', (userName, sign) => {
                alert(userName + " " + sign);

                axios.get<Game>(process.env.REACT_APP_ORIGIN_WEB_API + '/games',
                    {
                        headers: {
                            'Authorization': `Bearer ${localStorage.getItem('access_token')}`
                        }
                    })
                    .then(response => {
                        console.log(response.data);
                        setGame(response.data);
                    })
            });
            
            let decode = jwt_decode(jwtToken) as Claims;
            let username = decode["http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier"];

            const initiatorUserName = localStorage.getItem("initiatorUserName")
            
            if (initiatorUserName != null){
                return;
            }
            
            setUserName(username);
            
        } else {
             navigate(`/signup`, {replace: true});
        }
        
    }, [])

// Победивший игрок должен получить в личный зачёт +3 очка рейтинга, проигравший -1 очко рейтинга.
    let rating: number = 0;
    
    const handleJoin = () => {
        let jwtToken = localStorage.getItem("access_token") as string;
        if (jwtToken) {
            let decode = jwt_decode(jwtToken) as Claims;
            let username = decode["http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier"];
            setUserName(username);

            const initiatorUserName = localStorage.getItem("initiatorUserName");
            
            axios.defaults.headers.common['Authorization'] = `Bearer ${jwtToken}`;
            
            axios.post<boolean>(process.env.REACT_APP_ORIGIN_WEB_API + '/game/join',
                {
                    initiatorUserName: initiatorUserName
                })
                .then(response => {
                    if (!response.data) {
                        alert("Can not join to game");
                        return;
                    }

                    connection!.invoke("StartGame").then();
                });
        }
    }

    return (
        <>
            <p>Rating: {rating.toString()}</p>
            <div className={"tic-tac-toe-container"}>
                {game == undefined 
                    ? <></>
                    : <Board sign={game!.sign} squaresInRow={game!.board.length} connection={connection}/>
                }
            </div>
            <div className={"restart-button"}>
                <Button disabled={localStorage.getItem("initiatorUserName") === null || localStorage.getItem("initiatorUserName") === userName}
                        onClick={handleJoin} color={"primary"} variant="outlined" fullWidth={true}>
                    JOIN
                </Button>
            </div>

            <Button style={{marginTop: "2rem"}} variant={"outlined"} fullWidth={true} color={"secondary"} onClick={() => {navigate(`/selection`, {replace: true});}}>
                To selection
            </Button>
            
            {/*<Alert */}
            {/*    // style={{display: "none"}} */}
            {/*    severity="success">User {userName} win!</Alert>*/}
        </>
    );
};
//показывать алерт когда приходит уведомление signalr о победе