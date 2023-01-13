import React, {useState} from 'react';
import {Button, FormGroup, Input, InputLabel} from "@mui/material";
import axios from "axios";
import {useNavigate} from "react-router-dom";

interface RegistrationState {
    userName: string;
    password: string;
    confirmPassword: string;
}

export const RegistrationPage = () => {

    const [formData, setFormData] = useState<RegistrationState>({
        userName: '',
        password: '',
        confirmPassword: '',
    });

    const navigate = useNavigate();

    const handleChange = (event: React.ChangeEvent<HTMLInputElement>) => {
        const { name, value } = event.target;
        setFormData((prevState) => ({ ...prevState, [name]: value }));
    };

    const handleSubmit = async (event: React.FormEvent) => {
        event.preventDefault();
        if (formData.password !== formData.confirmPassword) {
            alert("passwords don't match")
            return;
        } else {
            try {
                const response = await axios.post(process.env.REACT_APP_ORIGIN_WEB_API + '/signup', {
                    userName: formData.userName,
                    password: formData.password
                });
                
                console.log( 'Response status: ' + response.status);
                
                if (response.status === 200) {
                    if (!response.data.succeeded) {
                        alert(response.data.errors);
                        return;
                    }
                } else {
                    console.log('fail');
                    alert('Server error');
                }

                navigate(`/authorization`, {replace: true});
                
            } catch (error) {
                console.error(error);
            }
        }
    };
    
    return (
    <form onSubmit={handleSubmit} className={"registration-form"}>
        <FormGroup className={"form-group-inputs"}>
            <InputLabel htmlFor="userName">Username</InputLabel>
            <Input
                type="text"
                name="userName"
                value={formData.userName}
                onChange={handleChange}
                id="username"
                required
            />

            <InputLabel htmlFor="password">Password</InputLabel>
            <Input
                type="password"
                name="password"
                value={formData.password}
                onChange={handleChange}
                id="password"
                required
            />

            <InputLabel htmlFor="confirmPassword">Confirm Password</InputLabel>
            <Input
                type="password"
                name="confirmPassword"
                value={formData.confirmPassword}
                onChange={handleChange}
                id="confirmPassword"
                required
            />
        </FormGroup>
        
        <Button type="submit">Submit</Button>
    </form>
    );
};
