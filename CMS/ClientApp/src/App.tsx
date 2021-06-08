import React from 'react';
import { Route, Switch, useRouteMatch } from 'react-router-dom';
import { CharacterCreate, CharacterEdit, CharacterList, Home, Media, TagCreate, TagEdit, TagList } from './pages';

export default function App() {
    const match = useRouteMatch();

    return (
        <>
            <Switch>

                <Route exact path='/'>
                    <Home />
                </Route>

                <Route exact path='/characters'>
                    <CharacterList />
                </Route>
                <Route exact path='/characters/new'>
                    <CharacterCreate />
                </Route>
                <Route
                    exact path='/characters/:characterId(\d+)'
                    render={({ match }) => <CharacterEdit characterId={match.params['characterId']} />}
                />

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
