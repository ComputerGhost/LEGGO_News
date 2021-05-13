import React, { ReactElement } from 'react';
import { connect } from 'react-redux';
import { Container, InputBase } from '@material-ui/core';
import { fade, makeStyles } from '@material-ui/core/styles';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { faSearch } from '@fortawesome/free-solid-svg-icons';
import { ImageGrid, PageInfo } from '../components';

const useStyles = makeStyles((theme) => ({
    search: {
        position: 'relative',
        borderRadius: theme.shape.borderRadius,
        backgroundColor: fade(theme.palette.common.white, 0.15),
        '&:hover': {
            backgroundColor: fade(theme.palette.common.white, 0.25),
        },
        marginLeft: theme.spacing(3),
        marginRight: theme.spacing(2),
        width: '100%',
    },
    searchIcon: {
        padding: theme.spacing(0, 2),
        height: '100%',
        position: 'absolute',
        pointerEvents: 'none',
        display: 'flex',
        alignItems: 'center',
        justifyContent: 'center',
    },
    inputRoot: {
        color: 'inherit',
        width: '100%',
    },
    inputInput: {
        padding: theme.spacing(1, 1, 1, 0),
        // vertical padding + font size from searchIcon
        paddingLeft: `calc(1em + ${theme.spacing(4)}px)`,
        width: '100%',
    },
}));

interface IProps {
    setToolbar: (toolbar: ReactElement) => void
}

function Media({ setToolbar }: IProps) {
    const classes = useStyles();

    setToolbar(
        <>
            <div className={classes.search}>
                <div className={classes.searchIcon}>
                    <FontAwesomeIcon icon={faSearch} fixedWidth />
                </div>
                <InputBase
                    placeholder='Search Media...'
                    classes={{
                        root: classes.inputRoot,
                        input: classes.inputInput
                    }}
                />
            </div>
        </>
    );

    return (
        <>
            <PageInfo title='Media' />
            <Container>
                <ImageGrid />
            </Container>
        </>
    );
}

export default connect()(Media);
