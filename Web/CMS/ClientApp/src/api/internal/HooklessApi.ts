import { ISearchResults } from '../ISearchResults';
import ApiFetch from './ApiFetch';

export default class <ItemSummary, ItemDetails, ItemSaveData> {
    private apiFetch: ApiFetch;

    private endpoint: string;

    public constructor(endpoint: string) {
        this.apiFetch = new ApiFetch();
        this.endpoint = endpoint;
    }

    public async createItem(data: ItemSaveData): Promise<ItemSummary> {
        const uri = this.endpoint;
        const payload = JSON.stringify(data);
        const response = await this.apiFetch.post(uri, payload);
        return response.json();
    }

    public async deleteItem(itemId: number): Promise<void> {
        const uri = `${this.endpoint}/${itemId}`;
        await this.apiFetch.delete(uri);
    }

    public async getItem(itemId: number): Promise<ItemDetails> {
        const uri = `${this.endpoint}/${itemId}`;
        const response = await this.apiFetch.get(uri);
        return response.json();
    }

    public async getItems(search: string, offset: number): Promise<ISearchResults<ItemSummary>> {
        const queryString = new URLSearchParams({
            query: search,
            offset: offset.toString(),
        });
        const uri = `${this.endpoint}/?${queryString}`;
        const response = await this.apiFetch.get(uri);
        return response.json();
    }

    public async updateItem(itemId: number, data: ItemSaveData): Promise<void> {
        const uri = `${this.endpoint}/${itemId}`;
        const payload = JSON.stringify(data);
        await this.apiFetch.put(uri, payload);
    }

    public async uploadItem(file: File): Promise<ItemSummary> {
        const uri = this.endpoint;
        const formData = new FormData();
        formData.append('file', file);
        const response = await this.apiFetch.fetch(uri, {
            method: 'POST',
            body: formData,
        }, 'multipart/form-data');
        return response.json();
    }
}
