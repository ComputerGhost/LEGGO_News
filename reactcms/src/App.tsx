import { useAuth } from 'react-oidc-context';
import { createBrowserRouter, RouterProvider } from 'react-router-dom';
import SignIn from './pages/SignIn';
import { routes } from './routes';

const router = createBrowserRouter(routes);

export default function App() {
    const auth = useAuth();

    if (!auth.isAuthenticated) {
        return (<SignIn />);
    }

    return (
        <RouterProvider router={router} />
    );
}
