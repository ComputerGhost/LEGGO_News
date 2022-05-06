import { ChangeEventHandler, FocusEventHandler, Ref, useCallback, useRef } from 'react';
import { createReactEditorJS } from 'react-editor-js';
import { makeStyles } from '@material-ui/styles';
import EditorJS, { OutputData } from '@editorjs/editorjs';
import { EDITOR_JS_TOOLS } from './Tools';
const Undo = require('editorjs-undo');
const DragDrop = require('editorjs-drag-drop');

const useStyles = makeStyles({
    container: {
        width: '100%',
    }
});

export interface IEditorProps {
    initialData?: OutputData,
    onApiSet?: (api: EditorJS) => void,

    // MaterialUI InputBaseComponent
    onFocus?: FocusEventHandler<HTMLInputElement>,
    onBlur?: FocusEventHandler<HTMLInputElement>,
    onChange?: ChangeEventHandler<HTMLInputElement>,
    forwardedRef?: Ref<HTMLDivElement>,
}

export default function Editor({
    initialData,
    onApiSet,
    onFocus,
    onBlur,
    onChange,
    forwardedRef,
}: IEditorProps) {
    var classes = useStyles();
    const editorCore = useRef<EditorJS|null>(null);

    const ReactEditorJS = createReactEditorJS();

    const handleInitialize = useCallback((instance?: EditorJS) => {
        if (instance) {
            editorCore.current = instance;
            if (onApiSet) {
                onApiSet(instance);
            }
        }
    }, []);

    function handleReady() {
        const editor = (editorCore.current as any)?._editorJS;

        new Undo({ editor });
        new DragDrop(editor);
    }

    return (
        <div
            className={classes.container}
            onFocus={onFocus}
            onBlur={onBlur}
            ref={forwardedRef}
        >
            <ReactEditorJS
                data={initialData}
                onInitialize={handleInitialize}
                onReady={handleReady}
                tools={EDITOR_JS_TOOLS}
            />
        </div>
    );
}
