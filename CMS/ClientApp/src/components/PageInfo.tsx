import * as React from 'react';
import { Helmet } from 'react-helmet';

interface IProps {
    title: string
}

export default function PageInfo({ title }: IProps) {

    return (
        <Helmet>
            <title>{title} - LEGGO News</title>
        </Helmet>
    );
}
