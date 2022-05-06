import React from 'react';
import { ListItem, ListItemText, ListItemButton, IconButton } from '@material-ui/core';
import { TagSummary } from '../../api/endpoints/tags';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { faEdit } from '@fortawesome/free-solid-svg-icons';
import { useNavigate } from 'react-router-dom';

interface IProps {
    tag: TagSummary,
}

export default function ({
    tag
}: IProps) {
    var navigate = useNavigate();

    function handleViewClicked() {
        // nop for now.
    }

    function handleEditClicked() {
        navigate(`./${tag.id}`);
    }

    const secondaryAction =
        <IconButton onClick={handleEditClicked}>
            <FontAwesomeIcon icon={faEdit} fixedWidth />
        </IconButton >;

    return (
        <ListItem disablePadding secondaryAction={secondaryAction}>
            <ListItemButton action={handleViewClicked}>
                <ListItemText primary={tag.name} />
            </ListItemButton>
        </ListItem>
    );
}

