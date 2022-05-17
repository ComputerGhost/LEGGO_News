﻿import React, { useEffect, useState } from 'react';
import { makeStyles } from '@material-ui/styles';
import { Container, IconButton, TextField } from '@material-ui/core';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { faSave } from '@fortawesome/free-solid-svg-icons';
import { useTag, useUpdateTag } from '../../api/endpoints/tags';
import { useParams } from 'react-router-dom';
import Page from '../../components/Page';

const useStyles = makeStyles((theme) => ({
    grow: {
        flexGrow: 1,
    },
}));

export default function()
{
    const tagId = parseInt(useParams().id!);

    const classes = useStyles();
    const { data } = useTag(tagId)
    const mutator = useUpdateTag(tagId);

    const [name, setName] = useState<string>('');
    const [description, setDescription] = useState<string>('');

    useEffect(() => {
        setName(data?.name ?? '');
        setDescription(data?.description ?? '');
    }, [data]);

    async function handleSaveClicked() {
        await mutator.mutate({
            name,
            description,
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
        <Page title='Edit Tag' toolbar={toolbar}>
            <Container>
                <TextField
                    fullWidth
                    label='Name'
                    margin='normal'
                    onChange={(e) => setName(e.target.value)}
                    value={name}
                />
                <TextField
                    label='Description'
                    fullWidth
                    margin='normal'
                    multiline
                    onChange={(e) => setDescription(e.target.value)}
                    rows={4}
                    value={description}
                />
            </Container>
        </Page>
    );
}
