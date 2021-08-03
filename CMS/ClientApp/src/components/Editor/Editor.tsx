import React, { ChangeEventHandler, FocusEventHandler, Ref } from 'react';
import EditorJs from 'react-editor-js';
import { makeStyles } from '@material-ui/styles';
import EditorJS from '@editorjs/editorjs';

const useStyles = makeStyles({
    container: {
        width: '100%',
    }
});

export interface IEditorProps {
    onApiSet?: (api: EditorJS) => void,
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

    function handleReady(apiInstance?: EditorJS) {
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
            <EditorJs
                onReady={handleReady}
            />
        </div>
    );
}
