import { UserManager } from "oidc-client-ts";
import { settings } from "../settings";

export default async function fetch(
    relativeUri: string,
    options: RequestInit,
    contentType?: string,
): Promise<any> {
    // Need to send the JWT token for auth
    var userManager = new UserManager(settings.oidc);
    var user = await userManager.getUser();
    var jwtToken = user?.id_token;

    // Make the fetch request.
    const requestUri = `${process.env.REACT_APP_API_URL}${relativeUri}`;
    const requestOptions: RequestInit = {
        ...options,
        headers: [
            ['Authorization', `Bearer ${jwtToken}`],
            ['Content-Type', contentType ?? 'application/json'],
            ['X-Requested-With', 'XMLHttpRequest'],
        ],
    };
    const response: any = await window.fetch(requestUri, requestOptions);

    // We need to handle !response.ok here
    // I'll do that later too.
    /// tbd.

    return response;
}
