import React from 'react';
import { Route, Switch, useRouteMatch } from 'react-router-dom';
import { AuthRoute } from './components';
import { ArticleCreate, ArticleEdit, ArticleList, CharacterCreate, CharacterEdit, CharacterList, Help, Home, Media, UserCreate, UserEdit, UserList } from './pages';

export default function App() {
    const match = useRouteMatch();

    return (
        <>
            <Switch>

                <AuthRoute exact path='/'>
                    <Home />
                </AuthRoute>

                <AuthRoute exact path='/articles'>
                    <ArticleList />
                </AuthRoute>
                <AuthRoute exact path='/articles/new'>
                    <ArticleCreate />
                </AuthRoute>
                <AuthRoute
                    exact path='/articles/:articleId(\d+)'
                    render={({ match }) => <ArticleEdit articleId={match.params['articleId']} />}
                />

                <AuthRoute exact path='/characters'>
                    <CharacterList />
                </AuthRoute>
                <AuthRoute exact path='/characters/new'>
                    <CharacterCreate />
                </AuthRoute>
                <AuthRoute
                    exact path='/characters/:characterId(\d+)'
                    render={({ match }) => <CharacterEdit characterId={match.params['characterId']} />}
                />

                <AuthRoute exact path='/help'>
                    <Help />
                </AuthRoute>

                <AuthRoute exact path='/media'>
                    <Media />
                </AuthRoute>

                <AuthRoute exact path='/users'>
                    <UserList />
                </AuthRoute>
                <AuthRoute exact path='/users/new'>
                    <UserCreate />
                </AuthRoute>
                <AuthRoute
                    exact path='/users/:userId(\d+)'
                    render={({ match }) => <UserEdit userId={match.params['userId']} />}
                />

            </Switch>
        </>
    );
};
