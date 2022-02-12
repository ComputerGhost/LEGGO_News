import React, { useEffect, useState } from 'react';
import { makeStyles } from '@material-ui/styles';
import { Box, Container, IconButton, Tab } from '@material-ui/core';
import { Page } from '../../components';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { faSave } from '@fortawesome/free-solid-svg-icons';
import { useArticle, useUpdateArticle } from '../../api/articles';
import { TabContext, TabList } from '@material-ui/lab';
import EditorJS from '@editorjs/editorjs';
import ContentTab from './ContentTab';
import MetadataTab from './MetadataTab';

const useStyles = makeStyles((theme) => ({
    grow: {
        flexGrow: 1,
    },
}));

interface IProps {
    articleId?: number,
}

export default function CharacterEdit({
    articleId,
}: IProps) {
    const classes = useStyles();
    const [tabIndex, setTabIndex] = useState('0');
    const [title, setTitle] = useState<string>('');
    const [topStory, setTopStory] = useState<boolean>(false);
    const [editorApi, setEditorApi] = useState<EditorJS>();
    const { data } = useArticle(articleId);
    const mutator = useUpdateArticle(articleId);

    useEffect(() => {
        setTitle(data?.title ?? '');
    }, [data]);

    async function handleSaveClicked() {
        const content = await editorApi!.saver.save();
        await mutator.mutate({
            title,
            editorVersion: content.version!,
            content: JSON.stringify(content.blocks),
        });
    }

    if (!data) {
        return <Page title='Edit Article'><p>Loading</p></Page >;
    }

    const toolbar =
        <>
            <div className={classes.grow} />
            <IconButton onClick={handleSaveClicked}>
                <FontAwesomeIcon icon={faSave} fixedWidth />
            </IconButton>
        </>;

    return (
        <Page title='Edit Article' toolbar={toolbar}>
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

