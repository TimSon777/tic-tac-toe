import { Button } from '@mui/material';
import React, {useState} from 'react';
import CloseIcon from "@mui/icons-material/Close";
import PanoramaFishEyeIcon from '@mui/icons-material/PanoramaFishEye';
import {HubConnection} from "@aspnet/signalr";
import {Square} from "./square";
import {Game} from "./game-card";

interface BoardProps {
    squaresInRow: number;
    game: Game;
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

export const Board = ({squaresInRow}: BoardProps) => {

    const squaresCount = squaresInRow * squaresInRow;
    const [squares, setSquares] = useState(Array(squaresCount).fill(null));
    const [isX, setIsX] = useState(true);
    const [isDisabled, setIsDisabled] = useState(false);

    const handleClick = (i: number) => {
        squares[i] = isX ? 'X' : 'O';
        setSquares(squares);
        setIsX(!isX);
        setIsDisabled(true);
    }
//подписка на сигналр, что оппонент сходил, и нужно поменять isDisabled
// нужно инвокать в сигналр метод хода и нужно поменять isDisabled
// откуда получать isDisabled: props или запрос к бэку?
    let squaresRow = [];
    const rows = [];

    for (let j = 0; j < squaresInRow; j++) {
        squaresRow = [];

        for (let i = 0; i < squaresInRow; i++) {
            let index = squaresInRow * j + i;
            squaresRow.push(
                <Button
                    className={"square-button"}
                    onClick={() => handleClick(index)}
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

        rows.push(<div className={"squares-row"}>
            {squaresRow}
        </div>)
    }

    return (
        <div className={"board-container"}>
            {rows}
        </div>
    );
};
