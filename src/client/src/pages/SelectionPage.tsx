import React, {useEffect, useState} from 'react';
import {Game, GameCard} from "../components/game-card";
import InfiniteScroll from 'react-infinite-scroller';
import axios from "axios";
import {useNavigate} from "react-router-dom";
import jwt_decode from "jwt-decode";
import {Claims} from "./RatingCreationPage";
import {Button} from "@mui/material";


interface GameList {
    games: Game[];
    hasMore: boolean;
    totalCount: number;
}

export const SelectionPage = () => {
    const loadMoreData = async (page: number) => {
        try {
            const response = await axios.get<GameList>(process.env.REACT_APP_ORIGIN_WEB_API
                + `/games/current?ItemsCount=${3}&PageNumber=${page}`,
                {
                    headers: {
                        'Authorization': `Bearer ${localStorage.getItem('access_token')}`
                    }
                })
            
            console.log(response.data.games);

            setItems((items) => [...items, ...response.data.games]);
            setHasMore(response.data.hasMore);
        } catch (error) {
            console.error(error);
        }
    }
    
    const [items, setItems] = useState<Game[]>([]);
    const [hasMore, setHasMore] = useState(true);

    const [userName, setUserName] = useState('');
    const navigate = useNavigate();
    
    
    useEffect(() => {
        let jwtToken = localStorage.getItem("access_token") as string;
        console.log("jwtToken: " + jwtToken);
        if (jwtToken) {
            let decode = jwt_decode(jwtToken) as Claims;
            let username = decode["http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier"];
            setUserName(username);
        } else {
            navigate(`/signup`, {replace: true});
        }

    }, []);


    const handleClick = () => {
        navigate(`/`, {replace: true});
    }

    const handleLogOut = () => {
        localStorage.removeItem('access_token');
        navigate(`/signup`, {replace: true});
    }


    const loader = (
        <div key="loader" className="loader">
            Loading ...
        </div>
    );

    return (
        <>
            <h1 className={"select-game-header"}>Select game</h1>
            <Button onClick={handleClick} variant={"outlined"}>Rating and Creation</Button>
            <div className={"selection-container"}>
                <InfiniteScroll
                    pageStart={0}
                    loadMore={loadMoreData}
                    hasMore={hasMore}
                    loader={loader}
                >
                    <div className={"selection-container"}>
                        {items.map((item) =>
                            <GameCard game={item} userName={userName} key={item.id}/>
                        )}
                    </div>
                </InfiniteScroll>
            </div>
            <Button style={{marginTop: "3rem"}} variant={"outlined"} onClick={handleLogOut} color={"error"}>
                Log out
            </Button>
        </>
    );
};