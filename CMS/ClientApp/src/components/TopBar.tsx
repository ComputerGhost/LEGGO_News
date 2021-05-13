import React, { ReactElement, useState } from 'react';
import { Helmet } from 'react-helmet';
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
    toolbar: ReactElement | null,
}

export default function TopBar({ drawerWidth, onDrawerOpen, isMobile, toolbar }: IProps) {
    const classes = useStyles(drawerWidth);

    const [title, setTitle] = useState('');

    function handleHelmetChanged({ title }: { title: string }) {
        // Somehow this can be an array, but we can't treat it like one, so...
        title = title.toString().split(',').join('');
        setTitle(title.split(' - ')[0]);
    }

    return (
        <>
            <Helmet onChangeClientState={handleHelmetChanged} />
            
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

                    {toolbar}

                </Toolbar>
            </AppBar>

            {/* Padding for the main content */}
            <Toolbar />
        </>
    );
}
