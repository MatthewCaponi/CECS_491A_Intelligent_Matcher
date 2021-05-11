import React from 'react';
import ReactDOM from 'react-dom';
import App from './App';
import './index.css'
import 'semantic-ui-css/semantic.min.css'
import AuthnContextProvider from './Context/AuthnContext';

ReactDOM.render(
  <AuthnContextProvider>
    <App />
  </AuthnContextProvider>,
  document.getElementById('root')
);
