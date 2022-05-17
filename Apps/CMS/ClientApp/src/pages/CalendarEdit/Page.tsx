import React, { useEffect, useState } from 'react';
import { makeStyles } from '@material-ui/styles';
import { Container, IconButton, TextField } from '@material-ui/core';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { faSave } from '@fortawesome/free-solid-svg-icons';
import { useParams } from 'react-router-dom';
import Page from '../../components/Page';
import { useCalendar, useUpdateCalendar } from '../../api/endpoints/calendars';

const useStyles = makeStyles((theme) => ({
    grow: {
        flexGrow: 1,
    },
}));

export default function()
{
    const calendarId = parseInt(useParams().id!);

    const classes = useStyles();
    const { data } = useCalendar(calendarId);
    const mutator = useUpdateCalendar(calendarId);

    const [color, setColor] = useState<string>('');
    const [googleId, setGoogleId] = useState<string>('');
    const [name, setName] = useState<string>('');
    const [timezoneOffset, setTimezoneOffset] = useState<number>(0);

    useEffect(() => {
        setColor(data?.color ?? '');
        setGoogleId(data?.googleId ?? '');
        setName(data?.name ?? '');
        setTimezoneOffset(data?.timezoneOffset ?? 0);
    }, [data]);

    async function handleSaveClicked() {
        await mutator.mutate({
            color,
            googleId,
            name,
            timezoneOffset,
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
        <Page title='Edit Calendar' toolbar={toolbar}>
            <Container>
                <TextField
                    fullWidth
                    label='Name'
                    margin='normal'
                    onChange={(e) => setName(e.target.value)}
                    value={name}
                />
                <TextField
                    fullWidth
                    label='Google Id'
                    margin='normal'
                    onChange={(e) => setGoogleId(e.target.value)}
                    value={googleId}
                />
                <TextField
                    fullWidth
                    label='Color'
                    margin='normal'
                    onChange={(e) => setColor(e.target.value)}
                    value={color}
                />
                <TextField
                    fullWidth
                    label='Korean Name'
                    margin='normal'
                    onChange={(e) => setTimezoneOffset(Number.parseInt(e.target.value))}
                    type='number'
                    value={timezoneOffset}
                />
            </Container>
        </Page>
    );
}

