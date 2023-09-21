import { Dropdown } from "react-bootstrap";

export default function BlockToolbar() {
    return (
        <div className="btn-group me-2">
            <Dropdown>
                <Dropdown.Toggle variant="light">
                    Block Type
                </Dropdown.Toggle>
                <Dropdown.Menu>
                    <Dropdown.Item>
                        <i className='fa-solid fa-fw fa-paragraph'></i> Paragraph
                    </Dropdown.Item>
                    <Dropdown.Item>
                        <i className='fa-solid fa-fw fa-heading'></i> Heading
                    </Dropdown.Item>
                    <Dropdown.Item>
                        <i className='fa-solid fa-fw fa-quote-left'></i> Quote
                    </Dropdown.Item>
                    <Dropdown.Item>
                        <i className='fa-solid fa-fw fa-list-ul'></i> Bulleted List
                    </Dropdown.Item>
                    <Dropdown.Item>
                        <i className='fa-solid fa-fw fa-list-ol'></i> Ordered List
                    </Dropdown.Item>
                    <Dropdown.Item>
                        <i className='fa-regular fa-fw fa-image'></i> Image
                    </Dropdown.Item>
                    <Dropdown.Item>
                        <i className='fa-regular fa-fw fa-file-code'></i> Embed
                    </Dropdown.Item>
                </Dropdown.Menu>
            </Dropdown>
        </div>
    );
}
