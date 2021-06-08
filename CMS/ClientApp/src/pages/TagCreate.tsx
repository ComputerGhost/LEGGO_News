import React, { useState } from 'react';
import { makeStyles } from '@material-ui/styles';
import { Container, IconButton, TextField } from '@material-ui/core';
import { Page } from '../components';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { faSave } from '@fortawesome/free-solid-svg-icons';
import { useHistory } from 'react-router-dom';
import { useCreateTag } from '../api/tags';

const useStyles = makeStyles((theme) => ({
    grow: {
        flexGrow: 1,
    },
}));

export default function TagCreate() {
    const classes = useStyles();
    const history = useHistory();
    const [name, setName] = useState<string>('');
    const [description, setDescription] = useState<string>('');
    const mutator = useCreateTag();

    async function handleSaveClicked() {
        await mutator.mutate({ name, description });
        if (mutator.isSuccess)
            history.replace('./' + mutator.data!.id);
        else {
            console.error('Creation failed.');
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

