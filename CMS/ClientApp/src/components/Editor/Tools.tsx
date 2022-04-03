import Header from '@editorjs/header';
const List = require('@editorjs/list');

export const EDITOR_JS_TOOLS = {
    header: {
        class: Header,
        config: {
            levels: [2, 3, 4],
            defaultLevel: 2
        }
    },
    list: {
        class: List,
        inlineToolbar: true,
        config: {
            defaultStyle: 'unordered'
        }
    }
};
