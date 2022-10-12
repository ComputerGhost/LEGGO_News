import React from 'react';
import { TextField } from '@mui/material';
import EditorJS from '@editorjs/editorjs';
import TabPanel from '../../components/TabPanel';
import Editor from '../../components/Editor';
import { ArticleSaveData } from '../../api/endpoints/articles';

interface IProps {
    tabIndex: string,
    articleData: ArticleSaveData | null,
    setArticleData: (data: ArticleSaveData) => void,
    setEditorApi: (value: EditorJS) => void
}

export default function ({
    tabIndex,
    articleData,
    setArticleData,
    setEditorApi,
}: IProps) {
    const handleChanged = (property: string, newValue: string) => {
        if (articleData != null) {
            setArticleData({
                ...articleData,
                [property]: newValue,
            });
        }
    };

    return (
        <TabPanel value={tabIndex}>
            <TextField
                fullWidth
                label='Title'
                margin='normal'
                onChange={(e) => handleChanged('title', e.target.value)}
                value={articleData?.title ?? ''}
            />
            {articleData?.content && (
                <Editor
                    initialData={JSON.parse(`{"blocks": ${articleData.content}}`)}
                    onApiSet={setEditorApi}
                    fullWidth
                    label='Content'
                    margin='normal'
                />
            )}
        </TabPanel>
    );
}
