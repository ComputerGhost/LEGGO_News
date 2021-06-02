import React from 'react';
import { Route, Switch, useRouteMatch } from 'react-router-dom';
import { Home, Media, TagEdit, TagList } from './pages';

export default function App() {
    const match = useRouteMatch();
    console.log(match);

    return (
        <>
            <Switch>
                <Route exact path='/'>
                    <Home />
                </Route>
                <Route exact path='/media'>
                    <Media />
                </Route>
                <Route exact path='/tags'>
                    <TagList />
                </Route>
                <Route exact path='/tags/new'>
                    <TagCreate />
                </Route>
                <Route
                    exact path='/tags/:tagId(\d+)'
                    render={({ match }) => <TagEdit tagId={match.params['tagId']} />}
                />
            </Switch>
        </>
    );
};
