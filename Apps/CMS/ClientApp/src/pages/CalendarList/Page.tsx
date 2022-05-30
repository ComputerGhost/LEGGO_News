import React, { useState } from 'react';
import { Container } from '@mui/material';
import { useNavigate } from 'react-router-dom';
import List from './List';
import Page from '../../components/Page';
import SearchAddToolbar from '../../components/Toolbars/SearchAddToolbar';
import UserRoles from '../../constants/UserRoles';

export default function () {
    const [search, setSearch] = useState('');
    const navigate = useNavigate();

    const handleAddClick = () => {
        navigate('/calendars/new');
    };

    return (
        <Page
            title='Calendars'
            toolbar={(
                <SearchAddToolbar
                    onAddClick={handleAddClick}
                    onSearchChange={setSearch}
                    placeholder='Search calendar...'
                    rolesForAdd={UserRoles.Administrator}
                />
            )}
        >
            <Container>
                <List search={search} />
            </Container>
        </Page>
    );
}
