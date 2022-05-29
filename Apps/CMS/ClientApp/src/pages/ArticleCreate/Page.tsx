import React, { useState } from 'react';
import { Box, Container, Tab } from '@material-ui/core';
import { useNavigate } from 'react-router-dom';
import { useCreateArticle } from '../../api/endpoints/articles';
import { TabContext, TabList } from '@material-ui/lab';
import EditorJS from '@editorjs/editorjs';
import ContentTab from './ContentTab';
import MetadataTab from './MetadataTab';
import Page from '../../components/Page';
import UserRoles from '../../constants/UserRoles';
import { SaveToolbar } from '../../components/Toolbars';

export default function()
{
    const [tabIndex, setTabIndex] = useState('0');
    const [title, setTitle] = useState<string>('');
    const [editorApi, setEditorApi] = useState<EditorJS>();
    const mutator = useCreateArticle();
    const navigate = useNavigate();

    async function handleSaveClick() {
        const content = await editorApi!.save();
        const response = await mutator.mutateAsync({
            title,
            format: "editorjs",
            content: JSON.stringify(content.blocks),
        });
        navigate('../' + response.id);
    }

    return (
        <Page
            requiresRole={UserRoles.Journalist}
            title='Create Article'
            toolbar={<SaveToolbar onSaveClick={handleSaveClick} />}
        >
            <Container>
                <TabContext value={tabIndex}>
                    <Box sx={{ borderBottom: 1, borderColor: 'divider' }}>
                        <TabList onChange={(event, newValue) => setTabIndex(newValue)}>
                            <Tab label='Article' value='0' />
                            <Tab label='Metadata' value='1' />
                        </TabList>
                    </Box>
                    <ContentTab
                        tabIndex='0'
                        title={title}
                        setTitle={setTitle}
                        setEditorApi={setEditorApi}
                    />
                    <MetadataTab tabIndex='1' />
                </TabContext>
            </Container>
        </Page>
    );
}
