import React from "react";
import { makeStyles } from "@material-ui/core/styles";
import { Drawer, IconButton, List, Toolbar, Typography } from '@material-ui/core';
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

const useStyles = (width: number) => makeStyles(theme => ({
    drawer: {
        width: width,
    },
    grow: {
        flexGrow: 1,
    },
    header: {
        fontSize: '1rem',
        paddingLeft: theme.spacing(2),
    },
}))();

interface IProps {
    width: number,
    open: boolean,
    allowClose: boolean,
    onDrawerClosed: () => void,
}

export default function NavDrawer({ width, open, allowClose, onDrawerClosed }: IProps) {
    const classes = useStyles(width);

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
                <DrawerItem text='Articles' icon={faPenNib} href='/' />
                <DrawerItem text='Leads' icon={faLightbulb} href='/' />
                <DrawerItem text='Comments' icon={faComments} href='/' />
                <DrawerItem text='Media' icon={faPhotoVideo} href='/media' />
            </List>

            <Typography variant='h6' className={classes.header}>Setup</Typography>
            <List>
                <DrawerItem text='Tags' icon={faTags} href='/' />
                <DrawerItem text='Characters' icon={faTheaterMasks} href='/' />
                <DrawerItem text='Templates' icon={faShapes} href='/' />
                <DrawerItem text='Users' icon={faUsers} href='/' />
            </List>

            <Typography variant='h6' className={classes.header}>Information</Typography>
            <List>
                <DrawerItem text='Licenses' icon={faHandshake} href='/' />
                <DrawerItem text='Help and Tips' icon={faQuestionCircle} href='/' />
            </List>

        </Drawer>
    );
}
