import React, { useState } from 'react';
import { makeStyles } from '@material-ui/styles';
import { Container, IconButton, TextField } from '@material-ui/core';
import { Page } from '../components';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { faSave } from '@fortawesome/free-solid-svg-icons';
import { useHistory } from 'react-router-dom';
import { useCreateTemplate } from '../api/templates';

const useStyles = makeStyles((theme) => ({
    grow: {
        flexGrow: 1,
    },
}));

export default function TemplateCreate() {
    const classes = useStyles();
    const history = useHistory();
    const [name, setName] = useState<string>('');
    const mutator = useCreateTemplate();

    async function handleSaveClicked() {
        await mutator.mutate({ name });
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
        <Page title='Create Template' toolbar={toolbar}>
            <Container>
                <TextField
                    fullWidth
                    label='Name'
                    margin='normal'
                    onChange={(e) => setName(e.target.value)}
                    value={name}
                />
            </Container>
        </Page>
    );
}

