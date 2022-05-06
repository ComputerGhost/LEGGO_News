import { StyledEngineProvider } from '@material-ui/core';
import { LocalizationProvider } from '@material-ui/lab';
import React from 'react';
import ReactDOM from 'react-dom';
import { QueryClient, QueryClientProvider } from 'react-query';
import { BrowserRouter } from 'react-router-dom';
import AdapterDateFns from '@material-ui/lab/AdapterDateFns';
import App from './App';
import registerServiceWorker from './registerServiceWorker';

const queryClient = new QueryClient();

ReactDOM.render(
    <LocalizationProvider dateAdapter={AdapterDateFns}>
        <StyledEngineProvider injectFirst>
            <QueryClientProvider client={queryClient}>
                <BrowserRouter>
                    <App />
                </BrowserRouter>
            </QueryClientProvider>
        </StyledEngineProvider>
    </LocalizationProvider>,
    document.getElementById('root'));

registerServiceWorker();
