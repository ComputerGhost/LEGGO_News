import React from 'react';
import { IconButton } from '@mui/material';
import { useNavigate } from 'react-router-dom';
import { User } from 'oidc-client-ts';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { faEdit, faTrash } from '@fortawesome/free-solid-svg-icons';
import { CharacterSummary, useDeleteCharacter } from '../../api/endpoints/characters';
import AuthorizationService from '../../services/AuthorizationService';
import UserRoles from '../../constants/UserRoles';
import userContext from '../../contexts/userContext';

interface IProps {
    character: CharacterSummary
}

export default function ({ character }: IProps) {
    const navigate = useNavigate();
    const deleteCharacter = useDeleteCharacter(character.id);

    const handleDeleteClick = () => {
        deleteCharacter.mutate(undefined);
    };

    const handleEditClick = () => {
        navigate(`./${character.id}`);
    };

    const canDeleteCharacter = (user: User | null) => {
        const service = new AuthorizationService(user);
        return service.hasRole(UserRoles.Administrator);
    };

    const canEditCharacter = (user: User | null) => {
        const service = new AuthorizationService(user);
        return service.hasRole(UserRoles.Administrator);
    };

    return (
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
}
