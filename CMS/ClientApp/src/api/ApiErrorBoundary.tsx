import React from 'react';
import ApiError from './ApiError';

type Props = {
    onAuthenticationRequired: () => void,
};

type State = {
    error?: ApiError
};

export default class extends React.Component<Props, State>
{
    private onAuthenticationRequired: () => void;

    public constructor(props: any) {
        super(props);
        this.state = {};
        this.onAuthenticationRequired = props.onAuthenticationRequired;
    }

    public static getDerivedStateFromError(error: Error) {
        return { error };
    }

    public render() {
        if ((this.state.error?.name === 'ApiError')) {
            this.handleError(this.state.error);
        }
        return this.props.children;
    }

    private handleError(error?: ApiError) {
        switch (error?.status) {
            case 401:
                this.onAuthenticationRequired();
                break;
        }
    }
}
