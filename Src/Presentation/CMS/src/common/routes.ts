import ArticleEdit from "../pages/ArticleEdit";
import Layout from "./Layout";

export const routes = [
    {
        path: '/',
        Component: Layout,
        children: [
            {
                index: true,
                Component: ArticleEdit,
            }
        ],
    }
];
