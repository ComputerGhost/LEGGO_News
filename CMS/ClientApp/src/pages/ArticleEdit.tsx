import React, { useEffect, useState } from 'react';
import { makeStyles } from '@material-ui/styles';
import { Box, Checkbox, Container, FormControlLabel, IconButton, Tab, TextField } from '@material-ui/core';
import { Editor, Page, TabPanel } from '../components';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { faSave } from '@fortawesome/free-solid-svg-icons';
import { ArticleDetails, useArticle, useUpdateArticle } from '../api/articles';
import { TabContext, TabList } from '@material-ui/lab';
import EditorJS, { OutputBlockData, OutputData } from '@editorjs/editorjs';

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
    const [tabIndex, setTabIndex] = useState('0');
    const [title, setTitle] = useState<string>('');
    const [topStory, setTopStory] = useState<boolean>(false);
    const [editorApi, setEditorApi] = useState<EditorJS>();

    const classes = useStyles();
    const { data } = useArticle(articleId);
    const mutator = useUpdateArticle(articleId);

    useEffect(() => {
        setTitle(data?.title ?? '');
    }, [data]);

    useEffect(() => {
        return;
    }, [data, editorApi]);

    async function handleSaveClicked() {
        const content = await editorApi!.saver.save();
        await mutator.mutate({
            title,
            editorVersion: content.version!,
            content: JSON.stringify(content.blocks),
        });
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
                    <TabPanel value='0'>
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
                    <TabPanel value='1'>
                        <FormControlLabel
                            control={<Checkbox checked={topStory} onChange={(e) => setTopStory(e.target.checked)} />}
                            label="Top story"
                        />
                    </TabPanel>
                </TabContext>
            </Container>
        </Page>
    );
}

