import React, { useState } from 'react';
import { makeStyles } from '@material-ui/styles';
import { Container, IconButton, TextField } from '@material-ui/core';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { faSave } from '@fortawesome/free-solid-svg-icons';
import { useNavigate } from 'react-router-dom';
import { useCreateTag } from '../../api/endpoints/tags';
import Page from '../../components/Page';

const useStyles = makeStyles((theme) => ({
    grow: {
        flexGrow: 1,
    },
}));

export default function()
{
    const classes = useStyles();
    const navigate = useNavigate();
    const mutator = useCreateTag();

    const [name, setName] = useState<string>('');
    const [description, setDescription] = useState<string>('');

    async function handleSaveClicked() {
        const response = await mutator.mutateAsync({
            name,
            description,
        });
        if (response)
            navigate('./' + response.id);
        else {
            console.error('Creation failed.');
            console.log(mutator);
        }
    }

    function handleNameChanged(newName: string) {
        newName = newName.replace(/\W/, '');
        setName(newName);
    }

    const toolbar =
        <>
            <div className={classes.grow} />
            <IconButton color='inherit' onClick={handleSaveClicked}>
                <FontAwesomeIcon icon={faSave} fixedWidth />
            </IconButton>
        </>;

    return (
        <Page title='Create Tag' toolbar={toolbar}>
            <Container>
                <TextField
                    fullWidth
                    label='Name'
                    margin='normal'
                    onChange={(e) => handleNameChanged(e.target.value)}
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

