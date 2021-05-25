import React, { ChangeEvent } from 'react';
import { InputBase } from '@material-ui/core';
import { alpha, makeStyles } from '@material-ui/core/styles';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { faSearch } from '@fortawesome/free-solid-svg-icons';

const useStyles = makeStyles((theme) => ({
    search: {
        position: 'relative',
        borderRadius: theme.shape.borderRadius,
        backgroundColor: alpha(theme.palette.common.white, 0.15),
        '&:hover': {
            backgroundColor: alpha(theme.palette.common.white, 0.25),
        },
        marginLeft: theme.spacing(3),
        marginRight: theme.spacing(2),
        width: '100%',
    },
    icon: {
        padding: theme.spacing(0, 2),
        height: '100%',
        position: 'absolute',
        pointerEvents: 'none',
        display: 'flex',
        alignItems: 'center',
        justifyContent: 'center',
    },
    input: {
        // !important is bad, but MUI puts the classes in the wrong order in the current version
        color: 'inherit !important',
        width: '100%',
        '& .MuiInputBase-input': {
            padding: theme.spacing(1, 1, 1, 0),
            // vertical padding + font size from searchIcon
            paddingLeft: `calc(1em + ${theme.spacing(4)})`,
            width: '100%',
        }
    },
}));

interface IProps {
    placeholder: string,
    onChange: (search: string) => void,
}

export default function SearchToolbar({
    placeholder,
    onChange,
}: IProps) {
    const classes = useStyles();

    function handleSearchChanged(event: ChangeEvent<HTMLInputElement>) {
        onChange(event.target.value);
    }

    return (
        <div className={classes.search}>
            <div className={classes.icon}>
                <FontAwesomeIcon icon={faSearch} fixedWidth />
            </div>
            <InputBase
                className={classes.input}
                placeholder={placeholder}
                onChange={handleSearchChanged}
            />
        </div>
    );
}
