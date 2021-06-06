import { StyledEngineProvider } from '@material-ui/core';
import React from 'react';
import ReactDOM from 'react-dom';
import { QueryClient, QueryClientProvider } from 'react-query';
import { BrowserRouter } from 'react-router-dom';
import App from './App';
import registerServiceWorker from './registerServiceWorker';

const queryClient = new QueryClient();

ReactDOM.render(
    <StyledEngineProvider injectFirst>
        <QueryClientProvider client={queryClient}>
            <BrowserRouter>
                <App />
            </BrowserRouter>
        </QueryClientProvider>
    </StyledEngineProvider>,
    document.getElementById('root'));

registerServiceWorker();
