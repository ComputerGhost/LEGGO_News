export default function ArticleEdit() {
    return (
        <>
            <div className='nav nav-tabs' role='tablist'>
                <li className='nav-item' role='presentation'>
                    <button
                        className='nav-link active'
                        data-bs-toggle='tab'
                        data-bs-target='#content-tab-pane'
                        id='content-tab'
                    >
                        Content
                    </button>
                </li>
                <li className='nav-item' role='presentation'>
                    <button
                        className='nav-link'
                        data-bs-toggle='tab'
                        data-bs-target='#meta-tab-pane'
                        id='meta-tab'
                    >
                        Metadata
                    </button>
                </li>
            </div>
            <div className='tab-content'>
                <div
                    aria-labelledby=''
                    className='tab-pane show active'
                    id='content-tab-pane'
                    role='tabpanel'
                    tabIndex={0}
                >
                    Content
                </div>
                <div
                    aria-labelledby=''
                    className='tab-pane'
                    id='meta-tab-pane'
                    role='tabpanel'
                    tabIndex={0}
                >
                    meta data
                </div>
            </div>
        </>
    );
}
