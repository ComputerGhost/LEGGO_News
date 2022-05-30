import { User, UserManager } from 'oidc-client-ts';

export default class {
    private userManager: UserManager;

    constructor() {
        this.userManager = new UserManager({
            authority: process.env.REACT_APP_OIDC_AUTHORITY!,
            client_id: process.env.REACT_APP_OIDC_CLIENT_ID!,
            redirect_uri: `${process.env.REACT_APP_CLIENT_ROOT}signin-callback.html`,
            silent_redirect_uri: `${process.env.REACT_APP_CLIENT_ROOT}silent-renew.html`,
            post_logout_redirect_uri: process.env.REACT_APP_CLIENT_ROOT,
            response_type: 'code',
            scope: 'openid profile api',
        });
    }

    public async getUser(): Promise<User | null> {
        return this.userManager.getUser();
    }

    public async login(): Promise<void> {
        return this.userManager.signinRedirect();
    }

    public async logout(): Promise<void> {
        return this.userManager.signoutRedirect();
    }

    public async renewToken(): Promise<User | null> {
        return this.userManager.signinSilent();
    }
}
