import React, { useState } from 'react';
import { Container } from '@material-ui/core';
import { useNavigate } from 'react-router-dom';
import List from './List';
import Page from '../../components/Page';
import SearchAddToolbar from '../../components/Toolbars/SearchAddToolbar';
import UserRoles from '../../constants/UserRoles';


export default function()
{
    var [search, setSearch] = useState('');
    var navigate = useNavigate();

    function handleAddClick() {
        navigate('/characters/new');
    }

    return (
        <Page
            title='Characters'
            toolbar={
                <SearchAddToolbar
                    onAddClick={handleAddClick}
                    onSearchChange={setSearch}
                    placeholder='Search characters...'
                    rolesForAdd={UserRoles.Administrator}
                />
            }
        >
            <Container>
                <List search={search} />
            </Container>
        </Page>
    );
}
