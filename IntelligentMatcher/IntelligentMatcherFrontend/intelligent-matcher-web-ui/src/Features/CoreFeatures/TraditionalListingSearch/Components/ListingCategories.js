import { Tab, TableBody, TableRow } from 'material-ui'
import React, {createRef, useState} from 'react'
import {Pagination} from 'semantic-ui-react'
import { Table, Grid, Dimmer, Loader, Segment, Container } from 'semantic-ui-react'
import './ListingCategories.css'


function ListingCategories(props) {
    const [timedOut, setTimedOut] = useState('False');
    if (props.rows.length === 0) {
        return (
            <Grid container centered>
                <Grid.Row />
                <Grid.Row />
                <Grid.Row>
                </Grid.Row>  
                <Grid.Row>
                </Grid.Row>          
            </Grid>    
        )
    }
}


export default ListingCategories;