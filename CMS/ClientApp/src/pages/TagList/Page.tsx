import React, { useEffect, useState } from 'react';
import { faPlus } from '@fortawesome/free-solid-svg-icons';
import { Page, SearchToolbar } from '../../components';
import { Container, IconButton, Paper, Table, TableBody, TableCell, TableContainer, TableHead, TableRow } from '@material-ui/core';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import InfiniteScroll from '../../components/InfiniteScroll';
import { useHistory } from 'react-router-dom';
import { useTags } from '../../api/tags';
import List from './List';


export default function TagList() {
    var [search, setSearch] = useState('');
    var history = useHistory();

    function handleAddClicked() {
        history.push('/tags/new');
    }

    const toolbar =
        <>
            <SearchToolbar placeholder='Search Tags...' onChange={setSearch} />
            <IconButton color='inherit' onClick={handleAddClicked}>
                <FontAwesomeIcon icon={faPlus} fixedWidth />
            </IconButton>
        </>;

    return (
        <Page title='Tags' toolbar={toolbar}>
            <Container>
                <List search={search} />
            </Container>
        </Page>
    );
}
