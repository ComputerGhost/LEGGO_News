/* eslint-disable react/no-unstable-nested-components */
/* eslint-disable react/no-this-in-sfc */

import React, { Component, forwardRef, Ref } from 'react';
import { FormControl, InputLabel, OutlinedInput } from '@mui/material';
import EditorJS, { OutputData } from '@editorjs/editorjs';
import Editor, { IEditorProps } from './Editor';

interface IProps {
    label: string,
    initialData?: OutputData,
    onApiSet?: (api: EditorJS) => void,
    // Forwarded props
    fullWidth?: boolean,
    margin?: 'dense' | 'normal' | 'none',
}

export default class Container extends Component<IProps> {
    shouldComponentUpdate() {
        return false;
    }

    render() {
        const InputComponent = forwardRef(
            (props: IEditorProps, forwardedRef: Ref<HTMLDivElement>) => {
                const { initialData, onApiSet } = this.props;
                return (
                    <Editor
                        {...props}
                        initialData={initialData}
                        onApiSet={onApiSet}
                        forwardedRef={forwardedRef}
                    />
                );
            }
        );

        const { fullWidth, margin, label } = this.props;

        return (
            <FormControl fullWidth={fullWidth} margin={margin}>
                <InputLabel shrink>{label}</InputLabel>
                <OutlinedInput
                    fullWidth={fullWidth}
                    inputComponent={InputComponent}
                    label={label}
                    notched
                />
            </FormControl>
        );
    }
}
