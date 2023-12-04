import { $isLinkNode, TOGGLE_LINK_COMMAND } from "@lexical/link";
import { useLexicalComposerContext } from "@lexical/react/LexicalComposerContext";
import { $getSelection, $isParagraphNode, $isRangeSelection, $isTextNode, FORMAT_TEXT_COMMAND, LexicalEditor } from "lexical";
import { useCallback, useEffect, useRef, useState } from "react";
import { ButtonGroup, ToggleButton } from "react-bootstrap";
import { createPortal } from "react-dom";
import { getDOMRangeRect } from "../../../../../../external/meta/getDOMRangeRect";
import { getSelectedNode } from "../../../../../../external/meta/getSelectedNode";
import { setFloatingElemPosition } from "../../../../../../external/meta/setFloatingElemPosition";
import styles from "./index.module.css";

function FloatingFormatToolbar({
    editor,
    anchorElem,
    isBold,
    isItalic,
    isLink,
    isStrikethrough,
    isUnderline,
}: {
    editor: LexicalEditor,
    anchorElem: HTMLElement,
    isBold: boolean,
    isItalic: boolean,
    isLink: boolean,
    isStrikethrough: boolean,
    isUnderline: boolean,
}): JSX.Element {
    const toolbarRootRef = useRef<HTMLDivElement | null>(null);

    const insertLink = useCallback(() => {
        if (!isLink) {
            editor.dispatchCommand(TOGGLE_LINK_COMMAND, 'https://');
        } else {
            editor.dispatchCommand(TOGGLE_LINK_COMMAND, null);
        }
    }, [editor, isLink]);

    useEffect(() => {
        if (toolbarRootRef.current === null) {
            return;
        }
        const nativeSelection = window.getSelection()!;
        const rootElement = editor.getRootElement()!;
        const rangeRect = getDOMRangeRect(nativeSelection, rootElement);
        setFloatingElemPosition(rangeRect, toolbarRootRef.current, anchorElem, isLink);
    }, [editor, anchorElem, isLink]);

    return (
        <ButtonGroup ref={toolbarRootRef} className={styles.toolbar}>
            <ToggleButton
                aria-label="Format bold"
                checked={isBold}
                id="toggle-bold"
                onClick={() => editor.dispatchCommand(FORMAT_TEXT_COMMAND, 'bold')}
                type="checkbox"
                value="1"
                variant="light"
            >
                <i className='fa-solid fa-fw fa-bold'></i>
            </ToggleButton>
            <ToggleButton
                aria-label="Format italics"
                checked={isItalic}
                id="toggle-italics"
                onClick={() => editor.dispatchCommand(FORMAT_TEXT_COMMAND, "italic")}
                type="checkbox"
                value="1"
                variant="light"
            >
                <i className='fa-solid fa-fw fa-italic'></i>
            </ToggleButton>
            <ToggleButton
                aria-label="Format underline"
                checked={isUnderline}
                id="toggle-underline"
                onClick={() => editor.dispatchCommand(FORMAT_TEXT_COMMAND, "underline")}
                type="checkbox"
                value="1"
                variant="light"
            >
                <i className='fa-solid fa-fw fa-underline'></i>
            </ToggleButton>
            <ToggleButton
                aria-label="Format strikethrough"
                checked={isStrikethrough}
                id="toggle-strikethrough"
                onClick={() => editor.dispatchCommand(FORMAT_TEXT_COMMAND, "strikethrough")}
                type="checkbox"
                value="1"
                variant="light"
            >
                <i className='fa-solid fa-fw fa-strikethrough'></i>
            </ToggleButton>
            <ToggleButton
                aria-label="Insert link"
                checked={isLink}
                id="toggle-link"
                onClick={insertLink}
                type="checkbox"
                value="1"
                variant="light"
            >
                <i className='fa-solid fa-fw fa-link'></i>
            </ToggleButton>
        </ButtonGroup>
    );
}

function useFloatingFormatToolbar(
    editor: LexicalEditor,
    anchorElem: HTMLElement,
): JSX.Element | null {
    const [isText, setIsText] = useState(false);
    const [isLink, setIsLink] = useState(false);
    const [isBold, setIsBold] = useState(false);
    const [isItalic, setIsItalic] = useState(false);
    const [isUnderline, setIsUnderline] = useState(false);
    const [isStrikethrough, setIsStrikethrough] = useState(false);

    const updateStates = useCallback(() => {
        editor.getEditorState().read(() => {
            if (editor.isComposing()) {
                return;
            }

            const selection = $getSelection();
            const nativeSelection = window.getSelection()!;
            const rootElement = editor.getRootElement()!;
            if (!$isRangeSelection(selection) || !rootElement.contains(nativeSelection.anchorNode)) {
                setIsText(false);
                return;
            }

            const node = getSelectedNode(selection);

            setIsBold(selection.hasFormat('bold'));
            setIsItalic(selection.hasFormat('italic'));
            setIsUnderline(selection.hasFormat('underline'));
            setIsStrikethrough(selection.hasFormat('strikethrough'));
            setIsLink($isLinkNode(node.getParent()) || $isLinkNode(node));

            if (selection.getTextContent() !== "") {
                setIsText($isTextNode(node) || $isParagraphNode(node));
            } else {
                setIsText(false);
            }

            const rawTextContent = selection.getTextContent().replace(/\n/g, '');
            if (!selection.isCollapsed() && rawTextContent === '') {
                setIsText(false);
            }
        });
    }, [editor]);
    
    useEffect(() => {
        return editor.registerUpdateListener(() => {
            updateStates();
        });
    }, [editor, updateStates]);

    if (!isText) {
        return null;
    }

    return createPortal(
        <FloatingFormatToolbar
            editor={editor}
            anchorElem={anchorElem}
            isLink={isLink}
            isBold={isBold}
            isItalic={isItalic}
            isStrikethrough={isStrikethrough}
            isUnderline={isUnderline}
        />,
        anchorElem,
    );
}

export default function FloatingFormatToolbarPlugin({
    anchorElem = document.body,
}: {
    anchorElem?: HTMLElement;
}): JSX.Element | null {
    const [editor] = useLexicalComposerContext();
    return useFloatingFormatToolbar(editor, anchorElem);
}
