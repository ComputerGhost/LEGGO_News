import * as React from 'react';
import { Grid, Paper } from '@material-ui/core';
import { makeStyles } from '@material-ui/core/styles';
import IImageProps from '../../interfaces/IImageProps';

const useStyles = makeStyles((theme) => ({
    paper: {
        width: 192,
        height: 192,
    }
}));

interface IProps {
    image: IImageProps
}

export default function ImageGridItem({ image }: IProps) {
    const classes = useStyles();
    const thumbnailUrl = `${image.baseUrl}?thumbnailSize=192`;
    return (
        <Grid item xl={2}>
            <Paper className={classes.paper}>
                <img alt={image.caption} src={thumbnailUrl} />
            </Paper>
        </Grid>
    );
}
