import * as React from "react";
import { makeStyles } from "@material-ui/core/styles";
import { Drawer, List, Typography } from '@material-ui/core';
import {
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
    header: {
        fontSize: '1rem',
        paddingLeft: theme.spacing(2),
    },
    toolbar: theme.mixins.toolbar
}))();

export default function NavDrawer({ width }: { width: number }) {
    const classes = useStyles(width);

    return (
        <Drawer
            classes={{ paper: classes.drawer }}
            open={true}
            variant='persistent'
        >
            <div className={classes.toolbar} />

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
