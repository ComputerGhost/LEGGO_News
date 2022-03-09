import React from 'react';
import { Route, Routes } from 'react-router-dom';
import ApiErrorBoundary from './api/ApiErrorBoundary';
import { useAuth } from './hooks/useAuth';
import { ArticleCreate, ArticleEdit, ArticleList, CharacterCreate, CharacterEdit, CharacterList, Help, Home, Media, TagCreate, TagEdit, TagList } from './pages';

export default function ()
{
    var user = useAuth();

    function handleAuthenticationRequired() {
        window.location.href = '/auth/challenge';
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
                <Route path='/articles/:id(\d+)' element={<ArticleEdit />} />

                <Route path='/characters' element={<CharacterList />} />
                <Route path='/characters/new' element={<CharacterCreate />} />
                <Route path='/characters/:id(\d+)' element={<CharacterEdit />} />

                <Route path='/tags' element={<TagList />} />
                <Route path='/tags/new' element={<TagCreate />} />
                <Route path='/tags/:id(\d+)' element={<TagEdit />} />

                <Route path='/help' element={<Help />} />

                <Route path='/media' element={<Media />} />

            </Routes>
        </ApiErrorBoundary>
    );
};
