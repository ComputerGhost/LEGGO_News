import React from 'react';
import { IconButton } from '@material-ui/core';
import { CharacterSummary } from '../../api/endpoints/characters';
import { useDeleteCharacter } from '../../api/endpoints/calendars';
import { useNavigate } from 'react-router-dom';
import { User } from 'oidc-client-ts';
import AuthorizationService from '../../services/AuthorizationService';
import UserRoles from '../../constants/UserRoles';
import userContext from '../../contexts/userContext';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { faEdit, faTrash } from '@fortawesome/free-solid-svg-icons';

interface IProps {
    character: CharacterSummary
}

export default function ({
    character
}: IProps) {
    var navigate = useNavigate();
    var deleteCharacter = useDeleteCharacter(character.id);

    function handleDeleteClick() {
        deleteCharacter.mutate(undefined);
    }

    function handleEditClick() {
        navigate(`./${character.id}`);
    }

    const canDeleteCharacter = (user: User | null) => {
        const service = new AuthorizationService(user);
        return service.hasRole(UserRoles.Administrator);
    };

    const canEditCharacter = (user: User | null) => {
        const service = new AuthorizationService(user);
        return service.hasRole(UserRoles.Administrator);
    }

    return (
        <userContext.Consumer>
            {(user) =>
                <>
                    <IconButton
                        disabled={canDeleteCharacter(user)}
                        onClick={handleDeleteClick}
                    >
                        <FontAwesomeIcon icon={faTrash} fixedWidth />
                    </IconButton>
                    <IconButton
                        disabled={canEditCharacter(user)}
                        onClick={handleEditClick}
                    >
                        <FontAwesomeIcon icon={faEdit} fixedWidth />
                    </IconButton >
                </>
            }
        </userContext.Consumer>
    );
}
