import React, { useEffect, useState } from 'react';
import { makeStyles } from '@material-ui/styles';
import { Container, IconButton, TextField } from '@material-ui/core';
import { Page } from '../components';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { faSave } from '@fortawesome/free-solid-svg-icons';
import { useTemplate, useUpdateTemplate } from '../api/templates';

const useStyles = makeStyles((theme) => ({
    grow: {
        flexGrow: 1,
    },
}));

interface IProps {
    templateId?: number,
}

export default function TagEdit({
    templateId,
}: IProps) {
    const classes = useStyles();
    const [name, setName] = useState<string>('');
    const { data } = useTemplate(templateId);
    const mutator = useUpdateTemplate(templateId);

    useEffect(() => {
        setName(data?.name ?? '');
    }, [data]);

    async function handleSaveClicked() {
        await mutator.mutate({ name });
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
        <Page title='Edit Template' toolbar={toolbar}>
            <Container>
                <TextField
                    fullWidth
                    label='Name'
                    margin='normal'
                    onChange={(e) => setName(e.target.value)}
                    value={name}
                />
            </Container>
        </Page>
    );
}

