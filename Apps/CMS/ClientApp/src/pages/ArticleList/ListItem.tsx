import React from 'react';
import { ListItem, ListItemText, ListItemButton } from '@mui/material';
import { ArticleSummary } from '../../api/endpoints/articles';
import ListItemActions from './ListItemActions';

interface IProps {
    article: ArticleSummary,
}

export default function ({ article }: IProps) {
    const handleViewClicked = () => {
        // nop for now.
    };

    return (
        <ListItem
            disablePadding
            secondaryAction={<ListItemActions article={article} />}
        >
            <ListItemButton action={handleViewClicked}>
                <ListItemText primary={article.title} />
            </ListItemButton>
        </ListItem>
    );
}
