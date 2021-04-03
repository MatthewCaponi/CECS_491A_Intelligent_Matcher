import React, {useState, useEffect} from 'react';
import ListingTable from '../Components/ListingTable';
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

    return (
        <Grid container centered>
            <Grid.Row>
                <Divider horizontal centered>Listings</Divider>
            </Grid.Row>
            <Grid.Row>
                <Search placeholder="Search for Listings"></Search>
            </Grid.Row>
            <Grid.Row>
                <Grid.Column>
                    <ListingTable rows={listings}/>
                </Grid.Column>
                
            </Grid.Row>
            <Grid.Row />    
        </Grid>
            

            
    

    )
}

export default ListingSearch;