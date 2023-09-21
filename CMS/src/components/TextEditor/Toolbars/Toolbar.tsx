import styles from '../TextEditor.module.css';
import BlockToolbar from './BlockToolbar';
import FormattingToolbar from './FormattingToolbar';
import HistoryToolbar from './HistoryToolbar';

export default function Toolbar()
{
    return (
        <>
            <div className={styles.toolbar}>
                <HistoryToolbar />
                <BlockToolbar />
                <FormattingToolbar />
            </div>
        </>
    );
}
