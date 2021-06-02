import React, { useState } from 'react';
import { connect } from 'react-redux';
import { Container, IconButton, makeStyles, TextField } from '@material-ui/core';
import { Page } from '../components';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { faSave } from '@fortawesome/free-solid-svg-icons';
import { TagDetails, TagSaveData } from '../store/Tags';
import { useHistory } from 'react-router-dom';
import { KnownAction } from '../store/Tags/Reducer';
import { createTag } from '../store/Tags';

const useStyles = makeStyles((theme) => ({
    grow: {
        flexGrow: 1,
    },
}));

interface IProps {
    createTag?: (data: TagSaveData) => Promise<TagDetails>,
}

function TagCreate({
    createTag,
}: IProps) {
    const classes = useStyles();
    const history = useHistory();
    const [name, setName] = useState<string>('');
    const [description, setDescription] = useState<string>('');

    async function handleSaveClicked() {
        const { id } = await createTag!({ name, description });
        history.replace('./' + id);
    }

    const toolbar =
        <>
            <div className={classes.grow} />
            <IconButton onClick={handleSaveClicked}>
                <FontAwesomeIcon icon={faSave} fixedWidth />
            </IconButton>
        </>;

    return (
        <Page title='Edit Tag' toolbar={toolbar}>
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
                />
            </Container>
        </Page>
    );
}


// Redux

interface DispatchProps {
    createTag: (data: TagSaveData) => (dispatch: (action: KnownAction) => void) => Promise<TagDetails>,
}

const mapDispatchToProps: DispatchProps = {
    createTag,
}

export default connect(null, mapDispatchToProps)(TagCreate);
