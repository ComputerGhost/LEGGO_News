import { useLoaderData } from "react-router-dom";
import { PagedArticles } from "../models/paged-articles";

export default function ArticleList() {
    const articleList = useLoaderData() as PagedArticles;

    return (
        <table>
            <tbody>
                {articleList.results.map(article => (
                    <tr>
                        <td>{article.title}</td>
                    </tr>
                ))}
            </tbody>
        </table>
    );
}
