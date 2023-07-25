import { LoaderFunctionArgs } from "react-router-dom";
import { PagedResults } from "../models/PagedResults";
import { TagSummary } from "../models/TagSummary";
import fetch from "./fetch";

export async function tagListLoader({ request } : LoaderFunctionArgs) {
    const url = new URL(request.url);
    const search = url.searchParams.get('search');
    var result = await getTags(search);
    result.search = search;
    return result;
}

async function getTags(search: string | null): Promise<PagedResults<TagSummary>> {
    var queryString = new URLSearchParams({
        search: search ?? "",
    });
    const uri = `/tags/?${queryString}`;
    const response = await fetch(uri, {
        method: 'GET',
    });
    return response.json();
}