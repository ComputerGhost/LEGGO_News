import ArticleEdit from "./components/ArticleEdit";
import ArticleList from "./components/ArticleList";

const module = {
    routes: [
        {
            index: true,
            path: '/articles',
            Component: ArticleList,
        },
        {
            path: '/articles/new',
            Component: ArticleEdit,
        },
        {
            path: '/articles/:articleId',
            Component: ArticleEdit,
        },
    ],
    name: 'Articles',
    icon: 'fa-pen-nib',
};

export default module;
