import * as React from 'react';
import { Route } from 'react-router';
import { makeStyles } from '@material-ui/core/styles';
import CssBaseline from '@material-ui/core/CssBaseline';
import TopBar from './components/TopBar';
import NavDrawer from './components/NavDrawer';
import Home from './pages/Home';

const drawerWidth = 240;

const useStyles = makeStyles((theme) => ({
    content: {
        marginLeft: drawerWidth,
        padding: theme.spacing(2),
    },
}));

export default function App() {
    const classes = useStyles();

    return (
        <>
            <CssBaseline />

            <TopBar leftMargin={drawerWidth} />

            <NavDrawer width={drawerWidth} />

            <main className={classes.content}>
                <Route exact path='/' component={Home} />
            </main>
        </>
    );
};
