
export interface SearchParameters {
    key: number,
    query: string,
    offset: number,
    count: number,
}

export interface SearchResults<T> {
    key: number,
    offset: number,
    count: number,
    totalCount: number,
    data: T[],
}


export function getNextPageParam<T>(lastPage: SearchResults<T>) {
    if (lastPage.offset + lastPage.count === lastPage.totalCount)
        return undefined;
    return lastPage.offset + lastPage.count;
}
