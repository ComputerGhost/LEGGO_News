import React, { ChangeEventHandler, FocusEventHandler, Ref } from 'react';
import { createReactEditorJS } from 'react-editor-js';
import { makeStyles } from '@material-ui/styles';
import EditorJS from '@editorjs/editorjs';
import { EDITOR_JS_TOOLS } from './Tools';

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
        console.log(apiInstance);
        if (apiInstance && onApiSet) {
            onApiSet(apiInstance);
        }
    }

    const initialData = JSON.parse("{\"blocks\": [{\"type\":\"paragraph\",\"data\":{\"text\":\"content test\"}}]}");

    return (
        <div
            className={classes.container}
            onFocus={onFocus}
            onBlur={onBlur}
            ref={forwardedRef}
        >
            <ReactEditorJS
                data={initialData}
                onInitialize={handleChange}
                tools={EDITOR_JS_TOOLS}
            />
        </div>
    );
}
