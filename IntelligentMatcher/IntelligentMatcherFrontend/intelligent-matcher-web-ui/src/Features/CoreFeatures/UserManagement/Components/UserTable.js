import { Tab } from 'material-ui'
import React, {createRef} from 'react'
import { Table, Sticky } from 'semantic-ui-react'
import './UserTable.css'


function UserTable(props) {
    if (props.rows.length === 0) {
        return (
            <h1>No users found</h1>
        )
    }

    const userstyle = {
        display: 'block',
    height: '75vh',
    overflowY: "auto"

      };

    return (
        <div>
            <Table inverted selectable size="small" style={userstyle}>
                <Table.Header className="userHeader">
                    <Table.Row>
                        <Table.HeaderCell>Id</Table.HeaderCell>
                        <Table.HeaderCell>Username</Table.HeaderCell>
                        <Table.HeaderCell>Email</Table.HeaderCell>
                        <Table.HeaderCell>Role</Table.HeaderCell>
                        <Table.HeaderCell>Status</Table.HeaderCell>
                        <Table.HeaderCell>Creation Date</Table.HeaderCell>
                        <Table.HeaderCell>Last Updated</Table.HeaderCell>
                    </Table.Row>
                </Table.Header>
                <Table.Body>
                    {(props.rows.map(user => (
                        <Table.Row className="userRow" key={user.id}>
                            <Table.Cell>{user.id}</Table.Cell>
                            <Table.Cell>{user.username}</Table.Cell>
                            <Table.Cell>{user.emailAddress}</Table.Cell>
                            <Table.Cell>{user.accountType}</Table.Cell>
                            <Table.Cell>{user.accountStatus}</Table.Cell>
                            <Table.Cell>{user.creationDate}</Table.Cell>
                            <Table.Cell>{user.updationDate}</Table.Cell>
                        </Table.Row>
                    )))}
                </Table.Body>
            </Table>
        </div>  
    )

}

export default UserTable;