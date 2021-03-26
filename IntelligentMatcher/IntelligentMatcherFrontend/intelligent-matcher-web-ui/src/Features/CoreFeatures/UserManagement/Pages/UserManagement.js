import React, {useState, useEffect} from 'react';
import UserTable from '../Components/UserTable';
import { Grid } from 'semantic-ui-react'

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
        <Grid centered container>
            <UserTable rows={users}/>
        </Grid>
            

            
 

    )
}

export default UserManagement;