import { faEdit, faTrash } from '@fortawesome/free-solid-svg-icons';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { ListItem, ListItemText, ListItemButton, IconButton } from '@mui/material';
import { User } from 'oidc-client-ts';
import React from 'react';
import { useNavigate } from 'react-router-dom';
import UserRoles from '../../constants/UserRoles';
import userContext from '../../contexts/userContext';
import { ArticleSummary, useDeleteArticle } from '../../api/endpoints/articles';
import AuthorizationService from '../../services/AuthorizationService';

interface IProps {
    article: ArticleSummary,
}

export default function ({ article }: IProps) {
    const navigate = useNavigate();
    const deleteArticle = useDeleteArticle(article.id);

    const canDeleteArticle = (user: User | null) => {
        const service = new AuthorizationService(user);
        return service.hasRole(UserRoles.Editor);
    };
    const canEditArticle = (user: User | null) => {
        const service = new AuthorizationService(user);
        return service.hasRole(UserRoles.Journalist);
    };

    const handleDeleteClick = () => {
        deleteArticle.mutate(undefined);
    };
    const handleEditClick = () => {
        navigate(`./${article.id}`);
    };
    const handleViewClicked = () => {
        // nop for now.
    };

    const secondaryActions = (
        <userContext.Consumer>
            {(user) => (
                <>
                    {canDeleteArticle(user) && (
                        <IconButton onClick={handleDeleteClick}>
                            <FontAwesomeIcon icon={faTrash} fixedWidth />
                        </IconButton>
                    )}
                    {canEditArticle(user) && (
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
            <ListItemButton action={handleViewClicked}>
                <ListItemText primary={article.title} />
            </ListItemButton>
        </ListItem>
    );
}
