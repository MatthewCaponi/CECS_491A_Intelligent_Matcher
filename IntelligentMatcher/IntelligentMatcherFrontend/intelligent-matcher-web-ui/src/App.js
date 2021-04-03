import UserManagement from "./Features/CoreFeatures/UserManagement/Pages/UserManagement";
import ListingSearch from "./Features/CoreFeatures/TraditionalListingSearch/Pages/ListingSearch"
import ListingCategory from "./Features/CoreFeatures/TraditionalListingSearch/Pages/ListingCategory"
import Home from "./Features/CoreFeatures/Home/Pages/Home";
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
          <Route path="/ListingSearch">
            <ListingSearch />
          </Route>
          <Route path="/ListingCategory">
            <ListingCategory />
          </Route>
          <Route path="/">
          <Home />
        </Route>
        </Switch>
          </Router>
      <SiteFooter />
    </div>
      
  );
}

export default App;