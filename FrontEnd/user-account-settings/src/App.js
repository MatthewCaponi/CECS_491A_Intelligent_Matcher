import React, { Component, Fragment } from 'react';
import AppHeader from './components/AppHeader';
import AppFooter from './components/AppFooter';
import UpdatePasswordForm from './components/UpdatePasswordForm';

class App extends Component {
  render() {
    return <Fragment>
      <AppHeader />
      <UpdatePasswordForm />

      <AppFooter />
    </Fragment>;
  }
}
export default App;