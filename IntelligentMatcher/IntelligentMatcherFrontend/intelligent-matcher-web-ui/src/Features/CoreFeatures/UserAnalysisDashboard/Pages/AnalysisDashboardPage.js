import React, {useState, useEffect} from 'react';
import AnalysisDashboard from '../Components/AnalysisDashboard';
import { Grid, Header, Divider, Label, Search,Button,Message, Container } from 'semantic-ui-react'
import { Router } from 'react-router';
import ListingSearch from '../../TraditionalListingSearch/Pages/ListingSearch';
import '../.././../../index'


function AnalysisDashboardPage () {
    const [listings, setListings] = useState([]);
    useEffect( () => {
        fetch(global.url + 'ListingForm/GetForm')
        .then(response => response.json())
        .then(responseData => {
            setListings(responseData);
            console.log(responseData);
        });
    }, [])


    return(
        AnalysisDashboard()
        
    )
}



export default AnalysisDashboardPage;