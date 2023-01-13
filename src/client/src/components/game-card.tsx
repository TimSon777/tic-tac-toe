import React from 'react';
import {Button} from "@mui/material";

interface GameInfo {
    gameId: string;
    username: string;
    creationDate: string;
    isStarted: boolean;
}

export const GameCard = ({gameId, username, creationDate, isStarted} : GameInfo) => {
    return (
        <div className={"game-card"}>
            <p>Id: {gameId}</p>
            <p>Creator: {username}</p>
            <p>Creation date: {creationDate}</p>
            <Button variant={"outlined"} color={"primary"}>
                Enter
            </Button>
        </div>
    );
};
