import React, { useEffect, useState } from 'react';
import { Route, Routes } from 'react-router-dom';
import { UserManager } from 'oidc-client-ts';
import ArticleCreate from './pages/ArticleCreate';
import ArticleEdit from './pages/ArticleEdit';
import ArticleList from './pages/ArticleList';
import CharacterCreate from './pages/CharacterCreate';
import CharacterEdit from './pages/CharacterEdit';
import CharacterList from './pages/CharacterList';
import Help from './pages/Help';
import Home from './pages/Home';
import TagCreate from './pages/TagCreate';
import TagEdit from './pages/TagEdit';
import TagList from './pages/TagList';
import MediaList from './pages/MediaList';
import CalendarCreate from './pages/CalendarCreate';
import CalendarEdit from './pages/CalendarEdit';
import CalendarList from './pages/CalendarList';
import AuthService from './services/AuthService';

export default function ()
{
    const [isSignedIn, setIsSignedIn] = useState(false);

    var authService = new AuthService();
    authService.getUser().then(user => {
        if (user) {
            setIsSignedIn(true);
        }
        if (!user) {
            authService.login();
        }
    });

    if (!isSignedIn) {
        return <p>Please sign in.</p>
    }

    return (
        <Routes>

            <Route path='/' element={<Home />} />

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
    );
};
