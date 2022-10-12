import React from 'react';
import { TextField } from '@mui/material';
import { CalendarSaveData } from '../../api/endpoints/calendars';

interface IProps {
    calendarData: CalendarSaveData | null,
    setCalendarData: (data: CalendarSaveData) => void,
}

export default function ({ calendarData, setCalendarData }: IProps) {
    const handleChanged = (property: string, newValue: string) => {
        if (calendarData != null) {
            setCalendarData({
                ...calendarData,
                [property]: newValue,
            });
        }
    };
    const handleTimezoneChanged = (newValue: string) => {
        if (calendarData != null) {
            const parsedOffset = newValue === '' ? +9 : parseInt(newValue, 10);
            setCalendarData({
                ...calendarData,
                timezoneOffset: parsedOffset,
            });
        }
    };

    return (
        <>
            <TextField
                fullWidth
                label='Name'
                margin='normal'
                onChange={(e) => handleChanged('name', e.target.value)}
                value={calendarData?.name}
            />
            <TextField
                fullWidth
                label='Google ID'
                margin='normal'
                onChange={(e) => handleChanged('googleId', e.target.value)}
                value={calendarData?.googleId}
            />
            <TextField
                fullWidth
                label='Color'
                margin='normal'
                onChange={(e) => handleChanged('color', e.target.value)}
                value={calendarData?.color}
            />
            <TextField
                fullWidth
                label='Color'
                margin='normal'
                onChange={(e) => handleTimezoneChanged(e.target.value)}
                type='number'
                value={calendarData?.timezoneOffset}
            />
        </>
    );
}
