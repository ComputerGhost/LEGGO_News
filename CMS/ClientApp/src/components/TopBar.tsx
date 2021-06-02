import React, { ReactElement, useState } from 'react';
import { makeStyles } from '@material-ui/core/styles';
import { AppBar, IconButton, Toolbar, Typography } from '@material-ui/core';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { faBars } from '@fortawesome/free-solid-svg-icons';

const useStyles = (drawerWidth: number) => makeStyles((theme) => ({
    topBar: {
        paddingLeft: drawerWidth,
    },

    [theme.breakpoints.down('sm')]: {
        topBar: {
            paddingLeft: 0,
        }
    }
}))();

interface IProps {
    drawerWidth: number,
    isMobile: boolean,
    onDrawerOpen: () => void,
    title: string,
    children: ReactElement | null,
}

export default function TopBar({
    drawerWidth,
    onDrawerOpen,
    isMobile,
    title,
    children
}: IProps) {
    const classes = useStyles(drawerWidth);

    return (
        <>
            <AppBar position='fixed' className={classes.topBar}>
                <Toolbar>

                    {isMobile &&
                        <IconButton onClick={onDrawerOpen}>
                            <FontAwesomeIcon icon={faBars} fixedWidth />
                        </IconButton>
                    }

                    <Typography variant='h6'>
                        {title}
                    </Typography>

                    {children}

                </Toolbar>
            </AppBar>
        </>
    );
}
