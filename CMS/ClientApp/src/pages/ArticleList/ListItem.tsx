import React from 'react';
import { ListItem, ListItemText, ListItemButton, IconButton } from '@material-ui/core';
import { ArticleSummary, useDeleteArticle } from '../../api/endpoints/articles';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { faEdit, faTrash } from '@fortawesome/free-solid-svg-icons';
import { useNavigate } from 'react-router-dom';

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

    function handleEditClicked() {
        navigate(`./${article.id}`);
    }

    function handleViewClicked() {
        // nop for now.
    }

    const secondaryAction =
        <>
            <IconButton onClick={handleDeleteClick}>
                <FontAwesomeIcon icon={faTrash} fixedWidth />
            </IconButton>
            <IconButton onClick={handleEditClicked}>
                <FontAwesomeIcon icon={faEdit} fixedWidth />
            </IconButton >
        </>;

    return (
        <ListItem disablePadding secondaryAction={secondaryAction}>
            <ListItemButton action={handleViewClicked}>
                <ListItemText primary={article.title} />
            </ListItemButton>
        </ListItem>
    );
}

