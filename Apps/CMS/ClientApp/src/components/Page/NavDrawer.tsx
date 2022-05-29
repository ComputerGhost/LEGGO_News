import React from "react";
import { Drawer, IconButton, List, Theme, Toolbar, Typography, useTheme } from '@mui/material';
import { makeStyles } from '@mui/styles';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import {
    faArrowLeft,
    faQuestionCircle,
    faPenNib,
    faPhotoVideo,
    faTag,
    faTheaterMasks,
    faCalendar,
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

export default function ({ width, open, allowClose, onDrawerClosed }: IProps) {
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
                    <IconButton onClick={onDrawerClosed} size="large">
                        <FontAwesomeIcon icon={faArrowLeft} fixedWidth />
                    </IconButton>
                }
            </Toolbar>

            <Typography variant='h6' className={classes.header}>Content</Typography>
            <List>
                <DrawerItem text='Articles' icon={faPenNib} href='/articles' />
                <DrawerItem text='Media' icon={faPhotoVideo} href='/media' />
                <DrawerItem text='Tags' icon={faTag} href='/tags' />
            </List>

            <Typography variant='h6' className={classes.header}>Setup</Typography>
            <List>
                <DrawerItem text='Calendars' icon={faCalendar} href='/calendars' />
                <DrawerItem text='Characters' icon={faTheaterMasks} href='/characters' />
            </List>

            <Typography variant='h6' className={classes.header}>Information</Typography>
            <List>
                <DrawerItem text='Help and Tips' icon={faQuestionCircle} href='/help' />
            </List>

        </Drawer>
    );
}
