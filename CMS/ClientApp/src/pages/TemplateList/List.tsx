import React, { useCallback, useState } from 'react';
import { Paper, Table, TableBody, TableCell, TableContainer, TableHead, TableRow } from '@material-ui/core';
import InfiniteScroll from '../../components/InfiniteScroll';
import { useTemplates } from '../../api/templates';

interface IProps {
    search: string,
}

export default function List({
    search
}: IProps) {
    const { data, fetchNextPage, hasNextPage } = useTemplates(search);
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
                            page.data.map((template) =>
                                <TableRow key={template.id}>
                                    <TableCell>
                                        {template.name}
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
