

export interface MediaItem {
    id: number,
    caption: string,
    credit: string,
    creditUrl: string,
    originalUrl: string,
}

export interface MediaState {
    key: number,
    search: string,
    data: MediaItem[],
    totalCount: number,
    isLoading: boolean,
}
