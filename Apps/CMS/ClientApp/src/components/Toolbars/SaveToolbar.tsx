import React from 'react';
import { IconButton } from '@mui/material';
import { makeStyles } from '@mui/styles';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { faSave } from '@fortawesome/free-solid-svg-icons';

const useStyles = makeStyles({
    grow: {
        flexGrow: 1,
    },
});

interface IProps {
    onSaveClick: () => void,
}

export default function ({ onSaveClick }: IProps) {
    const classes = useStyles();

    return (
        <>
            <div className={classes.grow} />
            <IconButton color='inherit' onClick={onSaveClick} size='large'>
                <FontAwesomeIcon icon={faSave} fixedWidth />
            </IconButton>
        </>
    );
}
