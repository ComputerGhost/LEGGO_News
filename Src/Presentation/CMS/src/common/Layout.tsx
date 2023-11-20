import { Link, Outlet } from 'react-router-dom';
import modules from "../modules";
import styles from './Layout.module.css';
import Module from "./module";

function ModuleLink(module: Module) {
    var index = module.routes.find(m => m.index === true);
    return (
        <li className='nav-item w-100'>
            <Link to={index?.path ?? '#'} className='nav-link link-dark'>
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
            <nav className={styles.sidebar + ' d-flex flex-column vh-100 p-3'}>
                <div>LEGGO News</div>
                <hr />
                <ul className='nav nav-pills nav-flush mb-auto'>
                    <ModuleLinks />
                </ul>
                <hr />
                <div className='dropdown'>
                    <button
                        aria-expanded='false'
                        className='btn d-flex align-items-center dropdown-toggle w-100'
                        data-bs-toggle='dropdown'
                        type='button'
                    >
                        <div className='rounded-circle me-2' style={{ 'background': '#f0f', 'width': 32, 'height': 32 }}></div>
                        Account
                    </button>
                    <ul className='dropdown-menu text-small shadow'>
                        <li>
                            <button className='dropdown-item btn'>
                                Sign out
                            </button>
                        </li>
                    </ul>
                </div>
            </nav>
            <main className={styles.main + ' py-2'}>
                <Outlet />
            </main>
        </>
    );
}