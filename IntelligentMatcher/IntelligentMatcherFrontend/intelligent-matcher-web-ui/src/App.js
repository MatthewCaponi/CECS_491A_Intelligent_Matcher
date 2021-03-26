import UserManagement from "./Features/CoreFeatures/UserManagement/Pages/UserManagement";
import { BrowserRouter as Router, Route, Switch } from 'react-router-dom'
import { Grid, Container } from 'semantic-ui-react'
import SiteHeader from './Shared/SiteHeader';
import SiteFooter from './Shared/SiteFooter';
import './App.css';
import React from "react";

function App() {
  return (
    <div className="box">
      <SiteHeader />
      <Router>
        <Switch>
          <Route path="/UserManagement">
                <UserManagement />
          </Route>
        </Switch>
          </Router>
      <SiteFooter />
    </div>
      
  );
}

export default App;