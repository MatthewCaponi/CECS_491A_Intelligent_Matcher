import React, { useEffect, useState } from 'react';
import Header from '../Components/Header'
import UserList from '../Components/UserList';


   
    
function UserManagementHome() {
    const items = [];
    const [data, setData] = useState([]);
    
    useEffect( () => {
        fetch('http://localhost:5000/UserManagement/GetAllUserAccounts')
        .then(response => response.json())
        .then(responseData => {

            for (const key in responseData) {
                items.push({
                    id: key,
                    username: responseData[key].username
                })
        
            }
            setData(items);

        });
    }, []);

    const [user, obj] = data; 
    return (
        <React.Fragment>
            
            <Header />
            <ul>
                {data.map(user => (
                    <li key={user.id}>
                        {user.username}
                    </li>
                ))}
            </ul>
        </React.Fragment>       
    )
}

export default UserManagementHome;