import React, { Component, forwardRef, Ref } from 'react';
import { Autocomplete, FormControl, Input, InputLabel, OutlinedInput, TextField } from '@mui/material';

interface IProps {
    defaultValue?: string[],
    fullWidth?: boolean,
    label: string,
    margin?: 'dense' | 'normal' | 'none',
    placeholder: string,
}

export default function ({
    defaultValue,
    fullWidth,
    label,
    margin,
    placeholder,
}: IProps) {
    const input = (
        <TextField
            fullWidth={fullWidth}
            label={label}
            margin={margin}
            placeholder={placeholder}
        />
    );

    return (
        <Autocomplete
            defaultValue={defaultValue}
            freeSolo
            limitTags={5}
            multiple
            options={options}
            renderInput={(params) => <TextField {...params} />}
        />
    );
}
