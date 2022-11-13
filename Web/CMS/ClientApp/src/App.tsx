import React, { useEffect, useState } from 'react';
import { Navigate, Route, Routes } from 'react-router-dom';
import { User } from 'oidc-client-ts';
import { ArticleCreate, ArticleEdit, ArticleList } from './pages/Articles';
import { CalendarCreate, CalendarEdit, CalendarList } from './pages/Calendars';
import { CharacterCreate, CharacterEdit, CharacterList } from './pages/Characters';
import Help from './pages/Help';
import MediaList from './pages/Media';
import { TagCreate, TagEdit, TagList } from './pages/Tags';
import AuthenticationService from './services/AuthenticationService';
import userContext from './contexts/userContext';

export default function () {
    const [user, setUser] = useState<User | null>(null);

    useEffect(() => {
        const authService = new AuthenticationService();
        authService.getUser().then(newUser => {
            if (newUser) {
                setUser(newUser);
            }
            if (!newUser) {
                authService.login();
            }
        });
    }, []);

    if (!user) {
        return <p>Please sign in.</p>;
    }

    return (
        <userContext.Provider value={user}>
            <Routes>

                <Route path='/' element={<Navigate replace to='/articles' />} />

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
}
