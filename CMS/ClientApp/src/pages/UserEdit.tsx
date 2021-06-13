import React, { useEffect, useState } from 'react';
import { makeStyles } from '@material-ui/styles';
import { Container, IconButton, TextField } from '@material-ui/core';
import { Page } from '../components';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { faSave } from '@fortawesome/free-solid-svg-icons';
import { useUser, useUpdateUser } from '../api/users';
import PasswordField from '../components/PasswordField';

const useStyles = makeStyles((theme) => ({
    grow: {
        flexGrow: 1,
    },
}));

interface IProps {
    userId?: number,
}

export default function UserEdit({
    userId,
}: IProps) {
    const classes = useStyles();
    const { data } = useUser(userId);
    const mutator = useUpdateUser(userId);

    const [username, setUsername] = useState<string>('');
    const [password, setPassword] = useState<string>('');
    const [displayName, setDisplayName] = useState<string>('');

    useEffect(() => {
        setUsername(data?.username ?? '');
        setPassword('');
        setDisplayName(data?.displayName ?? '');
    }, [data]);

    async function handleSaveClicked() {
        await mutator.mutate({
            username,
            password: (password.length > 0) ? password : undefined,
            displayName,
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
        <Page title='Edit User' toolbar={toolbar}>
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
                    label={password}
                    margin='normal'
                    onChange={(e) => setPassword(e.target.value)}
                    value={password}
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

