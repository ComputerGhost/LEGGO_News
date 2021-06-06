import React, { ReactElement } from 'react';
import { makeStyles } from '@material-ui/styles';
import { AppBar, IconButton, Toolbar, Typography, Theme } from '@material-ui/core';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { faBars } from '@fortawesome/free-solid-svg-icons';

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
    return (
        <>
            <AppBar position='fixed' sx={{ paddingLeft: isMobile ? 0 : drawerWidth }}>
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
