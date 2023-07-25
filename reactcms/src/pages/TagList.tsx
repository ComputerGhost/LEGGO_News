import { useLoaderData, useNavigation } from "react-router-dom";
import SearchForm from "../components/SearchForm";
import { PagedResults } from "../models/PagedResults";
import { TagSummary } from "../models/TagSummary";

interface IDataRow { data: TagSummary; }
function DataRow({ data }: IDataRow) {
    return (
        <tr>
            <td>#{data.name}</td>
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

export default function TagList() {
    const { data, nextCursor, search } = useLoaderData() as PagedResults<TagSummary>;
    const navigation = useNavigation();

    const isSearching =
        navigation.location &&
        new URLSearchParams(navigation.location.search).has("search");

    return (
        <div className='container'>
            <SearchForm
                defaultValue={search ?? ""}
                label='Search by name'
            />
            <table className='table'>
                <thead>
                    <tr>
                        <th>Tag name</th>
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
