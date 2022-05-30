import { User } from 'oidc-client-ts';
import { createContext } from 'react';

export default createContext<User | null>(null);
