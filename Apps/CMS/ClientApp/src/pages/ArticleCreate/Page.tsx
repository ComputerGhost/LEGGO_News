import React, { useState } from 'react';
import { makeStyles } from '@material-ui/styles';
import { Box, Container, IconButton, Tab } from '@material-ui/core';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { faSave } from '@fortawesome/free-solid-svg-icons';
import { useNavigate } from 'react-router-dom';
import { useCreateArticle } from '../../api/endpoints/articles';
import { TabContext, TabList } from '@material-ui/lab';
import EditorJS from '@editorjs/editorjs';
import ContentTab from './ContentTab';
import MetadataTab from './MetadataTab';
import Page from '../../components/Page';

const useStyles = makeStyles((theme) => ({
    grow: {
        flexGrow: 1,
    },
}));

export default function()
{
    const classes = useStyles();
    const [tabIndex, setTabIndex] = useState('0');
    const [title, setTitle] = useState<string>('');
    const [editorApi, setEditorApi] = useState<EditorJS>();
    const mutator = useCreateArticle();
    const navigate = useNavigate();

    async function handleSaveClicked() {
        const content = await editorApi!.save();
        const response = await mutator.mutateAsync({
            title,
            format: "editorjs",
            content: JSON.stringify(content.blocks),
        });
        navigate('./' + response.id);
    }

    const toolbar =
        <>
            <div className={classes.grow} />
            <IconButton color='inherit' onClick={handleSaveClicked}>
                <FontAwesomeIcon icon={faSave} fixedWidth />
            </IconButton>
        </>;

    return (
        <Page title='Create Article' toolbar={toolbar}>
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
