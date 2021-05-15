import React, { useState } from 'react';
import { Table, Grid, Message} from 'semantic-ui-react';
import './ListingTable.css';

function ListingTable(props) {
    const [timedOut, setTimedOut] = useState('False');
    if (props.listings.length === 0) {
        return (
            <Grid container centered>
                <Grid.Row />
                <Grid.Row>
                <Message color= "black" size="medium">NoListingsFound</Message>
                </Grid.Row>          
            </Grid>    
        )
    }

  return (
      <Grid container centered>
          <Table>
              <Table.Header>
                  <Table.Row>
                      <Table.HeaderCell>Id</Table.HeaderCell>
                      <Table.HeaderCell>Title</Table.HeaderCell>
                      <Table.HeaderCell>Details</Table.HeaderCell>
                      <Table.HeaderCell>City</Table.HeaderCell>
                      <Table.HeaderCell>State</Table.HeaderCell>
                      <Table.HeaderCell>NumberOfParticipants</Table.HeaderCell>
                      <Table.HeaderCell>InPersonOrRemote</Table.HeaderCell>
                      <Table.HeaderCell>UserAccountId</Table.HeaderCell>
                  </Table.Row>
              </Table.Header>
              <Table.Body>
              {(props.listings.map(listing => (
                  <Table.Row>
                      <Table.Cell>{listing.id}</Table.Cell>
                      <Table.Cell>{listing.title}</Table.Cell>
                      <Table.Cell>{listing.details}</Table.Cell>
                      <Table.Cell>{listing.city}</Table.Cell>
                      <Table.Cell>{listing.state}</Table.Cell>
                      <Table.Cell>{listing.numberOfParticipants}</Table.Cell>
                      <Table.Cell>{listing.inPersonOrRemote}</Table.Cell>
                      <Table.Cell>{listing.userAccountId}</Table.Cell>
                  </Table.Row>
                  )))}
              </Table.Body>
          </Table>
      </Grid>
  )
}

export default ListingTable;






