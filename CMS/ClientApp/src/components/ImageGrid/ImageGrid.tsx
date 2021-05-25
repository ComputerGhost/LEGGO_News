import * as React from 'react';
import { connect } from 'react-redux';
import { ThunkDispatch } from 'redux-thunk'
import { ImageList, ImageListItem } from '@material-ui/core';
import InfiniteScroll from '../InfiniteScroll';
import { ApplicationState } from '../../store';
import { getMoreMedia, MediaItem } from '../../store/Media';

interface IProps {
    images: MediaItem[],
    hasMore: boolean,
    getMoreMedia: () => void,
}

function ImageGrid({
    images,
    hasMore,
    getMoreMedia,
}: IProps) {
    return (
        <InfiniteScroll
            data={images}
            hasMore={hasMore}
            next={getMoreMedia}
        >
            <ImageList cols={6}>
                {images.map((image) => 
                    <ImageListItem key={image.id} >
                        <img alt={image.caption} src={image.originalUrl} />
                    </ImageListItem>
                )}
            </ImageList>
        </InfiniteScroll>
    );
}


// Redux

interface StateProps {
    images: MediaItem[],
    hasMore: boolean,
}

interface DispatchProps {
    getMoreMedia: () => void,
}

const mapStateToProps = (state: ApplicationState, ownProps: IProps): StateProps => {
    const media = state.media!;
    return {
        images: media.data,
        hasMore: media.data.length !== media.totalCount,
    }
}

const mapDispatchToProps = (dispatch: ThunkDispatch<{}, {}, any>, ownProps: IProps): DispatchProps => {
    return {
        getMoreMedia: () => dispatch(getMoreMedia()),
    }
}

export default connect(mapStateToProps, mapDispatchToProps)(ImageGrid);
