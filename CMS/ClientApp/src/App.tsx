import React, { ReactElement, useState } from 'react';
import { Route } from 'react-router';
import { makeStyles, useTheme } from '@material-ui/core/styles';
import { useMediaQuery } from '@material-ui/core';
import { NavDrawer, TopBar } from './components';
import { Home, Media } from './pages';

const drawerWidth = 240;

const useStyles = makeStyles((theme) => ({
    main: {
        marginLeft: 240,
        padding: theme.spacing(2),
    },

    [theme.breakpoints.down('sm')]: {
        main: {
            marginLeft: 0,
        }
    }
}));

export default function App() {
    const classes = useStyles();
    const theme = useTheme();
    const isMobile = useMediaQuery(theme.breakpoints.down('sm'));

    const [isDrawerOpen, setIsDrawerOpen] = useState(false);
    const [toolbar, setToolbar] = useState<ReactElement|null>(null);

    function handleOpenDrawer() {
        setIsDrawerOpen(true);
    }

    function handleCloseDrawer() {
        setIsDrawerOpen(false);
    }

    return (
        <>
            <NavDrawer
                width={drawerWidth}
                open={isDrawerOpen || !isMobile}
                allowClose={isMobile}
                onDrawerClosed={handleCloseDrawer}
            />

            <TopBar
                drawerWidth={drawerWidth}
                isMobile={isMobile}
                onDrawerOpen={handleOpenDrawer}
                toolbar={toolbar}
            />

            <main className={classes.main}>
                <Route exact path='/' component={Home} />
                <Route exact path='/media' render={() => <Media setToolbar={setToolbar} />} />
            </main>
        </>
    );
};
