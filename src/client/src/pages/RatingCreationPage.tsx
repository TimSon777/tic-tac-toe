import React, {useEffect, useState} from 'react';
import {Button} from "@mui/material";
import swal from 'sweetalert2'
import jwt_decode from "jwt-decode";
import {useNavigate} from "react-router-dom";
import axios from "axios";
import {HubConnection} from "@aspnet/signalr";

interface User {
    userName: string;
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
    const [rating, setRating] = useState('');
    const [userName, setUserName] = useState('');
    const navigate = useNavigate();

    useEffect(() => {
        let jwtToken = localStorage.getItem("access_token") as string;
        if (jwtToken) {
            let decode = jwt_decode(jwtToken) as Claims;
            let username = decode["http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier"];
            setUserName(username);
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
                        try {
                            connection!.start().then(() => {
                                connection!.on('IsConnected', (userName) => {
                                    alert(userName);
                                });
                            });
                        } catch (err) {
                            console.log(err);
                        }
                    } else {
                        alert("false");
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
            'rating',
            'info'
        );
    }
    
    const handleLogOut = () => {
      localStorage.removeItem('access_token');
        navigate(`/signup`, {replace: true});
    }

    return (
        <>
            <Button onClick={() => navigate('/selection', {replace: true})} color={"secondary"}>
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

            <Button style={{marginTop: "3rem"}} variant={"outlined"} onClick={handleLogOut} color={"error"}>
                Log out
            </Button>
        </>
    );
};