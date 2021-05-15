import React from 'react';
import { Header, Segment, Icon, Menu, Input, Container, Grid, Label, Dropdown, Button, Visibility} from 'semantic-ui-react';
import '../.././../../index'

import './AdminDashboard.css';

function AdminDashboard(){
    return(
        <Grid container centered style={{height: '60%'}}>
            <Grid.Row />
            <Grid.Row />
            <Grid.Row>
                <h1>Admin Dashboard</h1>
            </Grid.Row>
            <Grid.Row />
            <Grid.Row />
            <Grid.Row>
                <Button href={global.urlRoute + "UserManagement"} circular inverted color="red" size="massive">
                    User Management
                </Button>
                <Button href={global.urlRoute + "AnalysisDashboard"} circular inverted color="blue" size="massive">
                    Analysis Dashboard
                </Button>
            </Grid.Row>
            <Grid.Row />
            <Grid.Row />
            <Grid.Row />
            <Grid.Row>
                <Button href={global.urlRoute + "Archive"} circular inverted color="green" size="massive">
                    Archive
                </Button>
            </Grid.Row>
            <Grid.Row />
            <Grid.Row />
            <Grid.Row />
            {/*
            <Grid.Row>
                <Button href={global.urlRoute + "ModeratorPanel"} circular inverted color="yellow" size="massive">
                    Moderator Panel
                </Button>
                <Button href={global.urlRoute + "UserAccessControlDashboard"} circular inverted color="purple" size="massive">
                    User Access Control Dashboard
                </Button>
            </Grid.Row>
            */}
        </Grid>
    )
}

export default AdminDashboard;