import { QueryFunctionContext, useInfiniteQuery, useMutation, useQuery } from "react-query";
import { SearchResults, getNextPageParam } from "./search";


export interface TagDetails {
    id: number,
    name: string,
    description: string,
}

export interface TagSaveData {
    name: string,
    description: string,
}

export interface TagSummary {
    id: number,
    name: string,
    description: string,
}


export function useTags(search: string) {
    const getTags = async ({ pageParam }: QueryFunctionContext) => {
        const parameters = new URLSearchParams({
            query: search,
            offset: pageParam.toString(),
        });
        const endpoint = `${process.env.REACT_APP_API_URL}/tags?`;
        const response = await fetch(endpoint + '?' + parameters);
        return await response.json() as SearchResults<TagSummary>;
    }
    return useInfiniteQuery(['tags', search], getTags, { getNextPageParam });
}

export function useTag(tagId: number | undefined) {
    const getTag = async () => {
        if (!tagId)
            return undefined;
        const endpoint = `${process.env.REACT_APP_API_URL}/tags/${tagId}`;
        const response = await fetch(endpoint);
        return await response.json() as TagDetails;
    }
    return useQuery(['tags', tagId], getTag);
}

export function useCreateTag() {
    return useMutation(async (data: TagSaveData) => {
        const endpoint = `${process.env.REACT_APP_API_URL}/tags`;
        const response = await fetch(endpoint, {
            method: 'POST',
            body: JSON.stringify(data)
        });
        return await response.json() as TagDetails;
    });
}

export function useUpdateTag(tagId: number | undefined) {
    return useMutation(async (data: TagSaveData) => {
        if (!tagId)
            throw new Error('tagId must be defined to update.');
        const endpoint = `${process.env.REACT_APP_API_URL}/tags/${tagId}`;
        const response = await fetch(endpoint, {
            method: 'PUT',
            body: JSON.stringify(data)
        });
        return await response.json() as TagDetails;
    });
}

export function useDeleteTag(tagId: number) {
    return useMutation(async () => {
        const endpoint = `${process.env.REACT_APP_API_URL}/tags/${tagId}`;
        const response = await fetch(endpoint, {
            method: 'DELETE',
        });
    });
}
