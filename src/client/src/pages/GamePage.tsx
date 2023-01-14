import React, {useEffect, useState} from 'react';
import {Board} from "../components/board";
import {Alert, Button} from "@mui/material";
import {HubConnection, HubConnectionBuilder} from "@aspnet/signalr";
import jwt_decode from "jwt-decode";
import {Claims} from "./RatingCreationPage";
import {useNavigate} from "react-router-dom";
import axios from "axios";
import swal from "sweetalert2";

interface Game {
    sign: string;
    mateUsername: string;
    gameStatus: string;
    board: string[][];
}

export const GamePage = () => {
    
    const [userName, setUserName] = useState('');
    const [game, setGame] = useState<Game>();
    const navigate = useNavigate();
    const [isReady, setReady] = useState(false);
    const [connection, setConnection] = useState<HubConnection>();

    useEffect(() => {
        const cnct = async () => {
            setConnection(await configureConnection());
        }
        cnct();
    }, [])
    
    const configureConnection = async () => {
        const token = localStorage.getItem("access_token");
        const connection = new HubConnectionBuilder()
            .withUrl(process.env.REACT_APP_ORIGIN_WEB_API + '/gaming', {
                accessTokenFactory: () => token ?? ''
            })
            .build();

        await connection.start();
        setReady(true);
        return connection;
    }
    
    useEffect(() => {
        
        let jwtToken = localStorage.getItem("access_token") as string;
        console.log(userName + " " + localStorage.getItem("initiatorUserName"));

        if (jwtToken) {
            if (!isReady) {
                return;
            }
            
            console.log("Connection from GamePage: " + connection);
            connection!.on('IsConnected', (userName, sign) => {
                swal.fire(
                    userName,
                    sign,
                    'info'
                );

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
        
    }, [isReady])

    
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
        else {
            navigate(`/signup`, {replace: true});
        }
    }

    return (
        <>
            <div className={"tic-tac-toe-container"}>
                {game == undefined 
                    ? <></>
                    : <Board sign={game!.sign} squaresInRow={game!.board.length} connection={connection}/>
                }
            </div>
            <div className={"restart-button"}>
                <Button disabled={localStorage.getItem("initiatorUserName") === null 
                    || localStorage.getItem("initiatorUserName") === userName
                    || game?.gameStatus === "InProgress"}
                        onClick={handleJoin} color={"primary"} variant="outlined" fullWidth={true}>
                    JOIN
                </Button>
            </div>
        </>
    );
};
