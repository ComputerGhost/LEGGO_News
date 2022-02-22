import React, { ChangeEvent, useRef, useState } from 'react';
import { Container, IconButton } from '@material-ui/core';
import { ImageGrid, Page, SearchToolbar } from '../components';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { faPlus } from '@fortawesome/free-solid-svg-icons';
import { useUploadMedia } from '../api/media';


export default function Media() {
    const fileInput = useRef<HTMLInputElement>(null);
    const mutator = useUploadMedia();
    const [search, setSearch] = useState('');

    function handleAddClicked() {
        fileInput.current!.click();
    }

    function uploadMedia(files: File[]) {
        files.map(async (file) => {
            await mutator.mutateAsync(file);
        });
    }

    function handleFilesSelected(event: ChangeEvent<HTMLInputElement>) {
        uploadMedia(Array.from(event.target.files!));
    }

    function handleFilesDropped(files: File[]) {
        uploadMedia(files);
    }

    const toolbar =
        <>
            <SearchToolbar
                placeholder='Search Media...'
                onChange={setSearch}
            />
            <IconButton color='inherit' onClick={handleAddClicked }>
                <FontAwesomeIcon icon={faPlus} fixedWidth />
            </IconButton>
            <input ref={fileInput} hidden type='file' multiple onChange={handleFilesSelected} />
        </>;

    return (
        <Page title='Media' toolbar={toolbar}>
            <Container>
                <ImageGrid search={search} onFilesDropped={handleFilesDropped} />
            </Container>
        </Page>
    );
}

