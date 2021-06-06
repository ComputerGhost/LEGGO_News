import { QueryFunctionContext, useInfiniteQuery, useMutation, useQuery } from "react-query";
import { SearchParameters, SearchResults, getNextPageParam } from "./search";


export interface MediaSummary {
    id: number,
    caption: string,
    credit: string,
    creditUrl: string,
    originalUrl: string,
    thumbnailUrl: string,
    smallSizeUrl: string,
    mediumSizeUrl: string,
    largeSizeUrl: string,
}


export function useMedia(search: string) {
    const getTags = async ({ pageParam }: QueryFunctionContext) => {
        const parameters = new URLSearchParams({
            query: search,
            offset: pageParam?.toString() ?? 0,
        });
        const endpoint = `${process.env.REACT_APP_API_URL}/media?`;
        const response = await fetch(endpoint + '?' + parameters);
        return await response.json() as SearchResults<MediaSummary>;
    }
    return useInfiniteQuery(['media', search], getTags, { getNextPageParam });
}

export function useUploadMedia() {
    return useMutation(async (file: File) => {
        const formData = new FormData();
        formData.append('file', file);

        const endpoint = `${process.env.REACT_APP_API_URL}/media`;
        const response = await fetch(endpoint, {
            method: 'POST',
            body: formData,
        });
        return await response.json() as MediaSummary;
    });
}
