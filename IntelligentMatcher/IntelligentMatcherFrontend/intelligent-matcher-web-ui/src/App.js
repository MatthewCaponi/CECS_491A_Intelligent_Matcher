import UserManagement from "./Features/CoreFeatures/UserManagement/Pages/UserManagement";
import ListingCategoryPage from "./Features/CoreFeatures/TraditionalListingSearch/Pages/ListingCategoryPage"
import Home from "./Features/CoreFeatures/Home/Pages/Home";
import { BrowserRouter as Router, Redirect, Route, Switch } from 'react-router-dom'
import { Grid, Container } from 'semantic-ui-react'
import SiteHeader from './Shared/SiteHeader';
import AdminDashboard from "./Features/CoreFeatures/AdminDashboard/Pages/AdminDashboard";
import Help from "./Features/CoreFeatures/Help/Pages/Help";
import Messaging from "./Features/CoreFeatures/Messaging/Pages/Messaging";
import ListingForm from "./Features/CoreFeatures/ListingForm/Pages/ListingFormPage"
import Archive from "./Features/CoreFeatures/Archive/Pages/Archive";
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
import ConfirmAccount from "./Features/CoreFeatures/Registration/Pages/ConfirmAccount";
import ListingSearch from "./Features/CoreFeatures/TraditionalListingSearch/Pages/ListingSearch"

import { useEffect, useRef } from 'react';
import { useHistory } from 'react-router-dom';
import SiteFooter from './Shared/SiteFooter';
import './App.css';
import React from "react";
import AnalysisDashboard from "./Features/CoreFeatures/UserAnalysisDashboard/Components/AnalysisDashboard";

function App() {

  return (
    <div className="box">
      <SiteHeader/>
      <StatusToggle />
      <Router>
        <Switch>
          <Route path="/AdminDashboard">
            <AdminDashboard />
          </Route>
          <Route path="/Help">
            <Help />
          </Route>
          <Route path="/UserManagement">
            <UserManagement />
          </Route>
          <Route path="/ListingForm">
            <ListingForm />
          </Route>
          <Route path="/ListingSearch">
            <ListingSearch />
          </Route>
          <Route path="/ListingCategoryPage">
            <ListingCategoryPage />
          </Route> 
          <Route path="/Archive">
            <Archive />
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
          <Route path="/AnalysisDashboard">
            <AnalysisDashboard />
          </Route>

          <Route path="/ConfirmAccount">
            <ConfirmAccount />
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