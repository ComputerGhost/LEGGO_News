import React, { useState } from 'react';
import { Container, TextField } from '@material-ui/core';
import { useNavigate } from 'react-router-dom';
import { useCreateCharacter } from '../../api/endpoints/characters';
import { DatePicker } from '@material-ui/lab';
import Page from '../../components/Page';
import UserRoles from '../../constants/UserRoles';
import { SaveToolbar } from '../../components/Toolbars';

export default function()
{
    const navigate = useNavigate();
    const mutator = useCreateCharacter();

    const [birthDate, setBirthDate] = useState<Date | null>(null);
    const [englishName, setEnglishName] = useState<string>('');
    const [koreanName, setKoreanName] = useState<string>('');
    const [emoji, setEmoji] = useState<string>('');
    const [description, setDescription] = useState<string>('');

    async function handleSaveClick() {
        await mutator.mutate({
            birthDate,
            englishName,
            koreanName,
            emoji,
            description,
        });
        if (mutator.isSuccess)
            navigate('../' + mutator.data!.id);
        else {
            console.error('Creation failed.');
            console.log(mutator);
        }
    }

    return (
        <Page
            requiresRole={UserRoles.Administrator}
            title='Create Character'
            toolbar={<SaveToolbar onSaveClick={handleSaveClick} />}
        >
            <Container>
                <DatePicker
                    label='Birth Date'
                    onChange={(newValue: Date | null) => setBirthDate(newValue)}
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

