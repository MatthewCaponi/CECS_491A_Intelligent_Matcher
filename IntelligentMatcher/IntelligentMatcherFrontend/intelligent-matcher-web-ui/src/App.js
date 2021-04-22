import UserManagement from "./Features/CoreFeatures/UserManagement/Pages/UserManagement";
import ListingTable from "./Features/CoreFeatures/TraditionalListingSearch/Pages/ListingSearch"
import ListingCategoryPage from "./Features/CoreFeatures/TraditionalListingSearch/Pages/ListingCategoryPage"
import Home from "./Features/CoreFeatures/Home/Pages/Home";
import { BrowserRouter as Router, Redirect, Route, Switch } from 'react-router-dom'
import { Grid, Container } from 'semantic-ui-react'
import SiteHeader from './Shared/SiteHeader';
import Messaging from "./Features/CoreFeatures/Messaging/Pages/Messaging";
import ListingForm from "./Features/CoreFeatures/ListingForm/Pages/ListingFormPage"
import Login from "./Features/CoreFeatures/Login/Pages/Login";
import ForgotUsername from "./Features/CoreFeatures/Login/Pages/ForgotUsername";
import ForgotPasswordValidation from "./Features/CoreFeatures/Login/Pages/ForgotPasswordValidation";
import ForgotPasswordCodeInput from "./Features/CoreFeatures/Login/Pages/ForgotPasswordCodeInput";
import ResetPassword from "./Features/CoreFeatures/Login/Pages/ResetPassword";
import Registration from "./Features/CoreFeatures/Registration/Pages/Registration";
import ResendEmail from "./Features/CoreFeatures/Registration/Pages/ResendEmail";
import UserAccountSettings from "./Features/CoreFeatures/UserAccountSettings/Pages/UserAccountSettings";
import FriendsList from "./Features/CoreFeatures/FriendsList/Pages/FriendsList";
import UserProfile from "./Features/CoreFeatures/UserProfile/Pages/UserProfile";
import StatusToggle from "./Features/CoreFeatures/UserProfile/Pages/StatusToggle";
import Confirm from "./Features/CoreFeatures/Registration/Pages/Confirm";

import { useEffect, useRef } from 'react';
import { useHistory } from 'react-router-dom';
import SiteFooter from './Shared/SiteFooter';
import './App.css';
import React from "react";

function App() {

  global.url = "http://localhost:5000/";

  return (
    <div className="box">
      <SiteHeader/>
      <Router>
        <Switch>
          <Route path="/UserManagement">
            <UserManagement />
          </Route>
          <Route path="/ListingForm">
            <ListingForm />
          </Route>
          <Route path="/ListingTable">
            <ListingTable />
          </Route>
          <Route path="/ListingCategoryPage">
            <ListingCategoryPage />
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
          <Route path='/ForgotPasswordCodeInput'>
            <ForgotPasswordCodeInput />
          </Route>
          <Route path="/ResetPassword">
            <ResetPassword />
          </Route>
          <Route path="/Registration">
            <Registration />
          </Route>
          <Route path="/ResendEmail">
            <ResendEmail />
          </Route>
          <Route path="/UserAccountSettings">
            <UserAccountSettings />
          </Route>
          <Route path="/FriendsList">
            <FriendsList />
          </Route>
          <Route path="/Messaging">
            <Messaging />
          </Route>
          <Route path="/profile">
            <UserProfile />
          </Route>

          <Route path="/confirm">
            <Confirm />
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