import React from 'react';
import { TextField } from '@mui/material';

interface IProps {
    label: string,
    placeholder: string,
    margin?: 'dense' | 'normal' | 'none',
}

export default function ({
    label,
    placeholder,
    margin,
}: IProps) {
    return (
        <TextField
            label={label}
            margin={margin}
            placeholder={placeholder}
        />
    );
}
