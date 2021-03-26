import React, {useState, useEffect} from 'react';
import UserTable from '../Components/UserTable';
import { Grid } from "@material-ui/core";

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
        <div>
            <Grid item container>
                <Grid item sm={2}/>
                    <Grid item container sm={8}> 
                        <UserTable rows={users}/>
                    </Grid>
                    <Grid item sm={2}/>       
            </Grid>
        </div>   
    )
}

export default UserManagement;