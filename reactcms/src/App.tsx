import { useAuth } from 'react-oidc-context';
import { createBrowserRouter, RouterProvider } from 'react-router-dom';
import Layout from './components/Layout';
import { articleListLoader } from './loaders/articleListLoader';
import { tagListLoader } from './loaders/tagListLoader';
import ArticleList from './pages/ArticleList';
import SignIn from './pages/SignIn';
import TagList from './pages/TagList';

const router = createBrowserRouter([
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
                    }
                ],
            }
        ],
    }
]);

export default function App() {
    const auth = useAuth();

    if (!auth.isAuthenticated) {
        return (<SignIn />);
    }

    return (
        <RouterProvider router={router} />
    );
}
