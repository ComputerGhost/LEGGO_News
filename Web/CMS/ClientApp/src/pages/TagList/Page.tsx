import React, { useState } from 'react';
import { Container } from '@mui/material';
import { useNavigate } from 'react-router-dom';
import List from './List';
import Page from '../../components/Page';
import UserRoles from '../../constants/UserRoles';
import SearchAddToolbar from '../../components/Toolbars/SearchAddToolbar';

export default function () {
    const [search, setSearch] = useState('');
    const navigate = useNavigate();

    const handleAddClick = () => {
        navigate('/tags/new');
    };

    return (
        <Page
            title='Tags'
            toolbar={(
                <SearchAddToolbar
                    onAddClick={handleAddClick}
                    onSearchChange={setSearch}
                    placeholder='Search tags...'
                    rolesForAdd={UserRoles.Journalist}
                />
            )}
        >
            <Container>
                <List search={search} />
            </Container>
        </Page>
    );
}
