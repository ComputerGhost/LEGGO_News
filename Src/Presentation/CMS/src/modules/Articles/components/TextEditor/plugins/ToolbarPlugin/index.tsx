import { ButtonToolbar } from 'react-bootstrap';
import styles from '../../TextEditor.module.css';
import BlockToolbar from './BlockToolbar';
import HistoryToolbar from './HistoryToolbar';

export default function ToolbarPlugin()
{
    return (
        <>
            <ButtonToolbar className={styles.toolbar}>
                <HistoryToolbar />
                <BlockToolbar />
            </ButtonToolbar>
        </>
    );
}
