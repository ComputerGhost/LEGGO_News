import { $isLinkNode } from "@lexical/link";
import { useLexicalComposerContext } from "@lexical/react/LexicalComposerContext";
import { $getSelection, $isRangeSelection, COMMAND_PRIORITY_LOW, FORMAT_TEXT_COMMAND, SELECTION_CHANGE_COMMAND } from 'lexical';
import { useEffect, useState } from "react";
import { Button, Dropdown, ToggleButton } from "react-bootstrap";
import styles from '../TextEditor.module.css';
import { getSelectedNode } from "../utility";

export default function FormattingToolbar() {
    const [editor] = useLexicalComposerContext();
    const [isBold, setIsBold] = useState(false);
    const [isItalic, setIsItalic] = useState(false);
    const [isUnderline, setIsUnderline] = useState(false);
    const [isStrikethrough, setIsStrikethrough] = useState(false);
    const [isLink, setIsLink] = useState(false);

    function updateToolbar() {
        const selection = $getSelection();
        if ($isRangeSelection(selection)) {
            // Update text format
            setIsBold(selection.hasFormat("bold"));
            setIsItalic(selection.hasFormat("italic"));
            setIsUnderline(selection.hasFormat("underline"));
            setIsStrikethrough(selection.hasFormat("strikethrough"));

            // Update links
            const node = getSelectedNode(selection);
            const parent = node.getParent();
            setIsLink($isLinkNode(parent) || $isLinkNode(node));
        }
    }

    useEffect(() => {
        editor.registerCommand(
            SELECTION_CHANGE_COMMAND,
            () => {
                updateToolbar();
                return false;
            },
            COMMAND_PRIORITY_LOW
        );
    }, [editor]);

    return (
        <div className='btn-group me-2 border'>
            <ToggleButton
                aria-label="Format bold"
                checked={isBold}
                id="toggle-bold"
                onChange={() => editor.dispatchCommand(FORMAT_TEXT_COMMAND, "bold")}
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
            <Button
                aria-label="Insert link"
                variant="light"
            >
                <i className='fa-solid fa-fw fa-link'></i>
            </Button>
            <Dropdown>
                <Dropdown.Toggle variant="light">
                    <i className='fa-solid fa-fw fa-paintbrush'></i>
                </Dropdown.Toggle>
                <Dropdown.Menu>
                    <Dropdown.Item>
                        <i className='fa-solid fa-fw fa-square' style={{ color: '#f80' }}></i> Solji
                    </Dropdown.Item>
                    <Dropdown.Item>
                        <i className='fa-solid fa-fw fa-square' style={{ color: '#80f' }}></i> ELLY
                    </Dropdown.Item>
                    <Dropdown.Item>
                        <i className='fa-solid fa-fw fa-square' style={{ color: '#f00' }}></i> Hani
                    </Dropdown.Item>
                    <Dropdown.Item>
                        <i className='fa-solid fa-fw fa-square' style={{ color: '#080' }}></i> Hyelin
                    </Dropdown.Item>
                    <Dropdown.Item>
                        <i className='fa-solid fa-fw fa-square' style={{ color: '#06c' }}></i> Jeonghwa
                    </Dropdown.Item>
                </Dropdown.Menu>
            </Dropdown>
        </div>
    );
}

