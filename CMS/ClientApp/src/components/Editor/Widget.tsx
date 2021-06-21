import React, { forwardRef, Ref, RefObject, useImperativeHandle, useState } from 'react';
import { Box, FormControl, Input, InputBaseComponentProps, InputLabel, InputProps, OutlinedInput, TextField } from '@material-ui/core';
import Editor, { IEditorProps } from './Editor';
import EditorJS, { API } from '@editorjs/editorjs';


interface IProps {
    label: string,
    onApiSet?: (api: EditorJS) => void,
    // Forwarded props
    fullWidth?: boolean,
    margin?: 'dense' | 'normal' | 'none',
}

export default function Widget({
    label,
    onApiSet,
    // Forwarded props
    fullWidth,
    margin,
}: IProps) {

    const InputComponent = forwardRef((props: IEditorProps, forwardedReference: Ref<HTMLDivElement>) => {
        return (
            <Editor
                {...props}
                onApiSet={onApiSet}
                forwardedRef={forwardedReference}
            />
        );
    });

    return (
        <FormControl fullWidth={fullWidth} margin={margin}>
            <InputLabel>{label}</InputLabel>
            <OutlinedInput
                fullWidth={fullWidth}
                inputComponent={InputComponent}
                label={label}
            />
        </FormControl>
    );
}
