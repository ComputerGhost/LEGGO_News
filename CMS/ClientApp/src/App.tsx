import React from 'react';
import { Route, Switch, useRouteMatch } from 'react-router-dom';
import { ArticleCreate, ArticleEdit, ArticleList, CharacterCreate, CharacterEdit, CharacterList, Help, Home, Media, TemplateCreate, TemplateEdit, TemplateList, UserCreate, UserEdit, UserList } from './pages';

export default function App() {
    const match = useRouteMatch();

    return (
        <>
            <Switch>

                <Route exact path='/'>
                    <Home />
                </Route>

                <Route exact path='/articles'>
                    <ArticleList />
                </Route>
                <Route exact path='/articles/new'>
                    <ArticleCreate />
                </Route>
                <Route
                    exact path='/articles/:articleId(\d+)'
                    render={({ match }) => <ArticleEdit articleId={match.params['articleId']} />}
                />

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

                <Route exact path='/help'>
                    <Help />
                </Route>

                <Route exact path='/media'>
                    <Media />
                </Route>

                <Route exact path='/users'>
                    <UserList />
                </Route>
                <Route exact path='/users/new'>
                    <UserCreate />
                </Route>
                <Route
                    exact path='/users/:userId(\d+)'
                    render={({ match }) => <UserEdit userId={match.params['userId']} />}
                />

                <Route exact path='/templates'>
                    <TemplateList />
                </Route>
                <Route exact path='/templates/new'>
                    <TemplateCreate />
                </Route>
                <Route
                    exact path='/templates/:templateId(\d+)'
                    render={({ match }) => <TemplateEdit templateId={match.params['templateId']} />}
                />

            </Switch>
        </>
    );
};
