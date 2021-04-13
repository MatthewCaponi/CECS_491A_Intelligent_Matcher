import UserManagement from "./Features/CoreFeatures/UserManagement/Pages/UserManagement";
import Home from "./Features/CoreFeatures/Home/Pages/Home";
import { BrowserRouter as Router, Route, Switch } from 'react-router-dom'
import { Grid, Container } from 'semantic-ui-react'
import SiteHeader from './Shared/SiteHeader';
import Messaging from "./Features/CoreFeatures/Messaging/Pages/Messaging";
import UserAccountSettings from "./Features/CoreFeatures/UserAccountSettings/Pages/UserAccountSettings";
import FriendsList from "./Features/CoreFeatures/FriendsList/Pages/FriendsList";
import UserProfile from "./Features/CoreFeatures/UserProfile/Pages/UserProfile";

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
          <Route path="/UserAccountSettings">
            <UserAccountSettings />
          </Route>
          <Route path="/FriendsList">
            <FriendsList />
          </Route>
          <Route path="/UserProfile">
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