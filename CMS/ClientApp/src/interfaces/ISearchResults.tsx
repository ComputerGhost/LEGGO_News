
export default interface ISearchResults<T> {
    key: number,
    offset: number,
    count: number,
    totalCount: number,
    data: Array<T>,
}
