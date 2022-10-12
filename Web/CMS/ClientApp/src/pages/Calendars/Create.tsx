import React, { useState } from 'react';
import { Container } from '@mui/material';
import { useNavigate } from 'react-router-dom';
import { CalendarSaveData, useCreateCalendar } from '../../api/endpoints/calendars';
import Page from '../../components/Page';
import UserRoles from '../../constants/UserRoles';
import { SaveToolbar } from '../../components/Toolbars';
import Form from './Form';

export default function () {
    const navigate = useNavigate();
    const createCalendar = useCreateCalendar();
    const [calendarData, setCalendarData] = useState<CalendarSaveData>({
        color: '',
        googleId: '',
        name: '',
        timezoneOffset: +9,
    });

    const handleSaveClick = async () => {
        const response = await createCalendar.mutateAsync(calendarData);
        navigate(`../${response.id}`);
    };

    return (
        <Page
            requiresRole={UserRoles.Administrator}
            title='Create Calendar'
            toolbar={<SaveToolbar onSaveClick={handleSaveClick} />}
        >
            <Container>
                <Form
                    calendarData={calendarData}
                    setCalendarData={setCalendarData}
                />
            </Container>
        </Page>
    );
}
