import AuthService from '../../services/AuthenticationService';
import ApiError from '../ApiError';

export default class {
    private authService: AuthService;

    public constructor() {
        this.authService = new AuthService();
    }

    public async delete(uri: string) {
        return this.fetch(uri, {
            method: 'DELETE',
        });
    }

    public async get(uri: string) {
        return this.fetch(uri, {
            method: 'GET',
        });
    }

    public async post(uri: string, payload: string) {
        return this.fetch(uri, {
            method: 'POST',
            body: payload,
        });
    }

    public async put(uri: string, payload: string) {
        return this.fetch(uri, {
            method: 'PUT',
            body: payload,
        });
    }

    public async fetch(
        relativeUri: string,
        options: RequestInit,
        contentType?: string
    ) {
        // Ensure user is signed in.
        let user = await this.authService.getUser();
        if (user && !user.access_token) {
            user = await this.authService.renewToken();
        }
        if (!(user && user.access_token)) {
            throw new Error('User is not signed in.');
        }

        // Make the fetch request.
        const requestUri = `${process.env.REACT_APP_API_URL}${relativeUri}`;
        const requestOptions = {
            ...options,
            headers: [
                ['Authorization', `Bearer ${user.access_token}`],
                ['Content-Type', contentType ?? 'application/json'],
                ['X-Requested-With', 'XMLHttpRequest'],
            ],
            redirect: 'manual' as RequestRedirect,
        };
        const response = await fetch(requestUri, requestOptions);

        // Verify the response and return.
        if (!response.ok) {
            throw new ApiError(response.status);
        }

        // 끝!
        return response;
    }
}
