import React, { useEffect, useState } from 'react';
import { Container } from '@mui/material';
import { useParams } from 'react-router-dom';
import { CharacterSaveData, useCharacter, useUpdateCharacter } from '../../api/endpoints/characters';
import Page from '../../components/Page';
import { SaveToolbar } from '../../components/Toolbars';
import UserRoles from '../../constants/UserRoles';
import Form from './Form';

export default function () {
    const characterId = parseInt(useParams().id!, 10);
    const updateCharacter = useUpdateCharacter(characterId);
    const [characterData, setCharacterData] = useState<CharacterSaveData | null>(null);

    const { data } = useCharacter(characterId);
    useEffect(() => {
        setCharacterData({
            ...data!,
        });
    }, [data]);

    const handleSaveClick = async () => {
        if (characterData != null) {
            await updateCharacter.mutateAsync(characterData!);
        }
    };

    return (
        <Page
            requiresRole={UserRoles.Administrator}
            title='Edit Character'
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
