import React, { useState } from 'react';
import { Container, TextField } from '@mui/material';
import { useNavigate } from 'react-router-dom';
import Page from '../../components/Page';
import { useCreateCalendar } from '../../api/endpoints/calendars';
import UserRoles from '../../constants/UserRoles';
import { SaveToolbar } from '../../components/Toolbars';

export default function()
{
    const navigate = useNavigate();
    const mutator = useCreateCalendar();

    const [color, setColor] = useState<string>('');
    const [googleId, setGoogleId] = useState<string>('');
    const [name, setName] = useState<string>('');
    const [timezoneOffset, setTimezoneOffset] = useState<number>(0);

    async function handleSaveClick() {
        await mutator.mutate({
            color,
            googleId,
            name,
            timezoneOffset,
        });
        if (mutator.isSuccess)
            navigate('../' + mutator.data!.id);
        else {
            console.error('Creation failed.');
            console.log(mutator);
        }
    }

    return (
        <Page
            requiresRole={UserRoles.Administrator}
            title='Register Calendar'
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
                    onChange={(e) => setTimezoneOffset(Number.parseInt(e.target.value))}
                    type='number'
                    value={timezoneOffset}
                />
            </Container>
        </Page>
    );
}

