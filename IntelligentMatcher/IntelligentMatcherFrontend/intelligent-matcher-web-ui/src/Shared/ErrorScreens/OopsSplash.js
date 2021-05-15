import React from 'react';
import { Header, Segment, Container, Grid, Menu, Divider, Image } from 'semantic-ui-react'
import splash from '../images/ErrorSplashScreens/Generic/shutterstock_1265249455.png';


function OopsSplash() {
    return (
        <Grid container centered>
            <Grid.Row></Grid.Row>
            <Grid.Row></Grid.Row>
            <Grid.Row></Grid.Row>
            <Grid.Row></Grid.Row>
            <Grid.Row></Grid.Row>
            <Image fluid src={splash} size="massive" />
            <Grid.Row>
            <Header size='huge' inverted color="violet">Something went wrong...</Header>
            </Grid.Row>
            <Grid.Row></Grid.Row>
            <Grid.Row></Grid.Row>
            <Grid.Row></Grid.Row>
            <Grid.Row></Grid.Row>
        </Grid>
    )
}
export default OopsSplash;