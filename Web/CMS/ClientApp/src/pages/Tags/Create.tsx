import React, { useState } from 'react';
import { Container } from '@mui/material';
import { useNavigate } from 'react-router-dom';
import { TagSaveData, useCreateTag } from '../../api/endpoints/tags';
import Page from '../../components/Page';
import UserRoles from '../../constants/UserRoles';
import { SaveToolbar } from '../../components/Toolbars';
import Form from './Form';

export default function () {
    const navigate = useNavigate();
    const createTag = useCreateTag();
    const [tagData, setTagData] = useState<TagSaveData>({
        name: '',
        description: '',
    });

    const handleSaveClick = async () => {
        const response = await createTag.mutateAsync(tagData);
        navigate(`../${response.id}`);
    };

    return (
        <Page
            requiresRole={[
                UserRoles.Editor,
                UserRoles.Journalist,
            ]}
            title='Create Tag'
            toolbar={<SaveToolbar onSaveClick={handleSaveClick} />}
        >
            <Container>
                <Form
                    tagData={tagData}
                    setTagData={setTagData}
                />
            </Container>
        </Page>
    );
}
