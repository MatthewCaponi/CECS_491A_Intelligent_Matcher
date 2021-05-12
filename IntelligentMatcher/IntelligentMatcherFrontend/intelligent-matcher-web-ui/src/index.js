import React from 'react';
import ReactDOM from 'react-dom';
import App from './App';
import './index.css'
import 'semantic-ui-css/semantic.min.css'
import 'react-toastify/dist/ReactToastify.min.css';
import AuthnContextProvider from './Context/AuthnContext';
import ErrorBoundary from './Shared/ErrorBoundrary';

ReactDOM.render(
  <ErrorBoundary>
    <AuthnContextProvider>
      <App />
    </AuthnContextProvider>,
  </ErrorBoundary>,
  document.getElementById('root')
);
