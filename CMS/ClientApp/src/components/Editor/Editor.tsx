import React, { ChangeEventHandler, FocusEventHandler, forwardRef, Ref, RefObject, useImperativeHandle } from 'react';
import EditorJs from 'react-editor-js';
import { InputBaseComponentProps } from '@material-ui/core';
import { makeStyles } from '@material-ui/styles';
import { useState } from 'react';
import { useRef } from 'react';
import EditorJS, { API, OutputData } from '@editorjs/editorjs';

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
    const classes = useStyles();
    const proxyInput = useRef<HTMLInputElement>(null);
    const [api, setApi] = useState<EditorJS>();

    function handleChange(api: API) {
        const newData = '';

        // This is a hackish way to manually create a "change" event:

        const element = proxyInput.current!;
        const prototype = Object.getPrototypeOf(element);
        const prototypeSetter = Object.getOwnPropertyDescriptor(prototype, 'value')?.set;
        prototypeSetter!.call(element, newData);

        const event = new Event('input', { bubbles: true });
        proxyInput.current!.dispatchEvent(event);
    }

    function handleSetApi(newApi: EditorJS) {
        if (!newApi) {
            setApi(newApi);
            if (onApiSet) {
                onApiSet(newApi);
            }
        }
    }

    return (
        <div
            className={classes.container}
            onFocus={onFocus}
            onBlur={onBlur}
            ref={forwardedRef}
        >
            <input
                type='hidden'
                onChange={onChange}
                ref={proxyInput}
            />
            <EditorJs
                onChange={handleChange}
                instanceRef={apiInstance => handleSetApi(apiInstance)}
            />
        </div>
    );
}
