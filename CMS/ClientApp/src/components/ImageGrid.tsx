import React, { useCallback, useState } from 'react';
import { ImageList, ImageListItem } from '@material-ui/core';
import InfiniteScroll from './InfiniteScroll';
import DropFile from './DropFile';
import { useMedia } from '../api/endpoints/media';

interface IProps {
    onFilesDropped: (files: File[]) => void,
    search: string,
}

export default function ImageGrid({
    onFilesDropped,
    search,
}: IProps) {
    const { data, fetchNextPage, hasNextPage } = useMedia(search);
    const [count, setCount] = useState(0);

    useCallback(() => {
        setCount(data?.pages?.reduce((a, cv) => a + cv.count, 0) ?? 0);
    }, [data]);

    return (
        <DropFile onDrop={onFilesDropped}>
            <InfiniteScroll
                dataLength={count}
                hasMore={hasNextPage || false}
                next={fetchNextPage}
            >
                <ImageList cols={8}>
                    {data?.pages.map((page) =>
                        page.data.map((image) => (
                            <ImageListItem key={image.id} >
                                <img alt={image.caption} src={image.thumbnailUrl} />
                            </ImageListItem>
                        ))
                    ) ?? ""}
                </ImageList>
            </InfiniteScroll>
        </DropFile>
    );
}
