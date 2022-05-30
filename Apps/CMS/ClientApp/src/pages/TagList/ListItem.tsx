import React from 'react';
import { ListItem, ListItemText, ListItemButton } from '@mui/material';
import { TagSummary } from '../../api/endpoints/tags';
import ListItemActions from './ListItemActions';

interface IProps {
    tag: TagSummary,
}

export default function ({ tag }: IProps) {
    const handleViewClicked = () => {
        // nop for now.
    };

    return (
        <ListItem
            disablePadding
            secondaryAction={<ListItemActions tag={tag} />}
        >
            <ListItemButton action={() => handleViewClicked()}>
                <ListItemText primary={tag.name} />
            </ListItemButton>
        </ListItem>
    );
}
