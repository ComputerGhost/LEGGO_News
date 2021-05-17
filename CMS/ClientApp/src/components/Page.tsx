import React, { ReactElement, useState } from 'react';
import { Helmet } from 'react-helmet';
import { useMediaQuery } from '@material-ui/core';
import { makeStyles, useTheme } from '@material-ui/core/styles';
import { Toolbar } from '@material-ui/core';
import { NavDrawer, TopBar } from './';

const drawerWidth = 240;

const useStyles = makeStyles((theme) => ({
    topBar: {
        paddingLeft: drawerWidth,
    },

    main: {
        paddingLeft: drawerWidth,
    },

    [theme.breakpoints.down('sm')]: {
        topBar: {
            paddingLeft: 0,
        },
        main: {
            paddingLeft: drawerWidth,
        },
    }
}));

interface IProps {
    title: string,
    toolbar?: ReactElement,
    children: ReactElement,
}

export default function Page({ title, toolbar, children }: IProps) {
    const classes = useStyles();
    const theme = useTheme();
    const isMobile = useMediaQuery(theme.breakpoints.down('sm'));

    const [isDrawerOpen, setIsDrawerOpen] = useState(false);

    function handleOpenDrawer() {
        setIsDrawerOpen(true);
    }

    function handleCloseDrawer() {
        setIsDrawerOpen(false);
    }

    return (
        <>
            <Helmet>
                <title>{title} - LEGGO News</title>
            </Helmet>

            <NavDrawer
                width={drawerWidth}
                open={isDrawerOpen || !isMobile}
                allowClose={isMobile}
                onDrawerClosed={handleCloseDrawer}
            />

            <TopBar
                drawerWidth={drawerWidth}
                onDrawerOpen={handleOpenDrawer}
                isMobile={isMobile}
                title={title}
            >
                {toolbar ? toolbar!! : null}
            </TopBar>

            {/* Padding for the main content */}
            <Toolbar />

            <main className={classes.main}>
                {children}
            </main>
        </>
    );
}
