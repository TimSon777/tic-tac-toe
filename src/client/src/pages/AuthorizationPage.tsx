import React, {useState} from 'react';
import axios from "axios";
import {Button} from "@mui/material";


interface AuthorizationState {
    username: string;
    password: string;
}

export const AuthorizationPage = () => {
    const [authorizationState, setAuthorizationState] = useState<AuthorizationState>({
        username: '',
        password: '',
    });

    const handleChange = (event: React.ChangeEvent<HTMLInputElement>) => {
        const { name, value } = event.target;
        setAuthorizationState((prevState) => ({ ...prevState, [name]: value }));
    };

    const handleSubmit = async (event: React.FormEvent) => {
        event.preventDefault();
        if (!authorizationState.username || !authorizationState.password) {
            return;
        }
        try {
            const response = await axios.post('/api/login', authorizationState);
            if (response.status === 200) {
                
            } else {
            }
        } catch (error) {
            console.error(error);
        }
    };
    
    return (
        <form onSubmit={handleSubmit} className={"registration-form"}>
            <label>
                Username:
                <br />
                <input
                    type="text"
                    name="username"
                    value={authorizationState.username}
                    onChange={handleChange}
                    required
                />
            </label>
            <br />
            <label>
                Password:
                <br />
                <input
                    type="password"
                    name="password"
                    value={authorizationState.password}
                    onChange={handleChange}
                    required
                />
            </label>
            <br />
            <Button type="submit">Submit</Button>
        </form>
    );
};
