import React from 'react';
import { TextField } from '@mui/material';

interface IProps {
    fullWidth?: boolean,
    label: string,
    margin?: 'dense' | 'normal' | 'none',
    placeholder: string,
}

export default function ({
    fullWidth,
    label,
    margin,
    placeholder,
}: IProps) {
    return (
        <TextField
            fullWidth={fullWidth}
            label={label}
            margin={margin}
            placeholder={placeholder}
        />
    );
}
