import React from 'react';
import { TextField } from '@mui/material';
import { DatePicker } from '@mui/lab';
import { CharacterSaveData } from '../../api/endpoints/characters';

interface IProps {
    characterData: CharacterSaveData | null,
    setCharacterData: (data: CharacterSaveData) => void,
}

export default function ({ characterData, setCharacterData }: IProps) {
    const handleChanged = (property: string, newValue: string) => {
        if (characterData != null) {
            setCharacterData({
                ...characterData,
                [property]: newValue,
            });
        }
    };
    const handleBirthDateChanged = (newValue: Date | null) => {
        if (characterData != null) {
            setCharacterData({
                ...characterData,
                birthDate: newValue,
            });
        }
    };

    return (
        <>
            <DatePicker
                label='Birth Date'
                onChange={(newValue) => handleBirthDateChanged(newValue)}
                value={characterData?.birthDate ?? null}
                renderInput={(params) => <TextField fullWidth margin='normal' {...params} />}
            />
            <TextField
                fullWidth
                label='English Name'
                margin='normal'
                onChange={(e) => handleChanged('englishName', e.target.value)}
                value={characterData?.englishName}
            />
            <TextField
                fullWidth
                label='Korean Name'
                margin='normal'
                onChange={(e) => handleChanged('koreanName', e.target.value)}
                value={characterData?.koreanName}
            />
            <TextField
                fullWidth
                label='Emoji'
                margin='normal'
                onChange={(e) => handleChanged('emoji', e.target.value)}
                value={characterData?.emoji}
            />
            <TextField
                label='Description'
                fullWidth
                margin='normal'
                multiline
                onChange={(e) => handleChanged('description', e.target.value)}
                rows={4}
                value={characterData?.description}
            />
        </>
    );
}
