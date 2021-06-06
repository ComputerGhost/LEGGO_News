import React, { useCallback, useEffect, useMemo, useState } from 'react';
import { Container, IconButton, Paper, Table, TableBody, TableCell, TableContainer, TableHead, TableRow } from '@material-ui/core';
import InfiniteScroll from '../../components/InfiniteScroll';
import { useTags } from '../../api/tags';

interface IProps {
    search: string,
}

export default function List({
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
            <TableContainer component={Paper}>
                <Table>
                    <TableHead>
                        <TableRow>
                            <TableCell>Name</TableCell>
                        </TableRow>
                    </TableHead>
                    <TableBody>
                        {data && data.pages.map((page) =>
                            page.data.map((tag) =>
                                <TableRow key={tag.id}>
                                    <TableCell>
                                        {tag.name}
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
