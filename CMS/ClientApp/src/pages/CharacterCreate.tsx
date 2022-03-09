import React, { useState } from 'react';
import { makeStyles } from '@material-ui/styles';
import { Container, IconButton, TextField } from '@material-ui/core';
import { Page } from '../components';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { faSave } from '@fortawesome/free-solid-svg-icons';
import { useNavigate } from 'react-router-dom';
import { useCreateCharacter } from '../api/endpoints/characters';
import { DatePicker } from '@material-ui/lab';

const useStyles = makeStyles((theme) => ({
    grow: {
        flexGrow: 1,
    },
}));

export default function () {
    const classes = useStyles();
    const navigate = useNavigate();
    const mutator = useCreateCharacter();

    const [birthDate, setBirthDate] = useState<Date | null>(null);
    const [englishName, setEnglishName] = useState<string>('');
    const [koreanName, setKoreanName] = useState<string>('');
    const [emoji, setEmoji] = useState<string>('');
    const [description, setDescription] = useState<string>('');

    async function handleSaveClicked() {
        await mutator.mutate({
            birthDate,
            englishName,
            koreanName,
            emoji,
            description,
        });
        if (mutator.isSuccess)
            navigate('./' + mutator.data!.id);
        else {
            console.error('Creation failed.');
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
        <Page title='Create Character' toolbar={toolbar}>
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

