import RestApi from '../internal/HookedApi';

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
}

const tags = new RestApi<TagSummary, TagDetails, TagSaveData>('tags');

export function useTags(search: string) {
    return tags.useItems(search);
}

export function useTag(tagId: number | undefined) {
    return tags.useItem(tagId);
}

export function useCreateTag() {
    return tags.useCreateItem();
}

export function useUpdateTag(tagId: number | undefined) {
    if (!tagId) {
        throw new Error('tagId must be defined to update.');
    }
    return tags.useUpdateItem(tagId);
}

export function useDeleteTag(tagId: number) {
    return tags.useDeleteItem(tagId);
}
