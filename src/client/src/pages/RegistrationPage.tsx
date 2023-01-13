import React, {useState} from 'react';
import {Button, FormControlLabel, FormGroup, Input, InputLabel} from "@mui/material";


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
        <label>
            Username:
            <br />
            <input
                type="text"
                name="username"
                value={formData.username}
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
                value={formData.password}
                onChange={handleChange}
                required
            />
        </label>
        <br />
        <label>
            Confirm Password:
            <br />
            <input
                type="password"
                name="confirmPassword"
                value={formData.confirmPassword}
                onChange={handleChange}
                required
            />
        </label>
        <br />
        
        <Button type="submit">Submit</Button>
    </form>
    );
};
