import React, { useState } from 'react';
import { faPlus } from '@fortawesome/free-solid-svg-icons';
import { Page, SearchToolbar } from '../../components';
import { Container, IconButton, } from '@material-ui/core';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import List from './List';


export default function CharacterList() {
    var [search, setSearch] = useState('');

    const toolbar =
        <>
            <SearchToolbar placeholder='Search comments...' onChange={setSearch} />
        </>;

    return (
        <Page title='Comments' toolbar={toolbar}>
            <Container>
                <List search={search} />
            </Container>
        </Page>
    );
}
