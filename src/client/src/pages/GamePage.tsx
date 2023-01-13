import React, {useEffect, useState} from 'react';
import {Board} from "../components/board";
import {Alert, Button} from "@mui/material";


interface User {
    rating: number; 
    username: string;
}

export const GamePage = () => {
// Победивший игрок должен получить в личный зачёт +3 очка рейтинга, проигравший -1 очко рейтинга.
    let rating: number = 0;
    let user: User = {
        rating: 0,
        username: 'name'
    };

    const handleJoin = () => {
      if (user.rating >= rating) {
          
      }
      else {
          return;
      }
    }
    
    
    return (
        <>
            <p>Rating: {rating.toString()}</p>
            <div className={"tic-tac-toe-container"}>
                <Board squaresInRow={3}></Board>
            </div>
            <div className={"restart-button"} >
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