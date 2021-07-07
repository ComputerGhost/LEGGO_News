import React, { useCallback, useState } from 'react';
import { Paper, Table, TableBody, TableCell, TableContainer, TableHead, TableRow } from '@material-ui/core';
import InfiniteScroll from '../../components/InfiniteScroll';
import { useComments } from '../../api/comments';

interface IProps {
    search: string,
}

export default function List({
    search
}: IProps) {
    const { data, fetchNextPage, hasNextPage } = useComments(search);
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
                            page.data.map((comment) =>
                                <TableRow key={comment.id}>
                                    <TableCell>
                                        {comment.text}
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
