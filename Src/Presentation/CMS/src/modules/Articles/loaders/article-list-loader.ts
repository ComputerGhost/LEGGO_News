import { PagedArticles } from "../models/paged-articles";

export default function ArticleListLoader(): Promise<PagedArticles> {
    var uri = `${process.env.REACT_APP_API_BASE_URL}articles`;
    return fetch(uri).then(r => r.json());
}
