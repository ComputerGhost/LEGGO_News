import { QueryFunctionContext, useInfiniteQuery, useMutation, useQuery } from "react-query";
import { SearchResults, getNextPageParam } from "./search";


export interface UserDetails {
    id: number,
    username: string,
    displayName: string,
}

export interface UserSaveData {
    username: string,
    password?: string, // To not change the password, don't set this.
    displayName: string,
}

export interface UserSummary {
    id: number,
    username: string,
    displayName: string,
}


export function useUsers(search: string) {
    const getUsers = async ({ pageParam }: QueryFunctionContext) => {
        const parameters = new URLSearchParams({
            query: search,
            offset: pageParam?.toString() ?? 0,
        });
        const endpoint = `${process.env.REACT_APP_API_URL}/users`;
        const response = await fetch(endpoint + '?' + parameters);
        return await response.json() as SearchResults<UserSummary>;
    }
    return useInfiniteQuery(['users', search], getUsers, { getNextPageParam });
}

export function useUser(userId: number | undefined) {
    const getUser = async () => {
        if (!userId)
            return undefined;
        const endpoint = `${process.env.REACT_APP_API_URL}/users/${userId}`;
        const response = await fetch(endpoint);
        return await response.json() as UserDetails;
    }
    return useQuery(['users', userId], getUser);
}

export function useCreateUser() {
    return useMutation(async (data: UserSaveData) => {
        const endpoint = `${process.env.REACT_APP_API_URL}/users`;
        const response = await fetch(endpoint, {
            method: 'POST',
            body: JSON.stringify(data)
        });
        return await response.json() as UserDetails;
    });
}

export function useUpdateUser(userId: number | undefined) {
    return useMutation(async (data: UserSaveData) => {
        if (!userId)
            throw new Error('userId must be defined to update.');
        const endpoint = `${process.env.REACT_APP_API_URL}/users/${userId}`;
        const response = await fetch(endpoint, {
            method: 'PUT',
            body: JSON.stringify(data)
        });
        return await response.json() as UserDetails;
    });
}
