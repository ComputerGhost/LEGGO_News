import React, { ChangeEvent, useRef, useState } from 'react';
import { Container } from '@mui/material';
import { useUploadMedia } from '../../api/endpoints/media';
import Page from '../../components/Page';
import SearchAddToolbar from '../../components/Toolbars/SearchAddToolbar';
import UserRoles from '../../constants/UserRoles';
import MediaGrid from '../../components/MediaGrid';

export default function () {
    const fileInput = useRef<HTMLInputElement>(null);
    const uploadMedia = useUploadMedia();
    const [search, setSearch] = useState('');

    const handleAddClick = () => {
        fileInput.current!.click();
    };

    const uploadFiles = (files: File[]) => {
        files.map(async (file) => {
            await uploadMedia.mutateAsync(file);
        });
    };

    const handleFilesSelected = (event: ChangeEvent<HTMLInputElement>) => {
        uploadFiles(Array.from(event.target.files!));
    };

    const handleFilesDrop = (files: File[]) => {
        uploadFiles(files);
    };

    return (
        <Page
            title='Media'
            toolbar={(
                <SearchAddToolbar
                    onAddClick={handleAddClick}
                    onSearchChange={setSearch}
                    placeholder='Search media...'
                    rolesForAdd={[UserRoles.Editor, UserRoles.Journalist]}
                />
            )}
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
