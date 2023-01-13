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

export const GamePage = () => {
    const navigate = useNavigate();
    const [connection, setConnection] = useState<HubConnection>();
    const [userName, setUserName] = useState('');

    useEffect(() => {
        let jwtToken = localStorage.getItem("access_token") as string;
        console.log("jwtToken: " + jwtToken);
        if (jwtToken) {
            let decode = jwt_decode(jwtToken) as Claims;
            let username = decode["http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier"];
            setUserName(username);
        } else {
            navigate(`/authorization`, {replace: true});
        }
    }, [])

    useEffect(() => {
        const cnct = () => {
            setConnection(configureConnection());
        }
        cnct();
    }, [])

    const configureConnection = () => {
        const connection = new HubConnectionBuilder()
            .withUrl(process.env.RREACT_APP_ORIGIN_WEB_API + '/', {
                accessTokenFactory: () => localStorage.getItem("access_token") ?? ''
            })
            .build();


        return connection;
    }


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
        </>
    );
};


// Создатель комнаты должен просто сидеть и ждать когда кто-то подключится. 
// Подключившийся должен нажать на кнопку “присоединиться”. 
// Зашедший в комнату может подключиться только если его рейтинг подходит под настройки комнаты.
// Для всех остальных, кто находится в этой комнате должны не иметь возможности вмешиваться в игру. 
// Должны иметь возможность просматривать ход игры и писать в чате.  
// Победивший игрок должен получить в личный зачёт +3 очка рейтинга, проигравший -1 очко рейтинга.
// После окончания, должен быть отчёт в чате: Koyash(username) победил (выделить зеленым цветом).  
// В случае ничью очки не присуждаются.
// После окончания игра должна создаться автоматически через несколько секунд (кол-во секунд на ваш выбор).