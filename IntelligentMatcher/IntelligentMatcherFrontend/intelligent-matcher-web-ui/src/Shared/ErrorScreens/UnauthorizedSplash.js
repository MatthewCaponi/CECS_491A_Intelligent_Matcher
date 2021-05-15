import React from 'react';
import { Header, Segment, Container, Grid, Menu, Divider, Image } from 'semantic-ui-react'
import splash from '../images/ErrorSplashScreens/Unauthorized/shutterstock_761292145.png';


function UnauthorizedSplash() {
    return (
        <div>
            <Grid container centered>
                <Grid.Row></Grid.Row>
                <Grid.Row></Grid.Row>
                <Grid.Row></Grid.Row>
                <Grid.Row></Grid.Row>
                <Grid.Row></Grid.Row>
                <Image fluid src={splash} size="massive" />
                <Grid.Row>
                <Header size='huge' inverted color="violet">Restricted Area!</Header>
                </Grid.Row>
                <Grid.Row></Grid.Row>
                <Grid.Row></Grid.Row>
                <Grid.Row></Grid.Row>
                <Grid.Row></Grid.Row>
            </Grid>
        </div>
    )
}
export default UnauthorizedSplash;