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
import UserAccountSettings from "./Features/CoreFeatures/UserAccountSettings/Pages/UserAccountSettings";
import FriendsList from "./Features/CoreFeatures/FriendsList/Pages/FriendsList";
import UserProfile from "./Features/CoreFeatures/UserProfile/Pages/UserProfile";
import StatusToggle from "./Features/CoreFeatures/UserProfile/Pages/StatusToggle";

import { useEffect, useRef } from 'react';
import { useHistory } from 'react-router-dom';
import SiteFooter from './Shared/SiteFooter';
import './App.css';
import React from "react";

function App() {

  global.url = "http://localhost:5000/";

  return (
    <div className="box">
            <StatusToggle />

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
          <Route path="/UserAccountSettings">
            <UserAccountSettings />
          </Route>
          <Route path="/FriendsList">
            <FriendsList />
          </Route>
          <Route path="/profile">
            <UserProfile />
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