import { useCookies } from 'react-cookie';

export interface CurrentUser {
    email: string,
    name: string,
    scope: string[],
    username: string,
}

export function useAuth() {
    var [cookies] = useCookies(['jwt']);
    var jwtCookie = cookies.jwt as CurrentUser;
    return jwtCookie;
}
