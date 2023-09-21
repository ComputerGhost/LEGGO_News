export const settings = {
    oidc: {
        authority: process.env.REACT_APP_OIDC_AUTHORITY!,
        client_id: process.env.REACT_APP_OIDC_CLIENT_ID!,
        post_logout_redirect_uri: process.env.REACT_APP_CLIENT_ROOT!,
        redirect_uri: process.env.REACT_APP_CLIENT_ROOT!,
        scope: 'openid profile api',
    },
};