import React from 'react';
import { Card, List, Typography, Divider } from 'antd';

const UserList = props => {
    if (props.items.length === 0) {
        return (
            <div className="center">
                <Card>
                    <h2>No users found.</h2>
                </Card>
            </div>
        );
    }

    return (
        <List>
            {props.items.map(user => (
                <List.Item 
                    key={user.id}
                >
                    {user.Username}
                </List.Item>
            ))}
        </List>
    )
}

export default UserList;