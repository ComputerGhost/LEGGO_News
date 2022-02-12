import React from 'react';
import { ListItem, ListItemText, ListItemButton, IconButton } from '@material-ui/core';
import { ArticleSummary } from '../../api/articles';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { faEdit } from '@fortawesome/free-solid-svg-icons';
import { useHistory } from 'react-router-dom';

interface IProps {
    article: ArticleSummary,
}

export default function ({
    article
}: IProps) {
    var history = useHistory();

    function handleViewClicked() {
        // nop for now.
    }

    function handleEditClicked() {
        history.push(`./articles/${article.id}`);
    }

    const secondaryAction =
        <IconButton onClick={handleEditClicked}>
            <FontAwesomeIcon icon={faEdit} fixedWidth />
        </IconButton >;

    return (
        <ListItem disablePadding secondaryAction={secondaryAction}>
            <ListItemButton action={handleViewClicked}>
                <ListItemText primary={article.title} />
            </ListItemButton>
        </ListItem>
    );
}
