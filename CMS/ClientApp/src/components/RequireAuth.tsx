import { ReactElement } from 'react';
import { useAuth } from '../hooks/useAuth';


interface IProps {
    children: ReactElement
}

export default function RequireAuth({
    children
}: IProps) {

    var user = useAuth();

    if (!user || !user.email) {
        // This needs replaced later but for now just use the backend to force a refresh.
        window.location.reload();
    }

    return children;
}
