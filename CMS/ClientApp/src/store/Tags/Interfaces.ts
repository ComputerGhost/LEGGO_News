
/* Matches API data types */

export interface TagSummary {
    id: number,
    name: string,
    description: string,
}

export interface TagDetails {
    id: number,
    name: string,
    description: string,
}

export interface TagSaveData {
    name: string,
    description: string,
}


/* Data structure of our store */

export interface TagsState {
    key: number,
    search: string,
    data: TagSummary[],
    totalCount: number,
    isLoading: boolean,
}
