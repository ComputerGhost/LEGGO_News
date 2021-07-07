import { QueryFunctionContext, useInfiniteQuery, useMutation, useQuery } from "react-query";
import { SearchResults, getNextPageParam } from "./search";


export interface CommentDetails {
    id: number,
    text: string,
}

export interface CommentSaveData {
    text: string,
}

export interface CommentSummary {
    id: number,
    text: string,
}


export function useComments(search: string) {
    const getComments = async ({ pageParam }: QueryFunctionContext) => {
        const parameters = new URLSearchParams({
            query: search,
            offset: pageParam?.toString() ?? 0,
        });
        const endpoint = `${process.env.REACT_APP_API_URL}/comments?`;
        const response = await fetch(endpoint + '?' + parameters);
        return await response.json() as SearchResults<CommentSummary>;
    }
    return useInfiniteQuery(['comments', search], getComments, { getNextPageParam });
}

export function useArticle(commentId: number | undefined) {
    const getComment = async () => {
        if (!commentId)
            return undefined;
        const endpoint = `${process.env.REACT_APP_API_URL}/articles/${commentId}`;
        const response = await fetch(endpoint);
        return await response.json() as CommentDetails;
    }
    return useQuery(['comments', commentId], getComment);
}
