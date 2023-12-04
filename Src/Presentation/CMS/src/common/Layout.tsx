import { Button } from 'react-bootstrap';
import { Link, Outlet, useLocation } from 'react-router-dom';
import modules from '../modules';
import styles from './Layout.module.css';
import Module from './module';

function ModuleLink(module: Module) {
    var index = module.routes.find(m => m.index === true);

    var location = useLocation();
    const isActive = location.pathname.startsWith(index?.path ?? '');
    const linkStyle = isActive ? "nav-link active" : "nav-link";

    return (
        <li key={module.name} className='nav-item w-100'>
            <Link to={index?.path ?? '#'} className={linkStyle}>
                <i className={`fas fa-fw me-3 ${module.icon}`}></i>{module.name}
            </Link>
        </li>
    );
}

function ModuleLinks() {
    return (<>{modules.map(module => ModuleLink(module))}</>);
}

export default function Layout() {
    return (
        <>
            <nav className={`${styles.sidebar} d-flex flex-column vh-100 p-3 bg-white`}>
                <div>LEGGO News</div>
                <hr />
                <menu className='nav nav-pills nav-flush mb-auto'>
                    <ModuleLinks />
                </menu>
                <hr />
                <div className='dropdown'>
                    <Button
                        aria-expanded='false'
                        className='d-flex align-items-center dropdown-toggle w-100'
                        data-bs-toggle='dropdown'
                        variant="outline-dark"
                    >
                        <div className='rounded-circle me-2' style={{ 'background': '#f0f', 'width': 32, 'height': 32 }}></div>
                        Account
                    </Button>
                    <menu className='dropdown-menu text-small shadow'>
                        <li>
                            <Button className='dropdown-item'>
                                Sign out
                            </Button>
                        </li>
                    </menu>
                </div>
            </nav>
            <main className={`${styles.main} py-2`}>
                <Outlet />
            </main>
        </>
    );
}