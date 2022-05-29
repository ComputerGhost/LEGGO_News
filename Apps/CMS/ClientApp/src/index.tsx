import { StyledEngineProvider } from '@mui/material';
import { LocalizationProvider } from '@mui/lab';
import React from 'react';
import ReactDOM from 'react-dom';
import { QueryClient, QueryClientProvider } from 'react-query';
import { BrowserRouter } from 'react-router-dom';
import AdapterDateFns from '@mui/lab/AdapterDateFns';
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
