import React from 'react';
import { TextField } from '@mui/material';
import { TagSaveData } from '../../api/endpoints/tags';

interface IProps {
    tagData: TagSaveData | null,
    setTagData: (data: TagSaveData) => void,
}

export default function ({ tagData, setTagData }: IProps) {
    const handleChanged = (property: string, newValue: string) => {
        if (tagData != null) {
            setTagData({
                ...tagData,
                [property]: newValue,
            });
        }
    };
    const handleNameChanged = (newName: string) => {
        const sanitizedName = newName.replace(/\W/, '');
        handleChanged('name', sanitizedName);
    };

    return (
        <>
            <TextField
                fullWidth
                label='Name'
                margin='normal'
                onChange={(e) => handleNameChanged(e.target.value)}
                value={tagData?.name}
            />
            <TextField
                label='Description'
                fullWidth
                margin='normal'
                multiline
                onChange={(e) => handleChanged('description', e.target.value)}
                rows={4}
                value={tagData?.description}
            />
        </>
    );
}
