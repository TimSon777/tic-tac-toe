import React, {useCallback, useContext, useEffect, useState} from 'react';
import {GameCard} from "../components/game-card";
import InfiniteScroll from 'react-infinite-scroller';


export const SelectionPage = () => {
    const loadMoreData = (page: any) => {
        // Здесь должен быть код для загрузки данных
        // Используйте page для определения текущей страницы
    }

    const hasMoreData = true;
    
    const [items, setItems] = useState([]);

    const loader = (
        <div key="loader" className="loader">
            Loading ...
        </div>
    );

    return (
        <>
            <h1 className={"select-game-header"}>Select game</h1>
            <div className={"selection-container"}>
                <InfiniteScroll
                    pageStart={0}
                    loadMore={loadMoreData}
                    hasMore={hasMoreData}
                    loader={loader}
                >
                    <div className={"selection-container"}>
                        <GameCard gameId={'1'} username={'dsf'} creationDate={'11.02.2022'} isStarted={true}></GameCard>
                    </div>
                </InfiniteScroll>

            </div>
        </>
    );
};
