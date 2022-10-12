import { faEdit, faTrash } from '@fortawesome/free-solid-svg-icons';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { IconButton, ListItem, ListItemButton, ListItemText } from '@mui/material';
import { User } from 'oidc-client-ts';
import React from 'react';
import { useNavigate } from 'react-router-dom';
import { CharacterSummary, useDeleteCharacter } from '../../api/endpoints/characters';
import userContext from '../../contexts/userContext';
import AuthorizationService from '../../services/AuthorizationService';
import UserRoles from '../../constants/UserRoles';

interface IProps {
    character: CharacterSummary,
}

export default function ({ character }: IProps) {
    const navigate = useNavigate();
    const deleteCharacter = useDeleteCharacter(character.id);

    const canDeleteCharacter = (user: User | null) => {
        const service = new AuthorizationService(user);
        return service.hasRole(UserRoles.Administrator);
    };
    const canEditCharacter = (user: User | null) => {
        const service = new AuthorizationService(user);
        return service.hasRole(UserRoles.Administrator);
    };

    const handleDeleteClick = () => {
        deleteCharacter.mutate(undefined);
    };
    const handleEditClick = () => {
        navigate(`./${character.id}`);
    };

    const secondaryActions = (
        <userContext.Consumer>
            {(user) => (
                <>
                    {canDeleteCharacter(user) && (
                        <IconButton onClick={handleDeleteClick}>
                            <FontAwesomeIcon icon={faTrash} fixedWidth />
                        </IconButton>
                    )}
                    {canEditCharacter(user) && (
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
            <ListItemButton>
                <ListItemText primary={`${character.emoji} ${character.englishName}`} />
            </ListItemButton>
        </ListItem>
    );
}
