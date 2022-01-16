import { OutputBlockData } from "@editorjs/editorjs";
import { QueryFunctionContext, useInfiniteQuery, useMutation, useQuery } from "react-query";
import { SearchResults, getNextPageParam } from "./search";


export interface ArticleDetails {
    id: number,
    title: string,
    editorVersion: string,
    content: string,
}

export interface ArticleSaveData {
    title: string,
    editorVersion: string,
    content: string,
}

export interface ArticleSummary {
    id: number,
    title: string,
}


export function useArticles(search: string) {
    const getArticles = async ({ pageParam }: QueryFunctionContext) => {
        const parameters = new URLSearchParams({
            query: search,
            offset: pageParam?.toString() ?? 0,
        });
        const endpoint = `${process.env.REACT_APP_API_URL}/articles`;
        const response = await fetch(endpoint + '?' + parameters);
        return await response.json() as SearchResults<ArticleSummary>;
    }
    return useInfiniteQuery(['articles', search], getArticles, { getNextPageParam });
}

export function useArticle(articleId: number | undefined) {
    const getArticle = async () => {
        if (!articleId)
            return undefined;
        const endpoint = `${process.env.REACT_APP_API_URL}/articles/${articleId}`;
        const response = await fetch(endpoint);
        return await response.json() as ArticleDetails;
    }
    return useQuery(['articles', articleId], getArticle);
}

export function useCreateArticle() {
    return useMutation(async (data: ArticleSaveData) => {
        const endpoint = `${process.env.REACT_APP_API_URL}/articles`;
        const response = await fetch(endpoint, {
            method: 'POST',
            headers: [['Content-Type', 'application/json']],
            body: JSON.stringify(data),
        });
        return await response.json() as ArticleSummary;
    });
}

export function useUpdateArticle(articleId: number | undefined) {
    return useMutation(async (data: ArticleSaveData) => {
        if (!articleId)
            throw new Error('articleId must be defined to update.');
        const endpoint = `${process.env.REACT_APP_API_URL}/articles/${articleId}`;
        const response = await fetch(endpoint, {
            method: 'PUT',
            headers: [['Content-Type', 'application/json']],
            body: JSON.stringify(data)
        });
        return await response.json() as ArticleSummary;
    });
}

export function useDeleteCharacter(articleId: number) {
    return useMutation(async () => {
        const endpoint = `${process.env.REACT_APP_API_URL}/articles/${articleId}`;
        const response = await fetch(endpoint, {
            method: 'DELETE',
        });
    });
}
