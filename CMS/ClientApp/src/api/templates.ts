import { QueryFunctionContext, useInfiniteQuery, useMutation, useQuery } from "react-query";
import { SearchResults, getNextPageParam } from "./search";


export interface TemplateDetails {
    id: number,
    name: string,
}

export interface TemplateSaveData {
    name: string,
}

export interface TemplateSummary {
    id: number,
    name: string,
}


export function useTemplates(search: string) {
    const getTemplates = async ({ pageParam }: QueryFunctionContext) => {
        const parameters = new URLSearchParams({
            query: search,
            offset: pageParam?.toString() ?? 0,
        });
        const endpoint = `${process.env.REACT_APP_API_URL}/templates?`;
        const response = await fetch(endpoint + '?' + parameters);
        return await response.json() as SearchResults<TemplateSummary>;
    }
    return useInfiniteQuery(['templates', search], getTemplates, { getNextPageParam });
}

export function useTemplate(templateId: number | undefined) {
    const getTemplate = async () => {
        if (!templateId)
            return undefined;
        const endpoint = `${process.env.REACT_APP_API_URL}/templates/${templateId}`;
        const response = await fetch(endpoint);
        return await response.json() as TemplateDetails;
    }
    return useQuery(['templates', templateId], getTemplate);
}

export function useCreateTemplate() {
    return useMutation(async (data: TemplateSaveData) => {
        const endpoint = `${process.env.REACT_APP_API_URL}/templates`;
        const response = await fetch(endpoint, {
            method: 'POST',
            body: JSON.stringify(data)
        });
        return await response.json() as TemplateDetails;
    });
}

export function useUpdateTemplate(templateId: number | undefined) {
    return useMutation(async (data: TemplateSaveData) => {
        if (!templateId)
            throw new Error('templateId must be defined to update.');
        const endpoint = `${process.env.REACT_APP_API_URL}/templates/${templateId}`;
        const response = await fetch(endpoint, {
            method: 'PUT',
            body: JSON.stringify(data)
        });
        return await response.json() as TemplateDetails;
    });
}

export function useDeleteTemplate(templateId: number) {
    return useMutation(async () => {
        const endpoint = `${process.env.REACT_APP_API_URL}/templates/${templateId}`;
        const response = await fetch(endpoint, {
            method: 'DELETE',
        });
    });
}
