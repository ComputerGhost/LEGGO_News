import React, { ChangeEvent, useEffect, useRef } from 'react';
import { connect } from 'react-redux';
import { Container, IconButton } from '@material-ui/core';
import { ImageGrid, Page, SearchToolbar } from '../components';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { faPlus } from '@fortawesome/free-solid-svg-icons';
import { ThunkDispatch } from 'redux-thunk';
import { getMedia, postMedia } from '../store/Media';

interface IProps {
    getMedia: (search: string) => void,
    postMedia: (file: File) => void,
}

function Media({
    getMedia,
    postMedia,
}: IProps) {
    var fileInput = useRef<HTMLInputElement>(null);

    function handleAddClicked() {
        fileInput.current!.click();
    }

    function handleFilesSelected(event: ChangeEvent<HTMLInputElement>) {
        const fileList = Array.from(event.target.files!);
        [...fileList].map(file => {
            postMedia(file);
        });
    }

    function handleFilesDropped(files: File[]) {
        [...files].map(file => {
            postMedia(file);
        });
    }

    function handleSearchChanged(newSearch: string) {
        getMedia(newSearch);
    }

    useEffect(() => {
        handleSearchChanged('');
    }, [])

    const toolbar =
        <>
            <SearchToolbar
                placeholder='Search Media...'
                onChange={handleSearchChanged}
            />
            <IconButton color='inherit' onClick={handleAddClicked }>
                <FontAwesomeIcon icon={faPlus} fixedWidth />
            </IconButton>
            <input ref={fileInput} hidden type='file' multiple onChange={handleFilesSelected} />
        </>;

    return (
        <Page title='Media' toolbar={toolbar}>
            <Container>
                <ImageGrid onFilesDropped={handleFilesDropped} />
            </Container>
        </Page>
    );
}


// Redux

interface DispatchProps {
    getMedia: (search: string) => void,
    postMedia: (file: File) => void,
}

const mapDispatchToProps = (dispatch: ThunkDispatch<{}, {}, any>, ownProps: IProps): DispatchProps => {
    return {
        getMedia: (search: string) => dispatch(getMedia(search)),
        postMedia: (file: File) => dispatch(postMedia(file)),
    }
}

export default connect(null, mapDispatchToProps)(Media);
