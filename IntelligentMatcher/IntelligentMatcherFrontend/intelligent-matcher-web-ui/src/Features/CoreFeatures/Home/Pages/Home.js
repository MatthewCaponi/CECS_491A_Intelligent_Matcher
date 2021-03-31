import React, {useState, useEffect} from 'react';
import { Grid, Header, Divider, Label, Search, Container, Button } from 'semantic-ui-react'
import './Home.css';

function Home() {
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
                <Button compact size="tiny" circular inverted color="violet">Create Listing</Button>
                    <Button compact size="tiny" circular inverted color="blue">Traditional Search</Button> 
                </Grid.Row>
            </Grid> 
            </Grid.Column>
     
        </Grid.Row>
        </Grid>
    )

}

export default Home;