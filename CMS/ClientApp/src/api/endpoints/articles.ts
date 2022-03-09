import RestApi from "../RestApi";


export interface ArticleDetails {
    id: number,
    title: string,
    format: string,
    content: string,
}

export interface ArticleSaveData {
    title: string,
    format: string,
    content: string,
}

export interface ArticleSummary {
    id: number,
    title: string,
}


const articles = new RestApi<ArticleSummary, ArticleDetails, ArticleSaveData>('articles');

export function useArticles(search: string) {
    return articles.useItems(search);
}

export function useArticle(articleId: number | undefined) {
    return articles.useItem(articleId);
}

export function useCreateArticle() {
    return articles.useCreateItem();
}

export function useUpdateArticle(articleId: number | undefined) {
    if (!articleId) {
        throw new Error('articleId must be defined to update.');
    }
    return articles.useUpdateItem(articleId);
}

export function useDeleteArticle(articleId: number) {
    return articles.useDeleteItem(articleId);
}
