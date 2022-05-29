import React, { useCallback, useState } from 'react';
import { List, Paper } from '@mui/material';
import InfiniteScroll from '../../components/InfiniteScroll';
import { useArticles } from '../../api/endpoints/articles';
import ListItem from './ListItem';

interface IProps {
    search: string,
}

export default function ({
    search
}: IProps) {
    const articles = useArticles(search);
    const { data, fetchNextPage, hasNextPage } = articles;
    const [count, setCount] = useState(0);

    useCallback(() => {
        setCount(data?.pages?.reduce((a, cv) => a + cv.count, 0) ?? 0);
    }, [data]);

    return (
        <InfiniteScroll
            dataLength={count}
            hasMore={hasNextPage ?? false}
            next={fetchNextPage}
        >
            <List component={Paper}>
                {data && data.pages.map((page) =>
                    page.data.map((article) =>
                        <ListItem key={article.id} article={article} />
                    )
                )}
            </List>
        </InfiniteScroll>
    );
}

