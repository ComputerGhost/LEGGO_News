import React, { ChangeEvent, useRef, useState } from 'react';
import { Container } from '@mui/material';
import { useUploadMedia } from '../../api/endpoints/media';
import Page from '../../components/Page';
import SearchAddToolbar from '../../components/Toolbars/SearchAddToolbar';
import UserRoles from '../../constants/UserRoles';
import MediaGrid from '../../components/MediaGrid';


export default function()
{
    const fileInput = useRef<HTMLInputElement>(null);
    const mutator = useUploadMedia();
    const [search, setSearch] = useState('');

    function handleAddClick() {
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

    function handleFilesDrop(files: File[]) {
        uploadMedia(files);
    }

    return (
        <Page
            title='Media'
            toolbar={
                <SearchAddToolbar
                    onAddClick={handleAddClick}
                    onSearchChange={setSearch}
                    placeholder='Search media...'
                    rolesForAdd={[UserRoles.Editor, UserRoles.Journalist]}
                />
            }
        >
            <Container>
                <input
                    ref={fileInput}
                    hidden
                    type='file'
                    multiple
                    onChange={handleFilesSelected}
                />
                <MediaGrid search={search} onFilesDrop={handleFilesDrop} />
            </Container>
        </Page>
    );
}

