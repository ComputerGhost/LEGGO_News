import * as React from 'react';
import { Grid, Paper } from '@material-ui/core';
import { makeStyles } from '@material-ui/core/styles';

const useStyles = makeStyles((theme) => ({
    paper: {
        width: 192,
        height: 192,
    }
}));

export default function ImageGridItem({ }) {
    const classes = useStyles();
    return (
        <Grid item xl={2}>
            <Paper className={classes.paper}>
                test
            </Paper>
        </Grid>
    );
}
