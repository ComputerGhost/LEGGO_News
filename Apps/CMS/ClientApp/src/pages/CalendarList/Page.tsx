import React, { useState } from 'react';
import { faPlus } from '@fortawesome/free-solid-svg-icons';
import { Container, IconButton, } from '@material-ui/core';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { useNavigate } from 'react-router-dom';
import List from './List';
import Page from '../../components/Page';
import SearchToolbar from '../../components/SearchToolbar';


export default function()
{
    var [search, setSearch] = useState('');
    var navigate = useNavigate();

    function handleAddClicked() {
        navigate('/calendars/new');
    }

    const toolbar =
        <>
            <SearchToolbar placeholder='Search calendars...' onChange={setSearch} />
            <IconButton color='inherit' onClick={handleAddClicked}>
                <FontAwesomeIcon icon={faPlus} fixedWidth />
            </IconButton>
        </>;

    return (
        <Page title='Calendars' toolbar={toolbar}>
            <Container>
                <List search={search} />
            </Container>
        </Page>
    );
}
