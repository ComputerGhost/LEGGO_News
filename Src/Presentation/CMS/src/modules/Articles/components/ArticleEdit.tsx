import TextEditor from "./TextEditor";

function ContentTabContent() {
    return (
        <TextEditor />
    );
}

function MetadataTabContent() {
    return (
        <>
            Metadata here.
        </>
    );
}

function SourcesTabContent() {
    return (
        <>
            Sources here.
        </>
    );
}

export default function ArticleEdit() {
    return (
        <div className='container'>
            <div className='form-floating mb-3'>
                <input
                    className='form-control'
                    defaultValue=''
                    id='title'
                    maxLength={100}
                    placeholder='Title'
                />
                <label htmlFor='title'>Title</label>
            </div>
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
                        data-bs-target='#sources-tab-pane'
                        id='sources-tab'
                    >
                        Sources
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
                    <ContentTabContent />
                </div>
                <div
                    aria-labelledby=''
                    className='tab-pane'
                    id='sources-tab-pane'
                    role='tabpanel'
                    tabIndex={1}
                >
                    <SourcesTabContent />
                </div>
                <div
                    aria-labelledby=''
                    className='tab-pane'
                    id='meta-tab-pane'
                    role='tabpanel'
                    tabIndex={2}
                >
                    <MetadataTabContent />
                </div>
            </div>
        </div>
    );
}
