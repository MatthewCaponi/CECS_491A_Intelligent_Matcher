import React from 'react';
import Header from '../Components/Header'
import UserList from '../Components/UserList';
import { useEffect, useState } from 'react';

const UserManagementHome = () => {
    const [users, setUsers] = useState([]);
    
    useEffect(() => {
        fetch('http://localhost:5000/UserManagement/GetAllUserAccounts')
        .then(response => response.json())
        .then(data => {
            console.log("Data: " + data);
            setUsers(data)
            });
    })
         
    return (
        <React.Fragment>
            <Header />
            <h1>Users: {users.length}</h1>
            <UserList items={users}/>
        </React.Fragment>       
    )
}

export default UserManagementHome;