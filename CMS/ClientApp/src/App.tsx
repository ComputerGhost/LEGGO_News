import React from 'react';
import { Route, Switch } from 'react-router-dom';
import { Home, Media } from './pages';

export default function App() {
    return (
        <>
            <Switch>
                <Route exact path='/'>
                    <Home />
                </Route>
                <Route exact path='/media'>
                    <Media />
                </Route>
            </Switch>
        </>
    );
};
