import React from 'react';
import { TextField } from '@material-ui/core';
import { Editor, TabPanel } from '../../components';
import EditorJS from '@editorjs/editorjs';

interface IProps {
    tabIndex: string,
    title: string,
    setTitle: (value: string) => void,
    setEditorApi: (value: EditorJS) => void
}

export default function ({
    tabIndex,
    title,
    setTitle,
    setEditorApi
}: IProps) {
    return (
        <TabPanel value={tabIndex}>
            <TextField
                fullWidth
                label='Title'
                margin='normal'
                onChange={(e) => setTitle(e.target.value)}
                value={title}
            />
            <Editor
                onApiSet={setEditorApi}
                fullWidth
                label='Content'
                margin='normal'
            />
        </TabPanel>
    );
}