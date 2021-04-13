import UserManagement from "./Features/CoreFeatures/UserManagement/Pages/UserManagement";
import Home from "./Features/CoreFeatures/Home/Pages/Home";
import { BrowserRouter as Router, Route, Switch } from 'react-router-dom'
import { Grid, Container } from 'semantic-ui-react'
import SiteHeader from './Shared/SiteHeader';
import Messaging from "./Features/CoreFeatures/Messaging/Pages/Messaging";
import Login from "./Features/CoreFeatures/Login/Pages/Login";
import ForgotUsername from "./Features/CoreFeatures/Login/Pages/ForgotUsername";
import ForgotPasswordValidation from "./Features/CoreFeatures/Login/Pages/ForgotPasswordValidation";
import Registration from "./Features/CoreFeatures/Registration/Pages/Registration";
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
          <Route path="/Messaging">
            <Messaging />
          </Route>
          <Route path="/Login">
            <Login />
          </Route>
          <Route path="/ForgotUsername">
              <ForgotUsername />
          </Route>
          <Route path="/ForgotPasswordValidation">
              <ForgotPasswordValidation />
          </Route>
          <Route path="/Registration">
            <Registration />
          </Route>
          <Route path="/">
          <Home />
        </Route>
        </Switch>
          </Router>
    </div>
      
  );
}

export default App;