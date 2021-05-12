import React, {useState, useEffect, useContext} from 'react';
import UserTable from '../Components/UserTable';
import { Grid, Header, Divider, Label, Search } from 'semantic-ui-react'
import { useCookies } from 'react-cookie';
import { AuthnContext } from '../../../../Context/AuthnContext';
import '../.././../../App'
import OopsSplash from '../../../../Shared/ErrorScreens/OopsSplash';
import UnauthorizedSplash from '../../../../Shared/ErrorScreens/UnauthorizedSplash';
import ErrorSplash from '../../../../Shared/ErrorScreens/ErrorSplash';


function UserManagement () {
    const authnContext = useContext(AuthnContext);
    const [cookies, setCookie, removeCoookie] = useCookies(['IdToken']);
    const [users, setUsers] = useState([]);
    const [error, setError] = useState();
    useEffect( () => {
        try {
            const response = fetch(global.url + 'UserManagement/GetAllUserAccounts', {
                method: 'GET',
                headers: {
                    'Content-Type': 'application/json',
                    'Authorization': 'Bearer ' + cookies['AccessToken']
                }
            })
            .then(response => response.json())
            .then(responseData => {
                if (!response.ok){
                    throw new Error(responseData.status);
                }
                setUsers(responseData);
            }).catch(e =>  {
                setError(e)
            }       
            );
        } catch(err) {
            setError(err);
        }
      
    }, [])
    
    if (error) {
        if (error.message === "403"){
            return (
                <UnauthorizedSplash />
            )
        } else {
            return (
               <ErrorSplash />
            )
        }
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
