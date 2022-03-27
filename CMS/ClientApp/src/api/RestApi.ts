import { QueryFunctionContext, useInfiniteQuery, UseInfiniteQueryResult, useMutation, UseMutationResult, useQuery, UseQueryResult } from "react-query";
import { generatePath } from "react-router";
import { User } from "oidc-client-ts";
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
    private endpoint: string;

    public constructor(endpoint: string) {
        this.endpoint = endpoint;
    }


    public async getItems(search: string, offset?: number): Promise<ISearchResults<ItemSummary>> {
        const uri = this.buildQueryUri({
            query: search,
            offset: offset?.toString() ?? "0"
        });
        const response = await this.fetch(uri, { method: 'GET' });
        return await response.json();
    }

    public useItems(search: string): UseInfiniteQueryResult<ISearchResults<ItemSummary>> {
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


    public async getItem(itemId: number | undefined): Promise<ItemDetails | undefined> {
        if (!itemId)
            return undefined;
        const uri = this.buildItemUri(itemId);
        const response = await this.fetch(uri, { method: 'GET' });
        return await response.json() as ItemDetails;
    }

    public useItem(itemId: number | undefined): UseQueryResult<ItemDetails> {
        const getItem = async () => await this.getItem(itemId);
        const key = [this.endpoint, itemId];
        return useQuery(key, getItem, { useErrorBoundary: true });
    }


    public async createItem(data: ItemSaveData): Promise<ItemSummary> {
        const response = await this.fetch(this.endpoint, {
            method: 'POST',
            body: JSON.stringify(data)
        });
        return await response.json();
    }

    public useCreateItem(): UseMutationResult<ItemSummary, unknown, ItemSaveData> {
        const createItem = async (data: ItemSaveData) => this.createItem(data);
        return useMutation(createItem, { useErrorBoundary: true });
    }


    public async uploadItem(file: File): Promise<ItemSummary> {
        const formData = new FormData();
        formData.append('file', file);

        const response = await this.fetch(this.endpoint, {
            method: 'POST',
            body: formData,
        }, "multipart/form-data");
        return await response.json();
    }

    public useUploadItem(): UseMutationResult<ItemSummary, unknown, File> {
        const uploadItem = async (file: File) => this.uploadItem(file);
        return useMutation(uploadItem, { useErrorBoundary: true });
    }


    public async updateItem(itemId: number, data: ItemSaveData): Promise<ItemSummary> {
        const uri = this.buildItemUri(itemId);
        const response = await this.fetch(uri, {
            method: 'PUT',
            body: JSON.stringify(data)
        });
        return await response.json();
    }

    public useUpdateItem(itemId: number): UseMutationResult<ItemSummary, unknown, ItemSaveData> {
        const updateItem = async (data: ItemSaveData) => this.updateItem(itemId, data);
        return useMutation(updateItem, { useErrorBoundary: true });
    }


    public async deleteItem(itemId: number): Promise<void> {
        const uri = this.buildItemUri(itemId);
        await this.fetch(uri, { method: 'DELETE' });
    }

    public useDeleteItem(itemId: number): UseMutationResult {
        const deleteItem = async () => await this.deleteItem(itemId);
        return useMutation(deleteItem, { useErrorBoundary: true });
    }


    private buildItemUri(itemId: number): string {
        return this.endpoint + '/' + itemId.toString();
    }

    private buildQueryUri(parameters: Record<string, string>): string {
        return generatePath(this.endpoint + '/', parameters);
    }

    private async fetch(relativeUri: string, options: RequestInit, contentType?: string) {
        const response = await this.makeFetchRequest(relativeUri, options, contentType);
        this.checkFetchResponse(response);
        return response;
    }

    private getUser(): User|null {
        const oidcStorage = localStorage.getItem(`oidc.user:http://localhost:9011:dc487e3d-1a6a-49b6-a957-150dd821bbd8`);
        if (!oidcStorage)
            return null;
        return User.fromStorageString(oidcStorage);
    }

    private async makeFetchRequest(relativeUri: string, options: RequestInit, contentType?: string) {
        const requestUri = `${process.env.REACT_APP_API_URL}/${relativeUri}`;
        const requestOptions = {
            ...options,
            headers: [
                ['Bearer', `Bearer ${this.getUser()?.access_token}`],
                ['Content-Type', contentType ?? 'application/json'],
                ['X-Requested-With', 'XMLHttpRequest']
            ],
            redirect: 'manual' as RequestRedirect
        };
        var response = await fetch(requestUri, requestOptions);
        this.checkFetchResponse(response);
        return response;
    }

    private checkFetchResponse(response: Response) {
        if (!response.ok) {
            throw new ApiError(response.status);
        }
    }
}
