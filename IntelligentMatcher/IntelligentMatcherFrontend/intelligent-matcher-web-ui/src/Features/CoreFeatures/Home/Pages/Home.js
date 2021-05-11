import React, {useState, useEffect} from 'react';
import { Link } from 'react-router-dom';
import { Grid, Header, Divider, Label, Search, Container, Button } from 'semantic-ui-react'
import { useHistory } from 'react-router-dom';

import './Home.css';
import Cookies from 'js-cookie';

function Home() {


    const history = useHistory();
    //history.location.state.routerdatayouwanttoaccess
    return (
        <Grid container centered style={{height: '60%'}}>
        <Grid.Row />
        <Grid.Row />
        <Grid.Row>
            <Divider style={{fontSize: 20}} horizontal>What are you looking for today?</Divider>
        </Grid.Row>
        <Grid.Row verticalAlign="middle">
            <Grid.Column verticalAlign="middle" mobile={16}>
            <Grid container centered>
                <Grid.Row>
                    <Search placeholder="Start smart search..." size="massive"></Search>
                </Grid.Row>
                <Grid.Row>
                <Button href="http://localhost:3000/ListingForm"compact size="tiny" circular inverted color="violet">Create Listing
               
                </Button>
                    <Button href="http://localhost:3000/ListingCategoryPage" compact size="tiny" circular inverted color="blue">Traditional Search</Button>

                </Grid.Row>
            </Grid> 
            </Grid.Column>
     
        </Grid.Row>
        </Grid>
    )

}



export default Home;