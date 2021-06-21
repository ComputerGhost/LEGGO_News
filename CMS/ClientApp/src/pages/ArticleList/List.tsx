import React, { useCallback, useState } from 'react';
import { Paper, Table, TableBody, TableCell, TableContainer, TableHead, TableRow } from '@material-ui/core';
import InfiniteScroll from '../../components/InfiniteScroll';
import { useArticles } from '../../api/articles';

interface IProps {
    search: string,
}

export default function List({
    search
}: IProps) {
    const { data, fetchNextPage, hasNextPage } = useArticles(search);
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
            <TableContainer component={Paper}>
                <Table>
                    <TableHead>
                        <TableRow>
                            <TableCell>Title</TableCell>
                        </TableRow>
                    </TableHead>
                    <TableBody>
                        {data && data.pages.map((page) =>
                            page.data.map((article) =>
                                <TableRow key={article.id}>
                                    <TableCell>
                                        {article.title}
                                    </TableCell>
                                </TableRow>
                            )
                        )}
                    </TableBody>
                </Table>
            </TableContainer>
        </InfiniteScroll>
    );
}
