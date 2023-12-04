import { useLoaderData } from 'react-router-dom';
import { ArticleSummary, PagedArticles } from '../models/paged-articles';

function ArticleListItem(item: ArticleSummary) {
    return (
        <li key={item.id} className='list-group-item'>
            <div className='d-flex justify-content-between'>
                <h6>{item.title}</h6>
                <div className='btn-group'>
                    <button className='btn btn-outline-secondary'>
                        <i className='fas fa-fw fa-pen-to-square'></i>
                    </button>
                    <button className='btn btn-outline-danger'>
                        <i className='fas fa-fw fa-trash'></i>
                    </button>
                </div>
            </div>
        </li>
    );
}

export default function ArticleList() {
    const articleList = useLoaderData() as PagedArticles;

    return (
        <>
            <header className='d-flex justify-content-between mb-3'>
                <h2><i className="fas fa-pen-nib"></i> Articles</h2>
                <a href="articles/new" className="btn btn-primary">
                    New Article
                </a>
            </header>
            <search className='mb-3'>
                <div className='form-floating'>
                    <input type='search' className='form-control'></input>
                    <label>Search titles, #tags, or @people.</label>
                </div>
            </search>
            <ul className='list-group'>
                {articleList.results.map(article => ArticleListItem(article))}
            </ul>
        </>
    );
}
