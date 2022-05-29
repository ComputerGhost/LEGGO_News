import React, { useState } from 'react';
import { Container } from '@material-ui/core';
import List from './List';
import Page from '../../components/Page';
import SearchAddToolbar from '../../components/Toolbars/SearchAddToolbar';
import UserRoles from '../../constants/UserRoles';
import { useNavigate } from 'react-router-dom';


export default function()
{
    const navigate = useNavigate();
    const [search, setSearch] = useState('');

    const handleAddClick = () => {
        navigate('/articles/new');
    }

    return (
        <Page
            title='Articles'
            toolbar={
                <SearchAddToolbar
                    onAddClick={handleAddClick}
                    onSearchChange={setSearch}
                    placeholder='Search articles...'
                    rolesForAdd={UserRoles.Journalist}
                />
            }
        >
            <Container>
                <List search={search} />
            </Container>
        </Page>
    );
}
