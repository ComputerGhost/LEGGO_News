import React, { useState } from 'react';
import { Container } from '@mui/material';
import { useNavigate } from 'react-router-dom';
import { CharacterSaveData, useCreateCharacter } from '../../api/endpoints/characters';
import Page from '../../components/Page';
import UserRoles from '../../constants/UserRoles';
import { SaveToolbar } from '../../components/Toolbars';
import Form from './Form';

export default function () {
    const navigate = useNavigate();
    const createCharacter = useCreateCharacter();
    const [characterData, setCharacterData] = useState<CharacterSaveData>({
        birthDate: null,
        englishName: '',
        koreanName: '',
        emoji: '',
        description: '',
    });

    const handleSaveClick = async () => {
        const response = await createCharacter.mutateAsync(characterData);
        navigate(`../${response.id}`);
    };

    return (
        <Page
            requiresRole={UserRoles.Administrator}
            title='Create Character'
            toolbar={<SaveToolbar onSaveClick={handleSaveClick} />}
        >
            <Container>
                <Form
                    characterData={characterData}
                    setCharacterData={setCharacterData}
                />
            </Container>
        </Page>
    );
}
