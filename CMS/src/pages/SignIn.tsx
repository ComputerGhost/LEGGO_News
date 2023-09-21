import { useAuth } from 'react-oidc-context';
import styles from './SignIn.module.css';

export default function SignIn() {
    const auth = useAuth();
    const isBusy = auth.activeNavigator === 'signinSilent';

    const handleSignInClick = async () => {
        try {
            await auth.signinPopup();
        }
        catch (ex) {
            // We generally don't care about an exception within the popup,
            // but log it just in case it's useful for debugging.
            console.error(ex);
        }
    };

    return (
        <div className='d-flex align-items-center py-4'>
            <div className={styles.content + ' m-auto'}>
                <img
                    alt='logo'
                    className='mb-3'
                    height={64}
                    src={`${process.env.REACT_APP_CLIENT_ROOT}/images/logo_64.png`}
                />
                <h1 className='h3 mb-3 fw-normal'>Sign in needed</h1>
                <p>You are not signed in!  Click the link below to open our sign in page in a new window.</p>
                <div>
                    <button
                        className='btn btn-primary w-100 mt-1 py-2'
                        disabled={isBusy}
                        onClick={handleSignInClick}
                        type='submit'
                    >
                        {isBusy && (
                            <span className='spinner-border spinner-border-sm me-2' role='status'></span>
                        )}
                        Open in new window
                    </button>
                </div>
            </div>
        </div>
    );
}
