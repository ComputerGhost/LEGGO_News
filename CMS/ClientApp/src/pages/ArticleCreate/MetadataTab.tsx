import React from 'react';
import { TabPanel } from '../../components';

interface IProps {
    tabIndex: string,
}

export default function ({
    tabIndex
}: IProps) {
    return (
        <TabPanel value={tabIndex}>
            <div> stuff here </div>
        </TabPanel>
    );
}