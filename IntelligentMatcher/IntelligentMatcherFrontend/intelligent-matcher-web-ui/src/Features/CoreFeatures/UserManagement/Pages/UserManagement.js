import { activeGridFilterItemsSelector } from '@material-ui/data-grid';
import React, {useState, useEffect} from 'react';
import UserList from "../Components/UserList";
import UserTable from '../Components/UserTable'

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
            <UserTable rows={users}/>
        </div>

        
    )


}

export default UserManagement;