import UserManagement from "./Features/CoreFeatures/UserManagement/Pages/UserManagement";
import ListingTable from "./Features/CoreFeatures/TraditionalListingSearch/Pages/ListingSearch"
import ListingCategoryPage from "./Features/CoreFeatures/TraditionalListingSearch/Pages/ListingCategoryPage"
import Home from "./Features/CoreFeatures/Home/Pages/Home";
import { BrowserRouter as Router, Redirect, Route, Switch, useHistory } from 'react-router-dom'
import { Grid, Container } from 'semantic-ui-react'
import SiteHeader from './Shared/SiteHeader';
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
import { getCookie } from 'react-use-cookie';
import jwt from 'jwt-decode';
import { useCookies } from 'react-cookie';
import { useEffect, useRef, useState, useContext } from 'react';
import SiteFooter from './Shared/SiteFooter';
import './App.css';
import React from "react";
import {AuthnContext} from './Context/AuthnContext';

function App() {
  const authnContext = useContext(AuthnContext);
  const [cookies, setCookie, removeCoookie] = useCookies(['IdToken']);
  const history = useHistory();
  const [loggedIn, setLoggedIn] = useState();
  global.url = "http://localhost:5000/";
  useEffect(() => {
    try {
      if (cookies['IdToken'] !== null) {
        const idToken = cookies['IdToken'];
        const decodedIdToken = jwt(idToken).exp;
        var currentDateTime = Date.now()/1000;
        if (decodedIdToken > currentDateTime)
        {  
            authnContext.login();
        } else {
          authnContext.logout();
        }
      } else {
        authnContext.logout();
      }
    } catch (error) {
      console.log(error.message);
    }
    }, [])

    if (!authnContext.isLoggedIn) {
      return (
      <div className="box">
        <Router>
          <Switch>
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
            <Route path="/">
              <Login />
            </Route>
          </Switch>
        </Router>
      </div>)
    } else {
      return (
        <div className="box">
        <Router>
          <SiteHeader />
          <StatusToggle />
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
              <Route path="/Archive">
                <Archive />
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
              <Route path="/ConfirmAccount">
                <ConfirmAccount />
              </Route>
              <Route path="/">
                <Home />
              </Route>
            </Switch>
          </Router>
        </div>)

    }
  
  
}

export default App;