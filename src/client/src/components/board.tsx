import { Button } from '@mui/material';
import React, {useEffect, useState} from 'react';
import CloseIcon from "@mui/icons-material/Close";
import PanoramaFishEyeIcon from '@mui/icons-material/PanoramaFishEye';
import {HubConnection} from "@aspnet/signalr";
import {Square} from "./square";
import {Game} from "./game-card";
import {useNavigate} from "react-router-dom";

interface BoardProps {
    squaresInRow: number;
    sign: string;
    connection: HubConnection | undefined;
}

// Создатель комнаты должен просто сидеть и ждать когда кто-то подключится. 
// Подключившийся должен нажать на кнопку “присоединиться”. 
// Зашедший в комнату может подключиться только если его рейтинг подходит под настройки комнаты.
// Для всех остальных, кто находится в этой комнате должны не иметь возможности вмешиваться в игру. 
// Должны иметь возможность просматривать ход игры и писать в чате.  
// Победивший игрок должен получить в личный зачёт +3 очка рейтинга, проигравший -1 очко рейтинга.
// После окончания, должен быть отчёт в чате: Koyash(username) победил (выделить зеленым цветом).  
// В случае ничью очки не присуждаются.
// После окончания игра должна создаться автоматически через несколько секунд (кол-во секунд на ваш выбор).

export const Board = ({squaresInRow, sign, connection}: BoardProps) => {

    const squaresCount = squaresInRow * squaresInRow;
    const [squares, setSquares] = useState(Array(squaresCount).fill(null));
    const [isDisabled, setIsDisabled] = useState(sign !== 'X');

    const navigate = useNavigate();
    
    useEffect(() => {
        connection!.on('MateMoved', (y, x, errors : string) => {
            const index = squaresInRow * y + x;
            squares[index] = sign === "X" ? "O" : "X";
            setSquares(squares);
            setIsDisabled(false);
        });
    }, [])
    
    useEffect(() => {
        connection!.on('GameOver', (gameStatus : string) => {
            if (gameStatus === "CrossWin"){
                if (sign === "X"){
                    alert("Вы победили, вам +3 очка");
                }
                else{
                    alert("Вы проиграли, вам -1 очко");
                }
                navigate('/selection', {replace: true});
            }

            if (gameStatus === "NoughtWin"){
                if (sign === "O"){
                    alert("Вы победили, вам +3 очка");
                }
                else{
                    alert("Вы проиграли, вам -1 очко");
                }
                navigate('/selection', {replace: true});
            }

            if (gameStatus === "Draw"){
                alert("Ничья");
                navigate('/selection', {replace: true});
            }
        });
    }, [])
    
    const handleClick = (x: number, y: number, index: number) => {
        if (squares[index] == null) {
            squares[index] = sign;
            connection!.invoke("Move", x, y).then(() => {
                setSquares(squares);
                setIsDisabled(true);
            });
        }
    }
    let squaresRow = [];
    const rows = [];

    for (let j = 0; j < squaresInRow; j++) {
        squaresRow = [];
        for (let i = 0; i < squaresInRow; i++) {
            const index = squaresInRow * j + i;
            squaresRow.push(
                <Button
                    key={index}
                    className={"square-button"}
                    onClick={() => handleClick(j, i, index)}
                    variant="outlined"
                    sx={{
                        width: 64,
                        height: 64,
                        borderRadius: 0,
                        border: "1px solid",
                        "& .MuiButton-startIcon": { margin: 0 },
                        color: "inherit",
                        "&:hover": {borderColor: "aqua"}
                    }}
                    disabled={isDisabled}
                >
                    {squares[index]}
                </Button>
                );
        }

        rows.push(<div key={(Math.random())} className={"squares-row"}>
            {squaresRow}
        </div>)
    }

    return (
        <div className={"board-container"}>
            {rows}
        </div>
    );
};
