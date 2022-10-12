import React, { useState } from 'react';
import { Box, Tab } from '@mui/material';
import { TabContext, TabList } from '@mui/lab';
import EditorJS from '@editorjs/editorjs';
import { ArticleSaveData } from '../../api/endpoints/articles';
import Page from '../../components/Page';
import ContentTab from './ContentTab';
import MetadataTab from './MetadataTab';

interface IProps {
    articleData: ArticleSaveData | null,
    setArticleData: (data: ArticleSaveData) => void,
    setEditorApi: (value: EditorJS) => void,
}

export default function ({ articleData, setArticleData, setEditorApi }: IProps) {
    const [tabIndex, setTabIndex] = useState('0');

    if (!articleData) {
        return <Page title='Edit Article'><p>Loading</p></Page>;
    }

    return (
        <TabContext value={tabIndex}>
            <Box
                sx={{
                    borderBottom: 1,
                    borderColor: 'divider',
                }}
            >
                <TabList onChange={(event, newValue) => setTabIndex(newValue)}>
                    <Tab label='Article' value='0' />
                    <Tab label='Metadata' value='1' />
                </TabList>
            </Box>
            <ContentTab
                tabIndex='0'
                articleData={articleData}
                setArticleData={setArticleData}
                setEditorApi={setEditorApi}
            />
            <MetadataTab
                tabIndex='1'
            />
        </TabContext>
    );
}
