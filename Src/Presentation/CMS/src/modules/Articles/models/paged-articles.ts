
export interface ArticleSummary {
    id: number,
    title: string,
    abstract: string,
};

export interface PagedArticles {
    results: ArticleSummary[],
    next?: string,
};
