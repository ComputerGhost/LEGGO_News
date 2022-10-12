import { Container, List, Paper } from '@mui/material';
import React, { useCallback, useState } from 'react';
import { useNavigate } from 'react-router-dom';
import InfiniteScroll from '../../components/InfiniteScroll';
import { useTags } from '../../api/endpoints/tags';
import ListItem from './ListItem';
import Page from '../../components/Page';
import SearchAddToolbar from '../../components/Toolbars/SearchAddToolbar';
import UserRoles from '../../constants/UserRoles';

export default function () {
    const [search, setSearch] = useState('');
    const [count, setCount] = useState(0);

    const { data, fetchNextPage, hasNextPage } = useTags(search);
    useCallback(() => {
        setCount(data?.pages?.reduce((a, cv) => a + cv.count, 0) ?? 0);
    }, [data]);

    const handleAddClick = () => {
        const navigate = useNavigate();
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
                <InfiniteScroll
                    dataLength={count}
                    hasMore={hasNextPage ?? false}
                    next={fetchNextPage}
                >
                    <List component={Paper}>
                        {data && data.pages.map((page) => (
                            page.data.map((tag) => (
                                <ListItem key={tag.id} tag={tag} />
                            ))
                        ))}
                    </List>
                </InfiniteScroll>
            </Container>
        </Page>
    );
}
