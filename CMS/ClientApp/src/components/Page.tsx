import React, { ReactElement, useState } from 'react';
import { Helmet } from 'react-helmet';
import { useMediaQuery } from '@material-ui/core';
import { Toolbar, useTheme } from '@material-ui/core';
import TopBar from './TopBar';
import NavDrawer from './NavDrawer';

const drawerWidth = 240;

interface IProps {
    title: string,
    toolbar?: ReactElement,
    children: ReactElement | ReactElement[],
}

export default function ({
    title,
    toolbar,
    children
}: IProps)
{
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

            <main style={{ paddingLeft: isMobile ? 0 : drawerWidth }}>
                {children}
            </main>
        </>
    );
}
