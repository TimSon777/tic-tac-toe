import { Button } from '@mui/material';
import React from 'react';
import CloseIcon from '@mui/icons-material/Close';

interface SquareProps {
    value: any;
    onClick: any;
    isDisabled: boolean;
}

export const Square = ({value, onClick} : SquareProps) => {
    return (
        <Button
            className={"square-button"}
            onClick={onClick}
            variant="outlined"
            sx={{
                width: 64,
                height: 64,
                borderRadius: 0,
                border: "1px solid",
                "& .MuiButton-startIcon": { margin: 0 },
                color: "inherit",
                "&:hover": {borderColor: "aqua"}
            }}
        >
            {value}
        </Button>
    );
};