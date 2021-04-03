import { Tab, TableBody, TableRow } from 'material-ui'
import React, {createRef, useState} from 'react'
import {Pagination} from 'semantic-ui-react'
import { Table, Grid, Dimmer, Loader, Segment, Container ,Message} from 'semantic-ui-react'
import './ListingTable.css'


function ListingTable(props) {
    const [timedOut, setTimedOut] = useState('False');
    if (props.rows.length === 0) {
        return (
            <Grid container centered>
                <Grid.Row />
                <Grid.Row>
                <Message color= "black" size="medium">NoListingsFound</Message>
                </Grid.Row>          
            </Grid>    
        )
    }
const userStyle = {
    display: 'block',
    height: '40vh',
    overflowY: "auto",

  };

const Pagination =() =>(
    <Pagination defaultActivePage={5} totalPages= {10} />
)

  return (
    <Grid container centered>
        <Table stackable striped verticalAlign selectable size="large" style={userStyle} color="black"
        pagination defaultActivePage={5} totalPages= {10}>
            defaultPageSize(10);
        <Table.Header className="userHeader">
            <Table.row>
            <Table.HeaderCell>Id</Table.HeaderCell>
            <Table.HeaderCell>Title</Table.HeaderCell>
            <Table.HeaderCell>Details</Table.HeaderCell>
            <Table.HeaderCell>City</Table.HeaderCell>
            <Table.HeaderCell>State</Table.HeaderCell>
            <Table.HeaderCell>NumberOfParticipants</Table.HeaderCell>
            <Table.HeaderCell>CreationDate</Table.HeaderCell>
            <Table.HeaderCell>UserAccountId</Table.HeaderCell>
            </Table.row>
        </Table.Header> 
        <TableBody>
            {(props.rows.map(listing => (
                <Table.Row className="ListingRows" key={listing.id}>
                    <Table.Cell>{listing.id}</Table.Cell>
                    <Table.Cell>{listing.Title}</Table.Cell>
                    <Table.Cell>{listing.Details}</Table.Cell>
                    <Table.Cell>{listing.City}</Table.Cell>
                    <Table.Cell>{listing.State}</Table.Cell>
                    <Table.Cell>{listing.NumberOfParticipants}</Table.Cell>
                    <Table.Cell>{listing.CreationDate}</Table.Cell>
                    <Table.Cell>{listing.UserAccountId}</Table.Cell>
                </Table.Row>
            )))}
            </TableBody> 
        </Table>
    </Grid>
  )
}

export default ListingTable;






