import { Tab } from 'material-ui'
import React, {createRef} from 'react'
import Table from 'react-bootstrap/Table';
import './UserTable.css'

function UserTable(props) {
    if (props.rows.length === 0) {
        return (
            <h1>No users found</h1>
        )
    }

    return (
        <div >
            <Table bordered hover className="container__table">
                <thead>
                    <tr>
                        <th>Id</th>
                        <th>Username</th>
                        <th>Email</th>
                        <th>Role</th>
                        <th>Status</th>
                        <th>Creation Date</th>
                        <th>Last Updated</th>
                    </tr>
                </thead>
                <tbody>
                    {(props.rows.map(user => (
                        <tr className="userRow" key={user.id}>
                            <td>{user.id}</td>
                            <td>{user.username}</td>
                            <td>{user.emailAddress}</td>
                            <td>{user.accountType}</td>
                            <td>{user.accountStatus}</td>
                            <td>{user.creationDate}</td>
                            <td>{user.updationDate}</td>
                        </tr>
                    )))}
                </tbody>
            </Table>
        </div>
    )

}

export default UserTable;