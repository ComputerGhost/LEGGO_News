import React from 'react';
import { Switch, useRouteMatch } from 'react-router-dom';
import { AuthRoute } from './components';
import { ArticleCreate, ArticleEdit, ArticleList, CharacterCreate, CharacterEdit, CharacterList, Help, Home, Media, TagCreate, TagEdit, TagList } from './pages';

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

                <AuthRoute exact path='/tags'>
                    <TagList />
                </AuthRoute>
                <AuthRoute exact path='/tags/new'>
                    <TagCreate />
                </AuthRoute>
                <AuthRoute
                    exact path='/tags/:tagId(\d+)'
                    render={({ match }) => <TagEdit tagId={match.params['tagId']} />}
                />

                <AuthRoute exact path='/help'>
                    <Help />
                </AuthRoute>

                <AuthRoute exact path='/media'>
                    <Media />
                </AuthRoute>

            </Switch>
        </>
    );
};
