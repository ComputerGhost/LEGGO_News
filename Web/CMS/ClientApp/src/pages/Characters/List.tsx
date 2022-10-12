import React, { useCallback, useState } from 'react';
import { Container, List, Paper } from '@mui/material';
import { useNavigate } from 'react-router-dom';
import Page from '../../components/Page';
import SearchAddToolbar from '../../components/Toolbars/SearchAddToolbar';
import UserRoles from '../../constants/UserRoles';
import { useCharacters } from '../../api/endpoints/characters';
import InfiniteScroll from '../../components/InfiniteScroll';
import ListItem from './ListItem';

export default function () {
    const [search, setSearch] = useState('');
    const [count, setCount] = useState(0);

    const { data, fetchNextPage, hasNextPage } = useCharacters(search);
    useCallback(() => {
        setCount(data?.pages?.reduce((a, cv) => a + cv.count, 0) ?? 0);
    }, [data]);

    const handleAddClick = () => {
        const navigate = useNavigate();
        navigate('/characters/new');
    };

    return (
        <Page
            title='Characters'
            toolbar={(
                <SearchAddToolbar
                    onAddClick={handleAddClick}
                    onSearchChange={setSearch}
                    placeholder='Search characters...'
                    rolesForAdd={UserRoles.Administrator}
                />
            )}
        >
            <Container>
                <InfiniteScroll
                    dataLength={count}
                    hasMore={hasNextPage ?? false}
                    next={fetchNextPage}
                >
                    <List component={Paper}>
                        {data && data.pages.map((page) => (
                            page.data.map((character) => (
                                <ListItem key={character.id} character={character} />
                            ))
                        ))}
                    </List>
                </InfiniteScroll>
            </Container>
        </Page>
    );
}
