import React, { useState } from 'react';
import TabPanel from '../../components/TabPanel';
import TagsInput from '../../components/TagsInput';

interface IProps {
    tabIndex: string,
}

export default function ({ tabIndex }: IProps) {
    const [tags, setTags] = useState<string[]>([]);

    return (
        <TabPanel value={tabIndex}>
            <TagsInput
                label='Tags'
                onChange={setTags}
                value={tags}
            />
        </TabPanel>
    );
}
