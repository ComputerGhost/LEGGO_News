import React, { ChangeEventHandler, FocusEventHandler, Ref } from 'react';
import { createReactEditorJS } from 'react-editor-js';
import { makeStyles } from '@material-ui/styles';
import EditorJS from '@editorjs/editorjs';

const useStyles = makeStyles({
    container: {
        width: '100%',
    }
});

export interface IEditorProps {

    // In current version, not called until a change is made.
    onApiSet?: (api: EditorJS) => void,

    // MaterialUI InputBaseComponent
    onFocus?: FocusEventHandler<HTMLInputElement>,
    onBlur?: FocusEventHandler<HTMLInputElement>,
    onChange?: ChangeEventHandler<HTMLInputElement>,
    forwardedRef?: Ref<HTMLDivElement>,
}

export default function Editor({
    onApiSet,
    onFocus,
    onBlur,
    onChange,
    forwardedRef,
}: IEditorProps) {
    var classes = useStyles();

    const ReactEditorJS = createReactEditorJS();

    function handleChange(apiInstance?: EditorJS) {
        if (apiInstance && onApiSet) {
            onApiSet(apiInstance);
        }
    }

    return (
        <div
            className={classes.container}
            onFocus={onFocus}
            onBlur={onBlur}
            ref={forwardedRef}
        >
            <ReactEditorJS
                onChange={handleChange}
            />
        </div>
    );
}
