import { LexicalComposer } from "@lexical/react/LexicalComposer";
import { HistoryPlugin } from "@lexical/react/LexicalHistoryPlugin";
import { RichTextPlugin } from "@lexical/react/LexicalRichTextPlugin";
import { ContentEditable } from "@lexical/react/LexicalContentEditable";
import LexicalErrorBoundary from "@lexical/react/LexicalErrorBoundary";
import Toolbar from "./Toolbars/Toolbar";
import styles from './TextEditor.module.css';

export default function TextEditor() {
    function handleError(error: Error) {
        console.error(error);
    }

    const initialConfig = {
        namespace: 'MyEditor',
        onError: handleError,
    };

    return (
        <LexicalComposer initialConfig={initialConfig}>
            <Toolbar />
            <div className={styles.contentContainer}>
                <RichTextPlugin
                    contentEditable={<ContentEditable className={styles.content} />}
                    placeholder={<div className={styles.contentPlaceholder}>Start your article here.</div>}
                    ErrorBoundary={LexicalErrorBoundary}
                />
                <HistoryPlugin />
            </div>
        </LexicalComposer>
    );
}
