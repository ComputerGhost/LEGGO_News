import React, { useEffect, useState } from 'react';
import { connect } from 'react-redux';
import { Container, IconButton, makeStyles, TextField } from '@material-ui/core';
import { Page } from '../components';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { faSave } from '@fortawesome/free-solid-svg-icons';
import { ThunkDispatch } from 'redux-thunk';
import { ApplicationState } from '../store';
import { getTag, createTag } from '../store/Tags/Actions';
import { TagDetails } from '../store/Tags';
import { KnownAction } from '../store/Media/Reducer';

const useStyles = makeStyles((theme) => ({
    grow: {
        flexGrow: 1,
    },
}));

interface IProps {
    tagId?: number,
    getTag?: (tagId: number) => Promise<TagDetails>,
    updateTag?: (name: string, description: string) => void,
}

function TagEdit({
    tagId,
    getTag,
    updateTag,
}: IProps) {
    const classes = useStyles();
    const [name, setName] = useState<string>('');
    const [description, setDescription] = useState<string>('');

    useEffect(() => {
        if (tagId !== undefined)
            getTag!(tagId);
    }, []);

    useEffect(() => {
        if (tagItem !== undefined) {
            setName(tagItem.name);
            setDescription(tagItem.description);
        }
    }, [tagItem]);

    function handleSaveClicked() {
        postTag!(name, description);
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
    getTag: (tagId: number) => (dispatch: (action: KnownAction) => void, getState: () => ApplicationState) => Promise<TagDetails>,
    postTag: (name: string, description: string) => (dispatch: (action: KnownAction) => void) => Promise<TagDetails>,
}

const mapDispatchToProps: DispatchProps = {
    getTag,
    postTag,
}

export default connect(null, mapDispatchToProps)(TagEdit);
