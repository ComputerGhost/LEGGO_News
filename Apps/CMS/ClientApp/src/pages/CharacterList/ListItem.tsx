import React from 'react';
import { ListItem, ListItemButton, ListItemText } from '@mui/material';
import { CharacterSummary } from '../../api/endpoints/characters';
import ListItemActions from './ListItemActions';

interface IProps {
    character: CharacterSummary,
}

export default function ({ character }: IProps) {
    const title = `${character.emoji} ${character.englishName}`;

    return (
        <ListItem
            disablePadding
            secondaryAction={<ListItemActions character={character} />}
        >
            <ListItemButton>
                <ListItemText primary={title} />
            </ListItemButton>
        </ListItem>
    );
}
