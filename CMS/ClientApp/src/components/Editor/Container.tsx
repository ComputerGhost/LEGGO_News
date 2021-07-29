import React, { Component, forwardRef, Ref } from 'react';
import { FormControl, InputLabel, OutlinedInput } from '@material-ui/core';
import EditorJS from '@editorjs/editorjs';
import Editor, { IEditorProps } from './Editor';

interface IProps {
    label: string,
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

        const InputComponent = forwardRef((props: IEditorProps, forwardedRef: Ref<HTMLDivElement>) => {
            return (
                <Editor
                    {...props}
                    onApiSet={this.props.onApiSet}
                    forwardedRef={forwardedRef}
                />
            );
        });

        return (
            <FormControl fullWidth={this.props.fullWidth} margin={this.props.margin}>
                <InputLabel shrink>{this.props.label}</InputLabel>
                <OutlinedInput
                    fullWidth={this.props.fullWidth}
                    inputComponent={InputComponent}
                    label={this.props.label}
                    notched
                />
            </FormControl>
        );
    }
}
