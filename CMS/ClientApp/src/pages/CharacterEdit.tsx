import React, { useEffect, useState } from 'react';
import { makeStyles } from '@material-ui/styles';
import { Container, IconButton, TextField } from '@material-ui/core';
import { Page } from '../components';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { faSave } from '@fortawesome/free-solid-svg-icons';
import { useCharacter, useUpdateCharacter } from '../api/characters';
import { DatePicker } from '@material-ui/lab';

const useStyles = makeStyles((theme) => ({
    grow: {
        flexGrow: 1,
    },
}));

interface IProps {
    characterId?: number,
}

export default function CharacterEdit({
    characterId,
}: IProps) {
    const classes = useStyles();
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

    async function handleSaveClicked() {
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
    }

    const toolbar =
        <>
            <div className={classes.grow} />
            <IconButton onClick={handleSaveClicked}>
                <FontAwesomeIcon icon={faSave} fixedWidth />
            </IconButton>
        </>;

    return (
        <Page title='Edit Character' toolbar={toolbar}>
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

