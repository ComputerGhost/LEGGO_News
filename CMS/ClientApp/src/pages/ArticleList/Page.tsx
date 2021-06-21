import React, { useState } from 'react';
import { faPlus } from '@fortawesome/free-solid-svg-icons';
import { Page, SearchToolbar } from '../../components';
import { Container, IconButton, } from '@material-ui/core';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { useHistory } from 'react-router-dom';
import List from './List';


export default function CharacterList() {
    var [search, setSearch] = useState('');
    var history = useHistory();

    function handleAddClicked() {
        history.push('/articles/new');
    }

    const toolbar =
        <>
            <SearchToolbar placeholder='Search articles...' onChange={setSearch} />
            <IconButton color='inherit' onClick={handleAddClicked}>
                <FontAwesomeIcon icon={faPlus} fixedWidth />
            </IconButton>
        </>;

    return (
        <Page title='Articles' toolbar={toolbar}>
            <Container>
                <List search={search} />
            </Container>
        </Page>
    );
}
