import * as React from 'react';
import { NavLink } from 'react-router-dom';
import { ListItem, ListItemIcon, ListItemText } from '@material-ui/core';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { IconProp } from '@fortawesome/fontawesome-svg-core';

interface IProps {
    text: string,
    icon: IconProp,
    href: string
}

export default function ({ text, icon, href } : IProps) {
    return (
        <li>
            <ListItem button component={NavLink} to={href} activeClassName='Mui-selected'>
                <ListItemIcon>
                    <FontAwesomeIcon icon={icon} fixedWidth />
                </ListItemIcon>
                <ListItemText primary={text} />
            </ListItem>
        </li>
    );
}
