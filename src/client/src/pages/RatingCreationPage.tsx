import React, {useState} from 'react';
import {Button} from "@mui/material";
import swal from 'sweetalert2'
import { useForm } from "react-hook-form";

interface User {
    username: string;
    userRating: string;
}

export const RatingCreationPage = ({username, userRating}: User) => {
    const [showModal, setShowModal] = useState(false);
    const [rating, setRating] = useState('');

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

    const handleCreateRating = () => {
        if (!rating) {
            alert("required");
            return;
        }
// Make an API call to create a new rating with the value specified in the input field
        setShowModal(false);
        setRating('');
    };
    
    const handleRating = () => {
        swal.fire(
            username,
            rating,
            'info'
        );
    }
    
    return (
        <>
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
                                    <br />
                                <input required={true}
                                       type="number" value={rating} onChange={handleRatingChange}/>
                                </label>
                                <Button onClick={handleCreateRating} color={"success"}>Create</Button>
                                <Button onClick={handleCloseModal} color={"error"}>Close</Button>
                            </form>
                        </div>
                    </div>
        </>
    );
};

//На странице “выбора игр” должно быть 2 кнопки: рейтинг и создание игр. 
// При нажатии на “рейтинг” должно показываться окно с рейтингом.
// Должен показываться список: username, рейтинг. 
// Возможность закрыть окно. (Можно не окно, а ваш вариант)
// При нажатии на “создание игры” должно показываться окно с обязательным полем для ввода
// “Макс. кол-во рейтинга?” и кнопка “Создать”.