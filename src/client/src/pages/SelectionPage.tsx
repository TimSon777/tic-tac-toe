import React, {useContext, useEffect} from 'react';
import {GameCard} from "../components/game-card";

export const SelectionPage = () => {
    
   /* const { isAuthenticated } = useContext(AuthContext);
    const history = useHistory();

    useEffect(() => {
        if (!isAuthenticated) {
            history.push('/login');
        }
    }, [isAuthenticated, history]);*/
    // Загрузка списка игр должна быть реализована через ленивую пагинацию.
    // Список сортируется по дате создания и по статусу (начата или нет). 
    // Т.е. сначала идут новые и не начатые игры, а уже потом начатые. 
    return (
        <>
            <h1 className={"select-game-header"}>Select game</h1>
            <div className={"selection-container"}>
                <GameCard gameId={'1'} username={'dsf'} creationDate={'11.02.2022'} isStarted={true}></GameCard>
            </div>
        </>
    );
};
