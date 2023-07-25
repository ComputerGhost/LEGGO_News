export interface PagedResults<T> {
    // from api
    data: T[],
    nextCursor: string | null,

    // not from api
    search: string | null,
}
