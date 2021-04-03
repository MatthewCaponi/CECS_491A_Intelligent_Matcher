import UserManagement from "./Features/CoreFeatures/UserManagement/Pages/UserManagement";
import ListingTable from "./Features/CoreFeatures/TraditionalListingSearch/Pages/ListingSearch"
import ListingCategoryPage from "./Features/CoreFeatures/TraditionalListingSearch/Pages/ListingCategoryPage"
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
          <Route path="/ListingTable">
            <ListingTable />
          </Route>
          <Route path="/ListingCategoryPage">
            <ListingCategoryPage />
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