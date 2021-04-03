import React, {useState, useEffect} from 'react';
import ListingTable from '../Components/ListingCategory';
import { Grid, Header, Divider, Label, Search } from 'semantic-ui-react'

function ListingSearch () {
    const [listings, setListings] = useState([]);
    useEffect( () => {
        fetch('http://localhost:5000/ListingSearch/GetAllListings')
        .then(response => response.json())
        .then(responseData => {
            setListings(responseData);
            console.log(responseData);
        });
    }, [])
}
