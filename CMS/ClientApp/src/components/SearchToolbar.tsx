import React, { ChangeEvent } from 'react';
import { InputBase, Theme } from '@material-ui/core';
import { makeStyles } from '@material-ui/styles';
import { alpha, useTheme } from '@material-ui/core/styles';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { faSearch } from '@fortawesome/free-solid-svg-icons';

const useStyles = (theme: Theme) => makeStyles(() => ({
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
        color: 'inherit',
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

export default function ({
    placeholder,
    onChange,
}: IProps)
{
    const theme = useTheme();
    const classes = useStyles(theme)();

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
