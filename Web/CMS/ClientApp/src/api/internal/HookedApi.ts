import { QueryClient, useInfiniteQuery, UseInfiniteQueryResult, useMutation, UseMutationResult, useQuery, UseQueryResult } from 'react-query';
import { ISearchResults } from '../ISearchResults';
import HooklessApi from './HooklessApi';

export default class <ItemSummary, ItemDetails, ItemSaveData> {
    private endpoint: string;

    private hooklessApi: HooklessApi<ItemSummary, ItemDetails, ItemSaveData>;

    private queryClient: QueryClient;

    private defaultMutationOptions = {
        useErrorBoundary: true,
    };

    public constructor(endpoint: string) {
        this.endpoint = endpoint;
        this.hooklessApi = new HooklessApi(endpoint);
        this.queryClient = new QueryClient();
    }

    public useCreateItem(): UseMutationResult<ItemSummary, unknown, ItemSaveData> {
        this.resetCache();
        return useMutation(
            this.hooklessApi.createItem,
            this.defaultMutationOptions
        );
    }

    public useDeleteItem(itemId: number): UseMutationResult {
        this.resetCache();
        return useMutation(
            async () => this.hooklessApi.deleteItem(itemId),
            this.defaultMutationOptions
        );
    }

    public useItem(itemId: number | undefined): UseQueryResult<ItemDetails> {
        const cacheKey = [this.endpoint, itemId];
        return useQuery(
            cacheKey,
            async () => itemId && this.hooklessApi.getItem(itemId),
            this.defaultMutationOptions
        );
    }

    public useItems(search: string): UseInfiniteQueryResult<ISearchResults<ItemSummary>> {
        const cacheKey = [this.endpoint, search];
        const getNextPageParam = (lastPage: ISearchResults<ItemSummary>) => {
            if (lastPage.offset + lastPage.count === lastPage.totalCount) {
                return undefined;
            }
            return lastPage.offset + lastPage.count;
        };
        const options = {
            ...this.defaultMutationOptions,
            getNextPageParam,
        };
        return useInfiniteQuery(
            cacheKey,
            async (key, offset?: number) => this.hooklessApi.getItems(search, offset ?? 0),
            options
        );
    }

    public useUpdateItem(itemId: number): UseMutationResult<void, unknown, ItemSaveData> {
        this.resetCache();
        return useMutation(
            async (data) => this.hooklessApi.updateItem(itemId, data),
            this.defaultMutationOptions
        );
    }

    public useUploadItem(): UseMutationResult<ItemSummary, unknown, File> {
        this.resetCache();
        return useMutation(
            async (file) => this.hooklessApi.uploadItem(file),
            this.defaultMutationOptions
        );
    }

    private resetCache() {
        const cacheKey = [this.endpoint];
        this.queryClient.invalidateQueries(cacheKey);
    }
}
