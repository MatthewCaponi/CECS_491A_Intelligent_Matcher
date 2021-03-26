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
        <Grid container className="content">
        <Grid.Row />
        <Grid.Column width={2}/>
        <Grid.Column width={12}>
        <Router>
            <Switch>
                  <Route path="/UserManagement">
                    <UserManagement />
                  </Route>
            </Switch>
          </Router>
        </Grid.Column>
        <Grid.Column width={2}/>
       
          <Grid.Row />
        </Grid>
        <SiteFooter />
    </div>
      
  );
}

export default App;