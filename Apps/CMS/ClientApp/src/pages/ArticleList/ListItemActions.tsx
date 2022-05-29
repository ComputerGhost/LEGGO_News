import React from 'react';
import { IconButton } from '@material-ui/core';
import { ArticleSummary, useDeleteArticle } from '../../api/endpoints/articles';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { faEdit, faTrash } from '@fortawesome/free-solid-svg-icons';
import { useNavigate } from 'react-router-dom';
import { User } from 'oidc-client-ts';
import userContext from '../../contexts/userContext';
import UserRoles from '../../constants/UserRoles';
import AuthorizationService from '../../services/AuthorizationService';

interface IProps {
    article: ArticleSummary,
}

export default function ({
    article
}: IProps) {
    var navigate = useNavigate();
    var deleteArticle = useDeleteArticle(article.id);

    function handleDeleteClick() {
        deleteArticle.mutate(undefined);
    }

    function handleEditClick() {
        navigate(`./${article.id}`);
    }

    const canDeleteArticle = (user: User | null) => {
        const service = new AuthorizationService(user);
        return service.hasRole(UserRoles.Editor);
    }

    const canEditArticle = (user: User | null) => {
        const service = new AuthorizationService(user);
        return service.hasRole(UserRoles.Journalist);
    }

    return (
        <userContext.Consumer>
            {(user) =>
                <>
                    {canDeleteArticle(user) &&
                        <IconButton onClick={handleDeleteClick}>
                            <FontAwesomeIcon icon={faTrash} fixedWidth />
                        </IconButton>
                    }
                    {canEditArticle(user) &&
                        <IconButton onClick={handleEditClick}>
                            <FontAwesomeIcon icon={faEdit} fixedWidth />
                        </IconButton >
                    }
                </>
            }
        </userContext.Consumer>
    );
}

