import React from 'react';
import { Header, Segment, Container, Grid, Menu, Divider, Image } from 'semantic-ui-react'
import splash from '../images/ErrorSplashScreens/Generic/Error Guy Blue.png';


function ErrorSplash() {
    return (
        <Grid container centered>
            <Grid.Row></Grid.Row>
            <Grid.Row></Grid.Row>
            <Grid.Row></Grid.Row>
            <Grid.Row></Grid.Row>
            <Grid.Row></Grid.Row>
            <Image fluid src={splash} size="massive" />
            <Grid.Row>
            <Header size='huge' inverted color="violet">Page Not Found...</Header>
            </Grid.Row>
            <Grid.Row></Grid.Row>
            <Grid.Row></Grid.Row>
            <Grid.Row></Grid.Row>
            <Grid.Row></Grid.Row>
        </Grid>
    )
}
export default ErrorSplash;