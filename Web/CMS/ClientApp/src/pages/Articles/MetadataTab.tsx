import { Checkbox, FormControlLabel } from '@mui/material';
import React from 'react';
import TabPanel from '../../components/TabPanel';

interface IProps {
    tabIndex: string,
}

export default function ({ tabIndex }: IProps) {
    return (
        <TabPanel value={tabIndex}>
            <FormControlLabel
                control={(
                    <Checkbox />
                )}
                label='Top story'
            />
        </TabPanel>
    );
}
