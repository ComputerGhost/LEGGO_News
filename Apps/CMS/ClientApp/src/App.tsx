import React, { useState } from 'react';
import { Navigate, Route, Routes } from 'react-router-dom';
import ArticleCreate from './pages/ArticleCreate';
import ArticleEdit from './pages/ArticleEdit';
import ArticleList from './pages/ArticleList';
import CharacterCreate from './pages/CharacterCreate';
import CharacterEdit from './pages/CharacterEdit';
import CharacterList from './pages/CharacterList';
import Help from './pages/Help';
import TagCreate from './pages/TagCreate';
import TagEdit from './pages/TagEdit';
import TagList from './pages/TagList';
import MediaList from './pages/MediaList';
import CalendarCreate from './pages/CalendarCreate';
import CalendarEdit from './pages/CalendarEdit';
import CalendarList from './pages/CalendarList';
import AuthService from './services/AuthenticationService';
import userContext from './contexts/userContext';
import { User } from 'oidc-client-ts';

export default function ()
{
    const [user, setUser] = useState<User | null>(null);

    var authService = new AuthService();
    authService.getUser().then(newUser => {
        if (newUser) {
            setUser(newUser);
        }
        if (!newUser) {
            authService.login();
        }
    });

    if (!user) {
        return <p>Please sign in.</p>
    }

    return (
        <userContext.Provider value={user}>
            <Routes>

                <Route path="/" element={<Navigate replace to="/articles" />} />

                <Route path='/articles' element={<ArticleList />} />
                <Route path='/articles/new' element={<ArticleCreate />} />
                <Route path='/articles/:id' element={<ArticleEdit />} />

                <Route path='/calendars' element={<CalendarList />} />
                <Route path='/calendars/new' element={<CalendarCreate />} />
                <Route path='/calendars/:id' element={<CalendarEdit />} />

                <Route path='/characters' element={<CharacterList />} />
                <Route path='/characters/new' element={<CharacterCreate />} />
                <Route path='/characters/:id' element={<CharacterEdit />} />

                <Route path='/tags' element={<TagList />} />
                <Route path='/tags/new' element={<TagCreate />} />
                <Route path='/tags/:id' element={<TagEdit />} />

                <Route path='/help' element={<Help />} />

                <Route path='/media' element={<MediaList />} />

            </Routes>
        </userContext.Provider>
    );
};
