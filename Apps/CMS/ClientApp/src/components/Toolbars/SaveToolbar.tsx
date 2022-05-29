import React from 'react';
import { makeStyles } from '@material-ui/styles';
import { IconButton } from '@material-ui/core';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { faSave } from '@fortawesome/free-solid-svg-icons';

const useStyles = makeStyles((theme) => ({
    grow: {
        flexGrow: 1,
    },
}));

interface IProps {
    onSaveClick: () => void,
}

export default function ({
    onSaveClick
}: IProps) {
    const classes = useStyles();

    return (
        <>
            <div className={classes.grow} />
            <IconButton color='inherit' onClick={onSaveClick}>
                <FontAwesomeIcon icon={faSave} fixedWidth />
            </IconButton>
        </>
    );
}

