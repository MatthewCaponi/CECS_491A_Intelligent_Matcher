import React, {useState, useEffect, useContext} from 'react';
import UserTable from '../Components/UserTable';
import { Grid, Header, Divider, Label, Search } from 'semantic-ui-react'
import { useCookies } from 'react-cookie';
import { AuthnContext } from '../../../../Context/AuthnContext';


function UserManagement () {
    const authnContext = useContext(AuthnContext);
    const [cookies, setCookie, removeCoookie] = useCookies(['IdToken']);
    const [users, setUsers] = useState([]);
    useEffect( () => {
        fetch('http://localhost:5000/UserManagement/GetAllUserAccounts', {
            method: 'GET',
            headers: {
                'Content-Type': 'application/json',
                'Authorization': 'Bearer ' + cookies['AccessToken']
            }
        })
        .then(response => response.json())
        .then(responseData => {
            setUsers(responseData);
            console.log(responseData);
        });
    }, [])

    if (authnContext.getRole !== "admin") {

        return <h1>Not Authorized</h1>;
    }
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