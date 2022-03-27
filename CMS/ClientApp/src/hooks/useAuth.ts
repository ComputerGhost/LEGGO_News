import { useCookies } from 'react-cookie';

export interface CurrentUser {
    email: string,
    name: string,
    scope: string[],
    username: string,
}

export function useAuth() {
    var [cookies] = useCookies(['oidc-user']);
    var userCookie = cookies['oidc-user'] as CurrentUser;
    return userCookie;
}
