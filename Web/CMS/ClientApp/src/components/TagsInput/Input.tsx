import React, { Component, forwardRef, Ref } from 'react';
import { Autocomplete, FormControl, InputLabel, OutlinedInput } from '@mui/material';
import EditorJS, { OutputData } from '@editorjs/editorjs';
import Editor, { IEditorProps } from './Editor';

interface IProps {
    label: string,
    placeholder: string,
    margin?: 'dense' | 'normal' | 'none',
}

export default function ({
    label,
    placeholder,
    margin,
}): IProps {
    return (
        <TextField
            label={label}
            margin={margin}
            placeholder={placeholder}
        />
    );
}
