/* eslint-disable react/jsx-props-no-spreading */

import React, { useEffect, useState } from 'react';
import { useTabContext } from '@mui/lab';

// This is a modified copy/paste from:
// https://github.com/mui-org/material-ui/issues/21250
// It allows tab switching without unmounts.

export default function (props: any) {
    const {
        children,
        className,
        style,
        value: id,
        ...other
    } = props;
    const context = useTabContext();

    if (context === null) {
        throw new TypeError('No TabContext provided');
    }
    const tabId = context.value;

    const [visited, setVisited] = useState(false);
    useEffect(() => {
        if (id === tabId) {
            setVisited(true);
        }
    }, [id, tabId]);

    return (
        <div
            style={{
                position: 'relative',
            }}
        >
            <div
                className={className}
                style={{
                    boxSizing: 'border-box',
                    padding: 24,
                    ...style,
                    position: 'absolute',
                    top: 0,
                    width: '100%',
                    visibility: id === tabId ? 'visible' : 'hidden',
                }}
                {...other}
            >
                {visited && <div>{children}</div>}
            </div>
        </div>
    );
}
