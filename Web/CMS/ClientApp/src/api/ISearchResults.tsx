export interface ISearchResults<T> {
    key: number,
    offset: number,
    count: number,
    totalCount: number,
    data: T[],
}
