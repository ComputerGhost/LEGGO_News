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
                <DrawerItem text='Dashboard' icon={faHome} to='/' />
            </List>

            <Typography variant='h6' className={classes.header}>Content</Typography>
            <List>
                <DrawerItem text='Articles' icon={faPenNib} to='/' />
                <DrawerItem text='Leads' icon={faLightbulb} to='/' />
                <DrawerItem text='Comments' icon={faComments} to='/' />
                <DrawerItem text='Media' icon={faPhotoVideo} to='/' />
            </List>

            <Typography variant='h6' className={classes.header}>Setup</Typography>
            <List>
                <DrawerItem text='Tags' icon={faTags} to='/' />
                <DrawerItem text='Characters' icon={faTheaterMasks} to='/' />
                <DrawerItem text='Templates' icon={faShapes} to='/' />
                <DrawerItem text='Users' icon={faUsers} to='/' />
            </List>

            <Typography variant='h6' className={classes.header}>Information</Typography>
            <List>
                <DrawerItem text='Licenses' icon={faHandshake} to='/' />
                <DrawerItem text='Help and Tips' icon={faQuestionCircle} to='/' />
            </List>

        </Drawer>
    );
}
