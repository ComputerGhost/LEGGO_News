import React, { useState } from 'react';
import { makeStyles } from '@material-ui/styles';
import { Box, Container, IconButton, Tab, TextField } from '@material-ui/core';
import { Editor, Page, TabPanel } from '../components';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { faSave } from '@fortawesome/free-solid-svg-icons';
import { useHistory } from 'react-router-dom';
import { useCreateArticle } from '../api/articles';
import { TabContext, TabList } from '@material-ui/lab';
import EditorJS from '@editorjs/editorjs';

const useStyles = makeStyles((theme) => ({
    grow: {
        flexGrow: 1,
    },
}));

export default function ArticleCreate() {
    const [tabIndex, setTabIndex] = useState('0');
    const [title, setTitle] = useState<string>('');
    const [editorApi, setEditorApi] = useState<EditorJS>();

    const classes = useStyles();
    const history = useHistory();
    const mutator = useCreateArticle();

    async function handleSaveClicked() {
        const content = await editorApi!.save();
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
                    <TabPanel value='0'>
                        <TextField
                            fullWidth
                            label='Title'
                            margin='normal'
                            onChange={(e) => setTitle(e.target.value)}
                            value={title}
                        />
                        <Editor
                            onApiSet={api => setEditorApi(api)}
                            fullWidth
                            label='Content'
                            margin='normal'
                        />
                    </TabPanel>
                    <TabPanel value='1'>
                        <div> stuff here </div>
                    </TabPanel>
                </TabContext>
            </Container>
        </Page>
    );
}

