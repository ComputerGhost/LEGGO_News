import { createBrowserRouter, RouterProvider } from 'react-router-dom';
import Layout from './common/Layout';
import modules from './modules';

const routes = [
    {
        path: '/',
        Component: Layout,
        children: modules
            .map(m => m.routes)
            .reduce((a, b) => a.concat(b))
    }
];

const router = createBrowserRouter(routes);

export default function App() {
    return (
        <RouterProvider router={router} />
    );
}
