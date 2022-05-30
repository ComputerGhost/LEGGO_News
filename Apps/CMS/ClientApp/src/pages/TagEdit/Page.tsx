import React, { useEffect, useState } from 'react';
import { Container, TextField } from '@mui/material';
import { useParams } from 'react-router-dom';
import { useTag, useUpdateTag } from '../../api/endpoints/tags';
import Page from '../../components/Page';
import UserRoles from '../../constants/UserRoles';
import { SaveToolbar } from '../../components/Toolbars';

export default function () {
    const tagId = parseInt(useParams().id!, 10);

    const { data } = useTag(tagId);
    const mutator = useUpdateTag(tagId);

    const [name, setName] = useState<string>('');
    const [description, setDescription] = useState<string>('');

    useEffect(() => {
        setName(data?.name ?? '');
        setDescription(data?.description ?? '');
    }, [data]);

    const handleSaveClick = async () => {
        await mutator.mutate({
            name,
            description,
        });
        if (!mutator.isSuccess) {
            console.error('Updating failed.');
            console.log(mutator);
        }
    };

    return (
        <Page
            requiresRole={[UserRoles.Editor, UserRoles.Journalist]}
            title='Edit Tag'
            toolbar={<SaveToolbar onSaveClick={handleSaveClick} />}
        >
            <Container>
                <TextField
                    fullWidth
                    label='Name'
                    margin='normal'
                    onChange={(e) => setName(e.target.value)}
                    value={name}
                />
                <TextField
                    label='Description'
                    fullWidth
                    margin='normal'
                    multiline
                    onChange={(e) => setDescription(e.target.value)}
                    rows={4}
                    value={description}
                />
            </Container>
        </Page>
    );
}
