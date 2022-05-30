import React, { useEffect, useState } from 'react';
import { Container, TextField } from '@mui/material';
import { useParams } from 'react-router-dom';
import Page from '../../components/Page';
import { useCalendar, useUpdateCalendar } from '../../api/endpoints/calendars';
import UserRoles from '../../constants/UserRoles';
import { SaveToolbar } from '../../components/Toolbars';

export default function () {
    const calendarId = parseInt(useParams().id!, 10);

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

    const handleSaveClick = async () => {
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
    };

    return (
        <Page
            requiresRole={UserRoles.Administrator}
            title='Edit Calendar'
            toolbar={<SaveToolbar onSaveClick={handleSaveClick} />}
        >
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
                    onChange={(e) => setTimezoneOffset(parseInt(e.target.value, 10))}
                    type='number'
                    value={timezoneOffset}
                />
            </Container>
        </Page>
    );
}
