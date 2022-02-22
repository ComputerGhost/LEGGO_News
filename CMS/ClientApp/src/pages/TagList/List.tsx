import React, { useCallback, useState } from 'react';
import { List, Paper } from '@material-ui/core';
import InfiniteScroll from '../../components/InfiniteScroll';
import { useTags } from '../../api/tags';
import ListItem from './ListItem';

interface IProps {
    search: string,
}

export default function ({
    search
}: IProps) {
    const { data, fetchNextPage, hasNextPage } = useTags(search);
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
                    page.data.map((tag) =>
                        <ListItem key={tag.id} tag={tag} />
                    )
                )}
            </List>
        </InfiniteScroll>
    );
}
