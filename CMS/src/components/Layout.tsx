import { useAuth } from "react-oidc-context";
import { Link, Outlet } from "react-router-dom";
import styles from './Layout.module.css';

export default function Layout() {
    const auth = useAuth();

    const picture = auth.user!.profile.picture;

    const handleSignOutClick = async () => {
        await auth.signoutRedirect();
    };

    return (
        <>
            <nav className={styles.sidebar + ' d-flex flex-column vh-100 p-3'}>
                <div>LEGGO News</div>
                <hr />
                <ul className='nav nav-pills nav-flush mb-auto'>
                    <li className='nav-item w-100'>
                        <Link to="/articles" className='nav-link link-dark'>
                            <i className='fas fa-pen-nib fa-fw me-3'></i>Articles
                        </Link>
                    </li>
                    <li className='nav-item w-100'>
                        <Link to="/media" className='nav-link link-dark'>
                            <i className='fas fa-photo-video fa-fw me-3'></i>Media
                        </Link>
                    </li>
                    <li className='nav-item w-100'>
                        <Link to="/tags" className='nav-link link-dark'>
                            <i className='fas fa-tag fa-fw me-3'></i>Tags
                        </Link>
                    </li>
                    <li className='nav-item w-100'>
                        <Link to="/characters" className='nav-link link-dark'>
                            <i className='fas fa-masks-theater fa-fw me-3'></i>Characters
                        </Link>
                    </li>
                    <li className='nav-item w-100'>
                        <Link to="/events" className='nav-link link-dark'>
                            <i className='fas fa-calendar fa-fw me-3'></i>Events
                        </Link>
                    </li>
                </ul>
                <hr />
                <div className='dropdown'>
                    <button
                        aria-expanded='false'
                        className='btn d-flex align-items-center dropdown-toggle w-100'
                        data-bs-toggle='dropdown'
                        type='button'
                    >
                        <img src={picture} alt='' className='rounded-circle me-2' width='32' height='32' />
                        Account
                    </button>
                    <ul className='dropdown-menu text-small shadow'>
                        <li>
                            <button
                                className='dropdown-item btn'
                                onClick={handleSignOutClick}
                            >
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