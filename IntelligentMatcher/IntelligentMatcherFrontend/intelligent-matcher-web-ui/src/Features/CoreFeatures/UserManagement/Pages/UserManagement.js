import React, {useState, useEffect} from 'react';
import UserTable from '../Components/UserTable';
import { Grid, Header, Divider, Label, Search } from 'semantic-ui-react'

function UserManagement () {
    const [users, setUsers] = useState([]);
    useEffect( () => {
        fetch('http://localhost:5000/UserManagement/GetAllUserAccounts')
        .then(response => response.json())
        .then(responseData => {
            setUsers(responseData);
            console.log(responseData);
        });
    }, [])

    return (
        <Grid container centered>
            <Grid.Row>
                <Divider horizontal centered>User Management</Divider>
            </Grid.Row>
            <Grid.Row>
                <Search placeholder="Search User"></Search>
            </Grid.Row>
            <Grid.Row>
                <Grid.Column>
                    <UserTable rows={users}/>
                </Grid.Column>
                
            </Grid.Row>
            <Grid.Row /> 
          
        </Grid>
            

            
 

    )
}

export default UserManagement;