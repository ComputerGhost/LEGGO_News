import React, { useEffect, useState } from 'react';
import { ImageList, ImageListItem } from '@material-ui/core';
import { makeStyles } from '@material-ui/core/styles';
import IMediaSummary from '../../interfaces/IMediaSummary';
import ISearchResults from '../../interfaces/ISearchResults';
import InfiniteScroll from '../InfiniteScroll';

const useStyles = makeStyles((theme) => ({
    image: {
    }
}));

interface IProps {
    search: string,
}

export default function ImageGrid({ search }: IProps) {
    const classes = useStyles();
    const [images, setImages] = useState(new Array<IMediaSummary>());
    const [hasMore, setHasMore] = useState(true);
    const [draw, setDraw] = useState(0);

    function handleResponse(json: ISearchResults<IMediaSummary>) {
        if (json.key !== draw)
            return;
        setHasMore(images.length < json.totalCount);
        setImages([...images, ...json.data]);
    }

    async function fetchData() {
        setDraw(draw + 1);

        const endpoint = `${process.env.REACT_APP_API_URL}/media`;
        const safeSearch = encodeURIComponent(search);
        const url = `${endpoint}?query=${safeSearch}&offset=${images.length}&key=${draw}`;

        const response = await fetch(url);
        const json = await response.json();
        handleResponse(json);
    }

    useEffect(() => { fetchData(); }, []);

    return (
        <InfiniteScroll
            data={images}
            hasMore={hasMore}
            next={fetchData}
            loader={<h4>Loading...</h4>}
        >
            <ImageList
                cols={6}
            >
                {images.map((image) => 
                    <ImageListItem key={image.imageId} >
                        <img
                            className={classes.image}
                            alt={image.caption}
                            src={image.originalUrl}
                        />
                    </ImageListItem>
                )}
            </ImageList>
        </InfiniteScroll>
    );
}
