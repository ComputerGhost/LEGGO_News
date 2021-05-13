import * as React from 'react';
import { connect } from 'react-redux';
import { PageInfo } from '../components';

const Home = () => (
    <>
        <PageInfo title='Dashboard' />
        <div>
            <h1>Hello, world!</h1>
        </div>
    </>
);

export default connect()(Home);
