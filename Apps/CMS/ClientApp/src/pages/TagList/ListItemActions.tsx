import React from 'react';
import { useNavigate } from 'react-router-dom';
import { IconButton } from '@mui/material';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { faEdit, faTrash } from '@fortawesome/free-solid-svg-icons';
import { User } from 'oidc-client-ts';
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

    const handleDeleteClick = () => {
        deleteTag.mutate(undefined);
    };

    const handleEditClick = () => {
        navigate(`./${tag.id}`);
    };

    const canDeleteTag = (user: User | null) => {
        const service = new AuthorizationService(user);
        return service.hasRole(UserRoles.Administrator);
    };

    const canEditTag = (user: User | null) => {
        const service = new AuthorizationService(user);
        return service.hasRole(UserRoles.Administrator);
    };

    return (
        <userContext.Consumer>
            {(user) => (
                <>
                    {canDeleteTag(user) && (
                        <IconButton onClick={handleDeleteClick}>
                            <FontAwesomeIcon icon={faTrash} fixedWidth />
                        </IconButton>
                    )}
                    {canEditTag(user) && (
                        <IconButton onClick={handleEditClick}>
                            <FontAwesomeIcon icon={faEdit} fixedWidth />
                        </IconButton>
                    )}
                </>
            )}
        </userContext.Consumer>
    );
}
