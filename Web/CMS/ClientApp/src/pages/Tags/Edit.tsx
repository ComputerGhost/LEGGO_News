import React, { useEffect, useState } from 'react';
import { Container } from '@mui/material';
import { useParams } from 'react-router-dom';
import { TagSaveData, useTag, useUpdateTag } from '../../api/endpoints/tags';
import Page from '../../components/Page';
import UserRoles from '../../constants/UserRoles';
import { SaveToolbar } from '../../components/Toolbars';
import Form from './Form';

export default function () {
    const tagId = parseInt(useParams().id!, 10);
    const updateTag = useUpdateTag(tagId);
    const [tagData, setTagData] = useState<TagSaveData | null>(null);

    const { data } = useTag(tagId);
    useEffect(() => {
        setTagData({
            ...data!,
        });
    }, [data]);

    const handleSaveClick = async () => {
        if (tagData != null) {
            await updateTag.mutateAsync(tagData!);
        }
    };

    return (
        <Page
            requiresRole={[UserRoles.Editor, UserRoles.Journalist]}
            title='Edit Tag'
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
