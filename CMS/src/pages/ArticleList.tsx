import { Link, useLoaderData, useNavigation } from "react-router-dom";
import SearchForm from "../components/SearchForm";
import { ArticleSummary } from "../models/ArticleSummary";
import { PagedResults } from "../models/PagedResults";

interface IDataRow { data: ArticleSummary; }
function DataRow({ data }: IDataRow) {
    return (
        <tr>
            <td>#{data.title}</td>
            <td className='text-end'>
                <a href="#" className="px-2">
                    <i className="fas fa-pen-to-square"></i>
                </a>
                <a href="#" className="delete-button text-danger px-2">
                    <i className="fas fa-trash"></i>
                </a>
            </td>
        </tr>
    );
}

export default function ArticleList() {
    const { data, nextCursor, search } = useLoaderData() as PagedResults<ArticleSummary>;
    const navigation = useNavigation();

    const isSearching =
        navigation.location &&
        new URLSearchParams(navigation.location.search).has("search");

    return (
        <div className='container'>
            <div className='d-flex align-items-center mb-3'>
                <SearchForm
                    className='flex-grow-1'
                    defaultValue={search ?? ""}
                    label='Search by title'
                />
                <Link to='/articles/new' className='btn btn-primary p-3 ms-2'>
                    Create new
                </Link>
            </div>
            <table className='table'>
                <thead>
                    <tr>
                        <th>Title</th>
                        <th></th>
                    </tr>
                </thead>
                <tbody>
                    {!isSearching && data.map((tagSummary) => (
                        <DataRow data={tagSummary} key={tagSummary.id} />
                    ))}
                    {!isSearching && nextCursor != null && (
                        <tr>
                            <td colSpan={2} className='text-secondary'>
                                <em>Additional results are not listed.</em>
                            </td>
                        </tr>
                    )}
                    {!isSearching && data.length === 0 && (
                        <tr>
                            <td colSpan={2} className='text-secondary'>
                                <em>There are no results.</em>
                            </td>
                        </tr>
                    )}
                    {isSearching && (
                        <tr>
                            <td colSpan={2} className='text-secondary'>
                                <span className='spinner-border spinner-border-sm me-2' role='status'></span>
                                Loading data...
                            </td>
                        </tr>
                    )}
                </tbody>
            </table>
        </div>
    );
}
