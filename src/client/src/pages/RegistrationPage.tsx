import React, {useState} from 'react';
import {Button, FormGroup, Input, InputLabel} from "@mui/material";

interface RegistrationState {
    username: string;
    password: string;
    confirmPassword: string;
}


export const RegistrationPage = () => {

    const [formData, setFormData] = useState<RegistrationState>({
        username: '',
        password: '',
        confirmPassword: '',
    });

    const handleChange = (event: React.ChangeEvent<HTMLInputElement>) => {
        const { name, value } = event.target;
        setFormData((prevState) => ({ ...prevState, [name]: value }));
    };

    const handleSubmit = (event: React.FormEvent) => {
        event.preventDefault();
        if (formData.password !== formData.confirmPassword) {
            alert("passwords don't match")
           return;
        } else {
            // submit form data
        }
    };
    
    return (
    <form onSubmit={handleSubmit} className={"registration-form"}>
        <FormGroup className={"form-group-inputs"}>
            <InputLabel htmlFor="username">Username</InputLabel>
            <Input
                type="text"
                name="username"
                value={formData.username}
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
