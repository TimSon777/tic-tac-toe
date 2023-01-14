import React, {useState} from 'react';
import axios from "axios";
import {Button, FormGroup, Input, InputLabel} from "@mui/material";
import {SelectionPage} from "./SelectionPage";
import {useNavigate} from "react-router-dom";


interface AuthorizationState {
    userName: string;
    password: string;
}

export const AuthorizationPage = () => {
    const [authorizationState, setAuthorizationState] = useState<AuthorizationState>({
        userName: '',
        password: ''
    });

    const navigate = useNavigate();
    
    const [isAuthorized, setIsAuthorized] = useState(localStorage.getItem("access_token") !== "" &&
        localStorage.getItem("access_token") !== null);

    const handleChange = (event: React.ChangeEvent<HTMLInputElement>) => {
        const { name, value } = event.target;
        setAuthorizationState((prevState) => ({ ...prevState, [name]: value }));
    };

    const handleSubmit = async (event: React.FormEvent) => {
        event.preventDefault();
        
        if (!authorizationState.userName || !authorizationState.password) {
            return;
        }
        try {
            const response = await axios.post(process.env.REACT_APP_ORIGIN_WEB_API + '/signin', authorizationState);
            if (response.status === 200 ) {
                if (!response.data.succeeded) {
                    alert(response.data.errors);
                    return;
                }
                console.log(response.data.accessToken);
                localStorage.setItem("access_token", response.data.accessToken);
                setIsAuthorized(true);
            } else {
                alert("server error!")
            }
            
        } catch (error) {
            console.error(error);
        }
    };

    if (isAuthorized){
        return(<SelectionPage></SelectionPage>);
    }
    
    return (
        <>
            <form onSubmit={handleSubmit} className={"registration-form"}>
                <FormGroup className={"form-group-inputs"}>
                    <InputLabel htmlFor="userName">Username</InputLabel>
                    <Input
                        type="text"
                        name="userName"
                        value={authorizationState.userName}
                        onChange={handleChange}
                        id="username"
                        required
                    />

                    <InputLabel htmlFor="password">Password</InputLabel>
                    <Input
                        type="password"
                        name="password"
                        value={authorizationState.password}
                        onChange={handleChange}
                        id="password"
                        required
                    />
                </FormGroup>

                <Button type="submit">Submit</Button>
            </form>

            <Button onClick={() => {navigate(`/signup`, {replace: true})}}>Sign Up</Button>
        </>
    );
};
