import React, { useState } from 'react';
import { makeStyles } from '@material-ui/styles';
import { Box, Container, IconButton, Tab, TextField } from '@material-ui/core';
import { Editor, Page, TabPanel } from '../../components';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { faSave } from '@fortawesome/free-solid-svg-icons';
import { useHistory } from 'react-router-dom';
import { useCreateArticle } from '../../api/articles';
import { TabContext, TabList } from '@material-ui/lab';
import EditorJS from '@editorjs/editorjs';
import ContentTab from './ContentTab';
import MetadataTab from './MetadataTab';

const useStyles = makeStyles((theme) => ({
    grow: {
        flexGrow: 1,
    },
}));

export default function () {
    const classes = useStyles();
    const [tabIndex, setTabIndex] = useState('0');
    const [title, setTitle] = useState<string>('');
    const [editorApi, setEditorApi] = useState<EditorJS>();
    const mutator = useCreateArticle();
    const history = useHistory();

    async function handleSaveClicked() {
        const content = await editorApi!.saver.save();
        await mutator.mutate({
            title,
            editorVersion: content.version!,
            content: JSON.stringify(content.blocks),
        });
        history.replace('./' + mutator.data!.id);
    }

    const toolbar =
        <>
            <div className={classes.grow} />
            <IconButton onClick={handleSaveClicked}>
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
