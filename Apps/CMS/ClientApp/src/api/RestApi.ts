import { QueryFunctionContext, useInfiniteQuery, UseInfiniteQueryResult, useMutation, UseMutationResult, useQuery, UseQueryResult } from "react-query";
import AuthService from "../services/AuthService";
import ApiError from "./ApiError";


export interface ISearchParameters {
    key: number,
    query: string,
    offset: number,
    count: number,
}

export interface ISearchResults<T> {
    key: number,
    offset: number,
    count: number,
    totalCount: number,
    data: T[],
}


export default class <ItemSummary, ItemDetails, ItemSaveData>
{
    private authService: AuthService;
    private endpoint: string;

    public constructor(endpoint: string)
    {
        this.authService = new AuthService();
        this.endpoint = endpoint;
    }


    public async getItems(search: string, offset?: number): Promise<ISearchResults<ItemSummary>>
    {
        const uri = this.buildQueryUri({
            query: search,
            offset: offset?.toString() ?? "0"
        });
        const response = await this.fetch(uri, { method: 'GET' });
        return await response.json();
    }

    public useItems(search: string): UseInfiniteQueryResult<ISearchResults<ItemSummary>>
    {
        const getItems = async ({ pageParam }: QueryFunctionContext) =>
            await this.getItems(search, pageParam);

        const getNextPageParam = (lastPage: ISearchResults<ItemSummary>) => {
            if (lastPage.offset + lastPage.count === lastPage.totalCount)
                return undefined;
            return lastPage.offset + lastPage.count;
        };

        const key = [this.endpoint, search];
        return useInfiniteQuery(key, getItems, { getNextPageParam, useErrorBoundary: true });
    }


    public async getItem(itemId: number | undefined): Promise<ItemDetails | undefined>
    {
        if (!itemId)
            return undefined;
        const uri = this.buildItemUri(itemId);
        const response = await this.fetch(uri, { method: 'GET' });
        return await response.json() as ItemDetails;
    }

    public useItem(itemId: number | undefined): UseQueryResult<ItemDetails>
    {
        const getItem = async () => await this.getItem(itemId);
        const key = [this.endpoint, itemId];
        return useQuery(key, getItem, { useErrorBoundary: true });
    }


    public async createItem(data: ItemSaveData): Promise<ItemSummary>
    {
        const response = await this.fetch(this.endpoint, {
            method: 'POST',
            body: JSON.stringify(data)
        });
        return await response.json();
    }

    public useCreateItem(): UseMutationResult<ItemSummary, unknown, ItemSaveData>
    {
        const createItem = async (data: ItemSaveData) => this.createItem(data);
        return useMutation(createItem, { useErrorBoundary: true });
    }


    public async uploadItem(file: File): Promise<ItemSummary>
    {
        const formData = new FormData();
        formData.append('file', file);

        const response = await this.fetch(this.endpoint, {
            method: 'POST',
            body: formData,
        }, "multipart/form-data");
        return await response.json();
    }

    public useUploadItem(): UseMutationResult<ItemSummary, unknown, File>
    {
        const uploadItem = async (file: File) => this.uploadItem(file);
        return useMutation(uploadItem, { useErrorBoundary: true });
    }


    public async updateItem(itemId: number, data: ItemSaveData): Promise<void>
    {
        const uri = this.buildItemUri(itemId);
        await this.fetch(uri, {
            method: 'PUT',
            body: JSON.stringify(data)
        });
    }

    public useUpdateItem(itemId: number): UseMutationResult<void, unknown, ItemSaveData>
    {
        const updateItem = async (data: ItemSaveData) => this.updateItem(itemId, data);
        return useMutation(updateItem, { useErrorBoundary: true });
    }


    public async deleteItem(itemId: number): Promise<void>
    {
        const uri = this.buildItemUri(itemId);
        await this.fetch(uri, { method: 'DELETE' });
    }

    public useDeleteItem(itemId: number): UseMutationResult
    {
        const deleteItem = async () => await this.deleteItem(itemId);
        return useMutation(deleteItem, { useErrorBoundary: true });
    }


    private buildItemUri(itemId: number): string
    {
        return this.endpoint + '/' + itemId.toString();
    }

    private buildQueryUri(parameters: Record<string, string>): string
    {
        var queryString = new URLSearchParams(parameters).toString();
        return `${this.endpoint}/?${queryString}`;
    }

    private async fetch(relativeUri: string, options: RequestInit, contentType?: string)
    {
        // Ensure user is signed in.
        var user = await this.authService.getUser();
        if (user && !user.access_token) {
            user = await this.authService.renewToken();
        }
        if (!(user && user.access_token)) {
            throw new Error("User is not signed in.");
        }

        // Make the fetch request.
        const requestUri = `${process.env.REACT_APP_API_URL}${relativeUri}`;
        const requestOptions = {
            ...options,
            headers: [
                ['Authorization', 'Bearer ' + user.access_token],
                ['Content-Type', contentType ?? 'application/json'],
                ['X-Requested-With', 'XMLHttpRequest']
            ],
            redirect: 'manual' as RequestRedirect
        };
        var response = await fetch(requestUri, requestOptions);

        // Verify the response and return.
        if (!response.ok) {
            throw new ApiError(response.status);
        }

        // 끝!
        return response;
    }
}
