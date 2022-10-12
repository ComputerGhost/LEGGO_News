import { faEdit, faTrash } from '@fortawesome/free-solid-svg-icons';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import React from 'react';
import { ListItem, ListItemText, ListItemButton, IconButton } from '@mui/material';
import { User } from 'oidc-client-ts';
import { useNavigate } from 'react-router-dom';
import { TagSummary, useDeleteTag } from '../../api/endpoints/tags';
import AuthorizationService from '../../services/AuthorizationService';
import UserRoles from '../../constants/UserRoles';
import userContext from '../../contexts/userContext';

interface IProps {
    tag: TagSummary,
}

export default function ({ tag }: IProps) {
    const navigate = useNavigate();
    const deleteTag = useDeleteTag(tag.id);

    const canDelete = (user: User | null) => {
        const service = new AuthorizationService(user);
        return service.hasRole(UserRoles.Administrator);
    };
    const canEdit = (user: User | null) => {
        const service = new AuthorizationService(user);
        return service.hasRole(UserRoles.Administrator);
    };

    const handleDeleteClick = () => {
        deleteTag.mutate(undefined);
    };
    const handleEditClick = () => {
        navigate(`./${tag.id}`);
    };
    const handleViewClick = () => {
        // nop for now.
    };

    const secondaryActions = (
        <userContext.Consumer>
            {(user) => (
                <>
                    {canDelete(user) && (
                        <IconButton onClick={handleDeleteClick}>
                            <FontAwesomeIcon icon={faTrash} fixedWidth />
                        </IconButton>
                    )}
                    {canEdit(user) && (
                        <IconButton onClick={handleEditClick}>
                            <FontAwesomeIcon icon={faEdit} fixedWidth />
                        </IconButton>
                    )}
                </>
            )}
        </userContext.Consumer>
    );

    return (
        <ListItem
            disablePadding
            secondaryAction={secondaryActions}
        >
            <ListItemButton action={() => handleViewClick()}>
                <ListItemText primary={tag.name} />
            </ListItemButton>
        </ListItem>
    );
}
