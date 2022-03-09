import React, { useCallback, useState } from 'react';
import { Paper, Table, TableBody, TableCell, TableContainer, TableHead, TableRow } from '@material-ui/core';
import InfiniteScroll from '../../components/InfiniteScroll';
import { useCharacters } from '../../api/endpoints/characters';

interface IProps {
    search: string,
}

export default function ({
    search
}: IProps) {
    const { data, fetchNextPage, hasNextPage } = useCharacters(search);
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
                            <TableCell>Emoji</TableCell>
                            <TableCell>English Name</TableCell>
                        </TableRow>
                    </TableHead>
                    <TableBody>
                        {data && data.pages.map((page) =>
                            page.data.map((character) =>
                                <TableRow key={character.id}>
                                    <TableCell>
                                        {character.emoji}
                                    </TableCell>
                                    <TableCell>
                                        {character.englishName}
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
