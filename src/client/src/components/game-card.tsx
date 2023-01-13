import React from 'react';
import {Button} from "@mui/material";

export interface Game {
    userName: string;
    id: number;
    createdDateTimeUtc: string;
    isAvailableToJoin: boolean;
}

interface GameCardProps {
    game: Game;
    userName: string;
}

export const GameCard = ({game, userName}: GameCardProps) => {
    return (
        <div className={"game-card"}>
            <p>Id: {game.id}</p>
            <p>Creator: {game.userName}</p>
            <p>Creation date: {game.createdDateTimeUtc}</p>
            {game.isAvailableToJoin && userName !== game.userName && 
                <Button variant={"outlined"} color={"primary"}>
                    Enter
                </Button>
            }
            
        </div>
    );
};
