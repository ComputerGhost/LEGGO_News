import React, { ReactElement, useState } from 'react';
import { Helmet } from 'react-helmet';
import { Toolbar, useMediaQuery, useTheme } from '@mui/material';
import { User } from 'oidc-client-ts';
import TopBar from './TopBar';
import NavDrawer from './NavDrawer';
import AuthorizationService from '../../services/AuthorizationService';
import userContext from '../../contexts/userContext';

const drawerWidth = 240;

interface IProps {
    title: string,
    toolbar?: ReactElement,
    requiresRole?: string | string[],
    children: ReactElement | ReactElement[],
}

export default function ({
    title,
    toolbar,
    requiresRole,
    children,
}: IProps) {
    const theme = useTheme();
    const isMobile = useMediaQuery(theme.breakpoints.down('md'));

    const [isDrawerOpen, setIsDrawerOpen] = useState(false);

    const ensureCanViewPage = (user: User | null) => {
        if (requiresRole) {
            const service = new AuthorizationService(user);
            if (requiresRole && !service.hasRole(requiresRole)) {
                throw new Error('Insufficient permissions.');
            }
        }
    };

    const handleOpenDrawer = () => {
        setIsDrawerOpen(true);
    };

    const handleCloseDrawer = () => {
        setIsDrawerOpen(false);
    };

    return (
        <userContext.Consumer>
            {(user) => (
                <>
                    {ensureCanViewPage(user)}

                    <Helmet>
                        <title>
                            {title}
                            {' '}
                            - LEGGO News
                        </title>
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

                    <main
                        style={{
                            paddingLeft: `${isMobile ? 0 : drawerWidth}px`,
                        }}
                    >
                        {children}
                    </main>
                </>
            )}
        </userContext.Consumer>
    );
}
