import React, { ReactElement } from 'react';
import { Container } from '@material-ui/core';
import { ImageGrid, Page, SearchToolbar } from '../components';

export default function Media() {

    const toolbar = <SearchToolbar placeholder='Search Media...' />;

    return (
        <Page title='Media' toolbar={toolbar}>
            <Container>
                <ImageGrid />
            </Container>
        </Page>
    );
}
