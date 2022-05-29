import React from 'react';
import { ListItem, ListItemButton, ListItemText } from '@mui/material';
import { CalendarSummary, } from '../../api/endpoints/calendars';
import ListItemActions from './ListItemActions';

interface IProps {
    calendar: CalendarSummary,
}

export default function ({
    calendar
}: IProps) {
    return (
        <ListItem
            disablePadding
            secondaryAction={<ListItemActions calendar={calendar} />}
        >
            <ListItemButton>
                <ListItemText primary={calendar.name} />
            </ListItemButton>
        </ListItem>
    );
}
