import * as React from 'react';
import { makeStyles } from '@material-ui/core/styles';
import { AppBar, Button, IconButton, Toolbar, Typography } from '@material-ui/core';
import MenuIcon from '@material-ui/icons/Menu';

const useStyles = (leftMargin: number) => makeStyles((theme) => ({
    bar: {
        paddingLeft: leftMargin,
    },
    title: {
        flexGrow: 1,
    }
}))();

export default function TopBar({ leftMargin }: { leftMargin: number }) {
    const classes = useStyles(leftMargin);

    return (
        <AppBar position='static' className={classes.bar}>
            <Toolbar>
                <Typography variant='h6' className={classes.title}>
                    LEGGO News
                </Typography>
                <Button color='inherit'>Login</Button>
            </Toolbar>
        </AppBar>
    );
}
