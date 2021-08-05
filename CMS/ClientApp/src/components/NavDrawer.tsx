import React from "react";
import { createStyles, makeStyles } from '@material-ui/styles';
import { Drawer, IconButton, List, Theme, Toolbar, Typography, useTheme } from '@material-ui/core';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import {
    faArrowLeft,
    faComments,
    faHandshake,
    faHome,
    faLightbulb,
    faQuestionCircle,
    faPenNib,
    faPhotoVideo,
    faShapes,
    faTags,
    faTheaterMasks,
    faUsers,
} from '@fortawesome/free-solid-svg-icons';
import DrawerItem from './DrawerItem';

const useStyles = (theme: Theme, width: number) => makeStyles(() => ({
    drawer: {
        width: width,
    },
    grow: {
        flexGrow: 1,
    },
    header: {
        fontSize: '1rem',
        paddingLeft: '1rem',
    },
}))();

interface IProps {
    width: number,
    open: boolean,
    allowClose: boolean,
    onDrawerClosed: () => void,
}

export default function NavDrawer({ width, open, allowClose, onDrawerClosed }: IProps) {
    const theme = useTheme();
    const classes = useStyles(theme, width);

    return (
        <Drawer
            classes={{ paper: classes.drawer }}
            open={open}
            variant='persistent'
        >
            <Toolbar>
                <div className={classes.grow} />
                {allowClose && 
                    <IconButton onClick={onDrawerClosed}>
                        <FontAwesomeIcon icon={faArrowLeft} fixedWidth />
                    </IconButton>
                }
            </Toolbar>

            <List>
                <DrawerItem text='Dashboard' icon={faHome} href='/' />
            </List>

            <Typography variant='h6' className={classes.header}>Content</Typography>
            <List>
                <DrawerItem text='Articles' icon={faPenNib} href='/articles' />
                <DrawerItem text='Media' icon={faPhotoVideo} href='/media' />
            </List>

            <Typography variant='h6' className={classes.header}>Setup</Typography>
            <List>
                <DrawerItem text='Characters' icon={faTheaterMasks} href='/characters' />
                <DrawerItem text='Templates' icon={faShapes} href='/templates' />
                <DrawerItem text='Users' icon={faUsers} href='/users' />
            </List>

            <Typography variant='h6' className={classes.header}>Information</Typography>
            <List>
                <DrawerItem text='Help and Tips' icon={faQuestionCircle} href='/help' />
            </List>

        </Drawer>
    );
}
