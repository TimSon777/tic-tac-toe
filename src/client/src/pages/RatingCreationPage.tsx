import React, {useEffect, useState} from 'react';
import {Button} from "@mui/material";
import swal from 'sweetalert2'
import jwt_decode from "jwt-decode";
import {useNavigate} from "react-router-dom";
import axios from "axios";
import {HubConnection} from "@aspnet/signalr";
import {GameCard} from "../components/game-card";

interface User {
    userName: string;
}

interface UserRating {
    userName: string;
    rating: number;
}

interface UserRatings {
    userRatings: UserRating[];
}

export interface Claims {
    "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier": string;
}

interface Response {
    isCreated: boolean;
}


interface RatingCreationPageProps {
    connection: HubConnection | undefined;
}

export const RatingCreationPage = ({connection}: RatingCreationPageProps) => {
    const [showModal, setShowModal] = useState(false);
    const [showRating, setShowRating] = useState(false);
    const [rating, setRating] = useState('');
    const [userRating, setUserRating] = useState(0);
    const [userName, setUserName] = useState('');
    const navigate = useNavigate();
    const [ratings, setRatings] = useState<UserRating[]>();

    useEffect(() => {
        let jwtToken = localStorage.getItem("access_token") as string;
        if (jwtToken) {
            let decode = jwt_decode(jwtToken) as Claims;
            let username = decode["http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier"];
            setUserName(username);
            
            axios.get<UserRatings>(process.env.REACT_APP_ORIGIN_WEB_API + `/rating?ItemsNumber=${10}`,
                {
                    headers: {
                        'Authorization': `Bearer ${localStorage.getItem('access_token')}`
                    }
                })
                .then(response => {
                    let rats = response.data.userRatings;
                    setRatings(rats);
                    let userRat = response.data.userRatings.find(rating => rating.userName === userName);
                    if (userRat) {
                        setUserRating(userRat.rating);
                    }
                });
        } else {
            navigate(`/signup`, {replace: true});
        }
    }, [])

    const handleOpenModal = () => {
        setShowModal(true);
    };

    const handleCloseModal = () => {
        setShowModal(false);
        setRating('');
    };

    const handleRatingChange = (event: React.ChangeEvent<HTMLInputElement>) => {
        setRating(event.target.value);
    };

    const handleCreateRating = async () => {
        if (!rating) {
            alert("required");
            return;
        }

        try {
            const token = localStorage.getItem('access_token');

            axios.defaults.headers.common['Authorization'] = `Bearer ${token}`;

            axios.post<Response>(process.env.REACT_APP_ORIGIN_WEB_API + '/game', {})
                .then(response => {
                    if (response.data.isCreated) {
                        navigate('/game', {replace: true});
                            
                    } else {
                        alert("Game is already created");
                    }
                });

        } catch (error) {
            console.error(error);
        }

        setShowModal(false);
        setRating('');
    };

    const handleRating = () => {
        swal.fire(
            userName,
            userRating.toString(),
            'info'
        );
    }
    
    const handleRatings = () => {
        setShowRating(!showRating);
    }
    
    const handleLogOut = () => {
      localStorage.removeItem('access_token');
        navigate(`/signup`, {replace: true});
    }

    return (
        <>
            <Button onClick={() => navigate('/selection', {replace: true})} color={"secondary"} variant={"outlined"}>
                To selecion
            </Button>
            <h1 className={"choose-game-header"}>Choose game</h1>
            <div className={"rating-creation-container"}>

                <Button variant={"outlined"} color={"secondary"} onClick={handleOpenModal}>
                    Create game
                </Button>
                <Button onClick={handleRating} variant={"outlined"} color={"info"}>
                    Rating
                </Button>
                <Button onClick={handleRatings} variant={"outlined"} color={"info"}>
                    Ratings
                </Button>
            </div>

            <div className={showModal ? "modal active" : "modal"}>
                <div className={showModal ? "modal-content active" : "modal-content"}>
                    <form>
                        <label>
                            Max rating?
                            <br/>
                            <input required={true}
                                   type="number" value={rating} onChange={handleRatingChange}/>
                        </label>
                        <Button onClick={handleCreateRating} color={"success"}>Create</Button>
                        <Button onClick={handleCloseModal} color={"error"}>Close</Button>
                    </form>
                </div>
            </div>

            {ratings && 
                <div className={showRating ? "rating active" : "rating"}>
                <div className={showRating ? "rating-content active" : "rating-content"}>
                    {ratings.map((item) =>
                        <p>Usser name: {item.userName} : Rating: {item.rating}</p>
                    )}
                </div>
            </div>
            }
            
            <Button style={{marginTop: "3rem"}} variant={"outlined"} onClick={handleLogOut} color={"error"}>
                Log out
            </Button>
        </>
    );
};