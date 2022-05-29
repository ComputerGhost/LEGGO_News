import React, { useState } from 'react';
import { Container, TextField } from '@mui/material';
import { useNavigate } from 'react-router-dom';
import { useCreateTag } from '../../api/endpoints/tags';
import Page from '../../components/Page';
import UserRoles from '../../constants/UserRoles';
import { SaveToolbar } from '../../components/Toolbars';

export default function()
{
    const navigate = useNavigate();
    const mutator = useCreateTag();

    const [name, setName] = useState<string>('');
    const [description, setDescription] = useState<string>('');

    async function handleSaveClick() {
        const response = await mutator.mutateAsync({
            name,
            description,
        });
        if (response)
            navigate('./' + response.id);
        else {
            console.error('Creation failed.');
            console.log(mutator);
        }
    }

    function handleNameChanged(newName: string) {
        newName = newName.replace(/\W/, '');
        setName(newName);
    }

    return (
        <Page
            requiresRole={[ UserRoles.Editor, UserRoles.Journalist ]}
            title='Create Tag'
            toolbar={<SaveToolbar onSaveClick={handleSaveClick} />}
        >
            <Container>
                <TextField
                    fullWidth
                    label='Name'
                    margin='normal'
                    onChange={(e) => handleNameChanged(e.target.value)}
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

