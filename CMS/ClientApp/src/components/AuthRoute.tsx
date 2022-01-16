import { ReactElement } from 'react';
import { Route, RouteComponentProps } from 'react-router-dom';
import { Redirect, StaticContext } from 'react-router';
import { useAuth } from '../hooks/useAuth';


interface IProps {
    exact: boolean,
    path: string,
    render?: ({ match }: RouteComponentProps<any, StaticContext, any>) => JSX.Element,
    children?: ReactElement
}

export default function AuthRoute({
    exact,
    path,
    render,
    children
}: IProps) {

    var user = useAuth();

    if (!user || !user.email) {
        // This needs replaced later but for now just use the backend to force a refresh.
        window.location.reload();
    }

    return (
        <Route exact={exact} path={path} render={render}>
            {children}
        </Route>
    );
}
