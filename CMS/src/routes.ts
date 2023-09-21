import Layout from "./components/Layout";
import { articleListLoader } from "./loaders/articleListLoader";
import { tagListLoader } from "./loaders/tagListLoader";
import ArticleEdit from "./pages/ArticleEdit";
import ArticleList from "./pages/ArticleList";
import TagList from "./pages/TagList";

export const routes = [
    {
        path: '/',
        Component: Layout,
        children: [
            {
                path: 'tags',
                children: [
                    {
                        index: true,
                        Component: TagList,
                        loader: tagListLoader,
                    },
                ],
            },
            {
                path: 'articles',
                children: [
                    {
                        index: true,
                        Component: ArticleList,
                        loader: articleListLoader,
                    },
                    {
                        path: 'new',
                        Component: ArticleEdit,
                    }
                ],
            },
        ],
    }
];
