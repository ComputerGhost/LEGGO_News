import React from 'react';
import { NavLink } from 'react-router-dom';
import { ListItemButton, ListItemIcon, ListItemText } from '@mui/material';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { IconProp } from '@fortawesome/fontawesome-svg-core';

interface IProps {
    text: string,
    icon: IconProp,
    href: string
}

export default function ({
    text,
    icon,
    href,
}: IProps) {
    return (
        <li>
            <ListItemButton component={NavLink} to={href}>
                <ListItemIcon>
                    <FontAwesomeIcon icon={icon} fixedWidth />
                </ListItemIcon>
                <ListItemText primary={text} />
            </ListItemButton>
        </li>
    );
}
