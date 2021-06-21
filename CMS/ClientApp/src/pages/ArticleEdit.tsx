import React, { useEffect, useState } from 'react';
import { makeStyles } from '@material-ui/styles';
import { Container, IconButton, TextField } from '@material-ui/core';
import { Page } from '../components';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { faSave } from '@fortawesome/free-solid-svg-icons';
import { useArticle, useUpdateArticle } from '../api/articles';

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
    const { data } = useArticle(articleId);
    const mutator = useUpdateArticle(articleId);

    const [title, setTitle] = useState<string>('');

    useEffect(() => {
        setTitle(data?.title ?? '');
    }, [data]);

    async function handleSaveClicked() {
        await mutator.mutate({
            title
        });
        if (!mutator.isSuccess) {
            console.error('Updating failed.');
            console.log(mutator);
        }
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
                <TextField
                    fullWidth
                    label='Title'
                    margin='normal'
                    onChange={(e) => setTitle(e.target.value)}
                    value={title}
                />
            </Container>
        </Page>
    );
}

