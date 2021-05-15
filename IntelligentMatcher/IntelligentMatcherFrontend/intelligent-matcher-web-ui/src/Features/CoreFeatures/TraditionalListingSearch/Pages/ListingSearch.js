import React, {useState, useEffect} from 'react';

import { Grid, Header, Divider, Label, Search,Table,TableBody } from 'semantic-ui-react'
import '../../../../App'
import ListingTable from "../Components/ListingTable"


function ListingSearch () {
    const [listings, setListings] = useState([]);
    useEffect( () => {
        fetch(global.url + 'ListingTable/GetAllParentListing')
        .then(response => response.json())
        .then(responseData => {
            setListings(responseData);
            console.log(responseData);
        });
    }, [])

    console.log(listings.successValue);
    const userStyle = {
        display: 'block',
        height: '40vh',
        overflowY: "auto",
    
      };
    return (
        <Grid container centered>
    <Grid.Row>
        <Divider horizontal centered>Listings</Divider>
    </Grid.Row>
    <Grid.Row>
    </Grid.Row>
    <Grid.Row>
        <Grid.Column>
        <Grid container centered>
        <ListingTable listings={listings}/> 
    </Grid>
        </Grid.Column>
        
    </Grid.Row>
    <Grid.Row />    
</Grid>
            

            
    

    )
}

export default ListingSearch;