import { ReactElement, useEffect } from 'react';
import { default as WrappedComponent } from 'react-infinite-scroll-component';

interface IProps {
    children: ReactElement,
    dataLength: number,
    hasMore: boolean,
    next: () => void,
}

// This can be replaced with the third-party one after they fix the bugs.
export default function ({
    children,
    dataLength,
    hasMore,
    next,
}: IProps)
{
    // Fixes a bug on InfiniteScroll
    // Source: https://github.com/ankeetmaini/react-infinite-scroll-component/issues/217
    useEffect(() => {
        if (typeof window !== 'undefined')
            window.dispatchEvent(new CustomEvent('scroll'));
    }, [dataLength]);

    return (
        <WrappedComponent
            dataLength={dataLength}
            hasMore={hasMore}
            next={next}
            loader={<h4>Loading</h4>}
        >
            {children}
        </WrappedComponent>
    );
}
