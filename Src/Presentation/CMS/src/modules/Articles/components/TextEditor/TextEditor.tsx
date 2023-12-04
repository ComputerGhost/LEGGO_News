import { LexicalComposer } from "@lexical/react/LexicalComposer";
import { HistoryPlugin } from "@lexical/react/LexicalHistoryPlugin";
import { RichTextPlugin } from "@lexical/react/LexicalRichTextPlugin";
import { ContentEditable } from "@lexical/react/LexicalContentEditable";
import LexicalErrorBoundary from "@lexical/react/LexicalErrorBoundary";
import ToolbarPlugin from "./plugins/ToolbarPlugin";
import styles from './TextEditor.module.css';
import { useState } from "react";
import FloatingTextFormatToolbarPlugin from "./plugins/FloatingFormatToolbarPlugin";

export default function TextEditor() {
    const [floatingAnchor, setFloatingAnchor] = useState<HTMLDivElement | null>(null);

    const onRef = (element: HTMLDivElement) => {
        if (element !== null) {
            setFloatingAnchor(element);
        }
    };

    function onError(error: Error) {
        console.error(error);
    }

    const initialConfig = {
        namespace: 'MyEditor',
        onError,
    };

    return (
        <LexicalComposer initialConfig={initialConfig}>
            <div className={styles.editorContainer} ref={onRef}>
                <RichTextPlugin
                    contentEditable={<ContentEditable className={styles.content} />}
                    placeholder={<div className={styles.contentPlaceholder}>Start your article here.</div>}
                    ErrorBoundary={LexicalErrorBoundary}
                />
                <ToolbarPlugin />
                <HistoryPlugin />
                {floatingAnchor && (
                    <>
                        <FloatingTextFormatToolbarPlugin anchorElem={floatingAnchor} />
                    </>
                )}
            </div>
        </LexicalComposer>
    );
}
