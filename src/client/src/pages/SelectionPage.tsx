import React, {useEffect, useState} from 'react';
import {Game, GameCard} from "../components/game-card";
import InfiniteScroll from 'react-infinite-scroller';
import axios from "axios";


interface GameList {
    games: Game[];
    hasMore: boolean;
    totalCount: number;
}

//http://localhost:5087/games/current?ItemsCount=2&PageNumber=1
export const SelectionPage = () => {
    const loadMoreData = (page: number) => {
        axios.get<GameList>(process.env.RREACT_APP_ORIGIN_WEB_API
            + `/games/current?ItemsCount=${3}&PageNumber=${page}`,
            {
                headers: {
                    'Authorization': `Bearer ${localStorage.getItem('access_token')}`
                }
            })
            .then(value => {
                setHasMore(value.data.hasMore);
                setItems(value.data.games);
            });
    }
    

    const [items, setItems] = useState<Game[]>([]);
    const [hasMore, setHasMore] = useState(false);

    
    useEffect(() => {
        const fetchItems = async () => {
            try {
                await axios.get<GameList>(process.env.RREACT_APP_ORIGIN_WEB_API
                    + `/games/current?ItemsCount=${3}&PageNumber=${1}`,
                    {
                        headers: {
                            'Authorization': `Bearer ${localStorage.getItem('access_token')}`
                        }
                    }).then(value => {
                    setHasMore(value.data.hasMore);
                    setItems(value.data.games);
                });
            } catch (error) {
                console.error(error);
            }
        };
        
        fetchItems();
    }, []);
    

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
                    pageStart={1}
                    loadMore={loadMoreData}
                    hasMore={hasMore}
                    loader={loader}
                >
                    <div className={"selection-container"}>
                        {items.map((item) =>
                            <GameCard game={item}></GameCard>
                        )}
                        
                    </div>
                </InfiniteScroll>

            </div>
        </>
    );
};
/*    NotStarted = 0,
    InProgress = 1,
    CrossWin = 2,
    NoughtWin = 3,
    Draw = 4,
    Canceled = 5*/