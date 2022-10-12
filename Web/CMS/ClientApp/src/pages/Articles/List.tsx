import React, { useCallback, useState } from 'react';
import { useNavigate } from 'react-router-dom';
import { Container, List, Paper } from '@mui/material';
import Page from '../../components/Page';
import SearchAddToolbar from '../../components/Toolbars/SearchAddToolbar';
import UserRoles from '../../constants/UserRoles';
import { useArticles } from '../../api/endpoints/articles';
import InfiniteScroll from '../../components/InfiniteScroll';
import ListItem from './ListItem';

export default function () {
    const [search, setSearch] = useState('');
    const [count, setCount] = useState(0);

    const { data, fetchNextPage, hasNextPage } = useArticles(search);
    useCallback(() => {
        setCount(data?.pages?.reduce((a, cv) => a + cv.count, 0) ?? 0);
    }, [data]);

    const handleAddClick = () => {
        const navigate = useNavigate();
        navigate('/articles/new');
    };

    return (
        <Page
            title='Articles'
            toolbar={(
                <SearchAddToolbar
                    onAddClick={handleAddClick}
                    onSearchChange={setSearch}
                    placeholder='Search articles...'
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
                            page.data.map((article) => (
                                <ListItem key={article.id} article={article} />
                            ))
                        ))}
                    </List>
                </InfiniteScroll>
            </Container>
        </Page>
    );
}
