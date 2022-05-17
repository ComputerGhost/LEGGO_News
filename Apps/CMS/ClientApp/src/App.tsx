import React from 'react';
import { Route, Routes } from 'react-router-dom';
import ApiErrorBoundary from './api/ApiErrorBoundary';
import { useAuth } from './hooks/useAuth';
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

export default function ()
{
    var user = useAuth();

    function handleAuthenticationRequired() {
        window.location.reload();
    }

    if (!user || !user.email) {
        handleAuthenticationRequired();
    }

    return (
        <ApiErrorBoundary onAuthenticationRequired={handleAuthenticationRequired}>
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
        </ApiErrorBoundary>
    );
};
