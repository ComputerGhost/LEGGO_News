import Header from '@editorjs/header';

const List = require('@editorjs/list');
const Quote = require('@editorjs/quote');

export default {
    header: {
        class: Header,
        config: {
            levels: [2, 3, 4],
            defaultLevel: 2,
        },
    },
    list: {
        class: List,
        inlineToolbar: true,
        config: {
            defaultStyle: 'unordered',
        },
    },
    quote: {
        class: Quote,
        inlineToolbar: true,
        config: {
            quotePlaceholder: 'Enter a quote',
            captionPlaceholder: 'Quote\'s author',
        },
    },
};
