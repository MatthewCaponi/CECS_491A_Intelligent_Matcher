import React, {useState, useEffect} from 'react';
import UserTable from '../Components/UserTable';
import { Grid, Header, Divider, Label, Search } from 'semantic-ui-react'
import { AuthorizationContext } from '../../../../Context/AuthorizationContext';

function UserManagement () {
    const [users, setUsers] = useState([]);
    const authorization = useContext(AuthorizationContext);
    useEffect( () => {
        fetch('http://localhost:5000/UserManagement/GetAllUserAccounts', {
            method: 'GET',
            headers: {
                'Content-Type': 'application/json',
                'Authorization': 'Bearer' + authorization.token
            }
        })
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