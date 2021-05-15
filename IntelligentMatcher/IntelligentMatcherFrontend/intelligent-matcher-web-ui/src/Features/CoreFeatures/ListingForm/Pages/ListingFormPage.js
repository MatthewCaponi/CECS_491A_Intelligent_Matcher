import React, {useState, useEffect} from 'react';
import ListingForm from '../Components/ListingForm';
import { Grid, Header, Divider, Label, Search,Button,Message, Container } from 'semantic-ui-react'
import { Router } from 'react-router';


import '../.././../../index'

function ListingFormPage () {
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
        ListingForm()
        
    )
}



export default ListingFormPage;