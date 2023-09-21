import { LoaderFunctionArgs } from "react-router-dom";
import { ArticleSummary } from "../models/ArticleSummary";
import { PagedResults } from "../models/PagedResults";
import fetch from "./fetch";

export async function articleListLoader({ request }: LoaderFunctionArgs) {
    const url = new URL(request.url);
    const search = url.searchParams.get('search');
    var result = await getArticles(search);
    result.search = search;
    return result;
}

async function getArticles(search: string | null): Promise<PagedResults<ArticleSummary>> {
    var queryString = new URLSearchParams({
        search: search ?? "",
    });
    const uri = `/articles/?${queryString}`;
    const response = await fetch(uri, {
        method: 'GET',
    });
    return response.json();
}
