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
}

export const GameCard = ({game}: GameCardProps) => {
    return (
        <div className={"game-card"}>
            <p>Id: {game.id}</p>
            <p>Creator: {game.userName}</p>
            <p>Creation date: {game.createdDateTimeUtc}</p>
            {game.isAvailableToJoin && 
                <Button variant={"outlined"} color={"primary"}>
                    Enter
                </Button>
            }
            
        </div>
    );
};
