import { Checkbox, FormControlLabel } from '@material-ui/core';
import React from 'react';
import TabPanel from '../../components/TabPanel';

interface IProps {
    tabIndex: string,
    topStory: boolean,
    setTopStory: (value: boolean) => void,
}

export default function ({
    tabIndex,
    topStory,
    setTopStory,
}: IProps) {
    return (
        <TabPanel value={tabIndex}>
            <FormControlLabel
                control={<Checkbox checked={topStory} onChange={(e) => setTopStory(e.target.checked)} />}
                label="Top story"
            />
        </TabPanel>
    );
}