import React, { useEffect, useState } from 'react';
import { Box, Container, Tab } from '@mui/material';
import { TabContext, TabList } from '@mui/lab';
import EditorJS from '@editorjs/editorjs';
import { useParams } from 'react-router-dom';
import { useArticle, useUpdateArticle } from '../../api/endpoints/articles';
import ContentTab from './ContentTab';
import MetadataTab from './MetadataTab';
import Page from '../../components/Page';
import { SaveToolbar } from '../../components/Toolbars';
import UserRoles from '../../constants/UserRoles';

export default function () {
    const articleId = parseInt(useParams().id!, 10);

    const [tabIndex, setTabIndex] = useState('0');
    const [title, setTitle] = useState<string>('');
    const [topStory, setTopStory] = useState<boolean>(false);
    const [editorApi, setEditorApi] = useState<EditorJS>();
    const { data } = useArticle(articleId);
    const mutator = useUpdateArticle(articleId);

    useEffect(() => {
        setTitle(data?.title ?? '');
    }, [data]);

    const handleSaveClick = async () => {
        const content = await editorApi!.save();
        await mutator.mutateAsync({
            title,
            format: 'editorjs',
            content: JSON.stringify(content.blocks),
        });
    };

    if (!data) {
        return <Page title='Edit Article'><p>Loading</p></Page>;
    }

    return (
        <Page
            requiresRole={UserRoles.Journalist}
            title='Edit Article'
            toolbar={<SaveToolbar onSaveClick={handleSaveClick} />}
        >
            <Container>
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
                        title={title}
                        setTitle={setTitle}
                        initialContent={JSON.parse(`{"blocks": ${data.content}}`)}
                        setEditorApi={setEditorApi}
                    />
                    <MetadataTab
                        tabIndex='1'
                        topStory={topStory}
                        setTopStory={setTopStory}
                    />
                </TabContext>
            </Container>
        </Page>
    );
}
