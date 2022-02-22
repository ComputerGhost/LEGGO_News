import React, { useState } from 'react';
import { faPlus } from '@fortawesome/free-solid-svg-icons';
import { Page, SearchToolbar } from '../../components';
import { Container, IconButton, } from '@material-ui/core';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { useHistory } from 'react-router-dom';
import List from './List';


export default function () {
    var [search, setSearch] = useState('');
    var history = useHistory();

    function handleAddClicked() {
        history.push('/characters/new');
    }

    const toolbar =
        <>
            <SearchToolbar placeholder='Search characters...' onChange={setSearch} />
            <IconButton color='inherit' onClick={handleAddClicked}>
                <FontAwesomeIcon icon={faPlus} fixedWidth />
            </IconButton>
        </>;

    return (
        <Page title='Characters' toolbar={toolbar}>
            <Container>
                <List search={search} />
            </Container>
        </Page>
    );
}
