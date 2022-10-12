import React, { useEffect, useState } from 'react';
import { Container } from '@mui/material';
import { useParams } from 'react-router-dom';
import { CalendarSaveData, useCalendar, useUpdateCalendar } from '../../api/endpoints/calendars';
import Page from '../../components/Page';
import { SaveToolbar } from '../../components/Toolbars';
import UserRoles from '../../constants/UserRoles';
import Form from './Form';

export default function () {
    const calendarId = parseInt(useParams().id!, 10);
    const updateCalendar = useUpdateCalendar(calendarId);
    const [calendarData, setCalendarData] = useState<CalendarSaveData | null>(null);

    const { data } = useCalendar(calendarId);
    useEffect(() => {
        setCalendarData({
            ...data!,
        });
    }, [data]);

    const handleSaveClick = async () => {
        if (calendarData != null) {
            await updateCalendar.mutateAsync(calendarData!);
        }
    };

    return (
        <Page
            requiresRole={UserRoles.Administrator}
            title='Edit Calendar'
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
