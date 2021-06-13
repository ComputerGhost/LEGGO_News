import React, { useState } from 'react';
import { makeStyles } from '@material-ui/styles';
import { Container, IconButton, TextField } from '@material-ui/core';
import { Page } from '../components';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { faSave } from '@fortawesome/free-solid-svg-icons';
import { useHistory } from 'react-router-dom';
import { useCreateUser } from '../api/users';
import PasswordField from '../components/PasswordField';

const useStyles = makeStyles((theme) => ({
    grow: {
        flexGrow: 1,
    },
}));

export default function UserCreate() {
    const classes = useStyles();
    const history = useHistory();
    const mutator = useCreateUser();

    const [username, setUsername] = useState<string>('');
    const [password, setPassword] = useState<string>('');
    const [displayName, setDisplayName] = useState<string>('');

    async function handleSaveClicked() {
        await mutator.mutate({
            username,
            password,
            displayName,
        });
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
        <Page title='Create User' toolbar={toolbar}>
            <Container>
                <TextField
                    fullWidth
                    label='Username'
                    margin='normal'
                    onChange={(e) => setUsername(e.target.value)}
                    value={username}
                    required
                />
                <PasswordField
                    fullWidth
                    label='Password'
                    margin='normal'
                    onChange={(e) => setPassword(e.target.value)}
                    value={password}
                    required
                />
                <TextField
                    fullWidth
                    label='Display name'
                    margin='normal'
                    onChange={(e) => setDisplayName(e.target.value)}
                    value={displayName}
                    required
                />
            </Container>
        </Page>
    );
}

