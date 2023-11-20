import ArticleEdit from "./components/ArticleEdit";
import ArticleList from "./components/ArticleList";
import ArticleListLoader from "./loaders/article-list-loader";

const module = {
    routes: [
        {
            index: true,
            path: '/articles',
            Component: ArticleList,
            loader: ArticleListLoader,
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
