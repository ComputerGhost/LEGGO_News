import React, { useEffect, useState } from 'react';
import { Container, TextField } from '@mui/material';
import { DatePicker } from '@mui/lab';
import { useParams } from 'react-router-dom';
import { useCharacter, useUpdateCharacter } from '../../api/endpoints/characters';
import Page from '../../components/Page';
import { SaveToolbar } from '../../components/Toolbars';
import UserRoles from '../../constants/UserRoles';

export default function () {
    const characterId = parseInt(useParams().id!, 10);

    const { data } = useCharacter(characterId);
    const mutator = useUpdateCharacter(characterId);

    const [birthDate, setBirthDate] = useState<Date | null>(null);
    const [englishName, setEnglishName] = useState<string>('');
    const [koreanName, setKoreanName] = useState<string>('');
    const [emoji, setEmoji] = useState<string>('');
    const [description, setDescription] = useState<string>('');

    useEffect(() => {
        setBirthDate(data?.birthDate ?? null);
        setEnglishName(data?.englishName ?? '');
        setKoreanName(data?.koreanName ?? '');
        setEmoji(data?.emoji ?? '');
        setDescription(data?.description ?? '');
    }, [data]);

    const handleSaveClick = async () => {
        await mutator.mutate({
            birthDate,
            englishName,
            koreanName,
            emoji,
            description,
        });
        if (!mutator.isSuccess) {
            console.error('Updating failed.');
            console.log(mutator);
        }
    };

    return (
        <Page
            requiresRole={UserRoles.Administrator}
            title='Edit Character'
            toolbar={<SaveToolbar onSaveClick={handleSaveClick} />}
        >
            <Container>
                <DatePicker
                    label='Birth Date'
                    onChange={(newValue) => setBirthDate(newValue)}
                    value={birthDate}
                    renderInput={(params) => <TextField fullWidth margin='normal' {...params} />}
                />
                <TextField
                    fullWidth
                    label='English Name'
                    margin='normal'
                    onChange={(e) => setEnglishName(e.target.value)}
                    value={englishName}
                />
                <TextField
                    fullWidth
                    label='Korean Name'
                    margin='normal'
                    onChange={(e) => setKoreanName(e.target.value)}
                    value={koreanName}
                />
                <TextField
                    fullWidth
                    label='Emoji'
                    margin='normal'
                    onChange={(e) => setEmoji(e.target.value)}
                    value={emoji}
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
