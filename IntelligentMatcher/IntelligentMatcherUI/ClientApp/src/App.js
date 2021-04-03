import React, { Component } from 'react';
import { Route } from 'react-router';
import { Layout } from './components/Layout';
import { UserAccountSettings } from './components/UserAccountSettings/UserAccountSettings';
import { FetchData } from './components/FetchData';
import { Messaging } from './components/Messaging/Messaging';

import './custom.css'

export default class App extends Component {
  static displayName = App.name;

  render () {
    return (
      <Layout>
        <Route exact path='/' component={Messaging} />
        <Route path='/fetch-data' component={FetchData} />
      </Layout>
    );
  }
}
