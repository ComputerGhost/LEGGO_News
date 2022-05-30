import React from 'react';
import { useNavigate } from 'react-router-dom';
import { ListItem, ListItemText, ListItemButton, IconButton } from '@mui/material';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { faEdit } from '@fortawesome/free-solid-svg-icons';
import { TagSummary } from '../../api/endpoints/tags';

interface IProps {
    tag: TagSummary,
}

export default function ({ tag }: IProps) {
    const navigate = useNavigate();

    const handleViewClicked = () => {
        // nop for now.
    };

    const handleEditClicked = () => {
        navigate(`./${tag.id}`);
    };

    const secondaryAction = (
        <IconButton
            onClick={() => handleEditClicked()}
            size='large'
        >
            <FontAwesomeIcon icon={faEdit} fixedWidth />
        </IconButton>
    );

    return (
        <ListItem disablePadding secondaryAction={secondaryAction}>
            <ListItemButton action={() => handleViewClicked()}>
                <ListItemText primary={tag.name} />
            </ListItemButton>
        </ListItem>
    );
}
