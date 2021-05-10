import * as React from 'react';
import { ListItem, ListItemIcon, ListItemText } from '@material-ui/core';
import { NavLink } from 'reactstrap';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { IconProp } from '@fortawesome/fontawesome-svg-core';

interface IProps {
    text: string,
    icon: IconProp,
    to: string
}

export default function ({ text, icon, to } : IProps) {
    return (
        <ListItem component={NavLink} to={to}>
            <ListItemIcon>
                <FontAwesomeIcon icon={icon} fixedWidth />
            </ListItemIcon>
            <ListItemText primary={text} />
        </ListItem>
    );
}
