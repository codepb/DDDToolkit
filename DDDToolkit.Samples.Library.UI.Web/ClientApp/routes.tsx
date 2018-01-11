import * as React from 'react';
import { Route } from 'react-router-dom';
import { Layout } from './components/Layout';
import { FetchData } from './components/FetchData';
import CreateBook from './components/CreateBook';

export const routes = <Layout>
    <Route exact path='/' component={FetchData} />
    <Route exact path='/Book/Create' component={CreateBook} />
</Layout>;
