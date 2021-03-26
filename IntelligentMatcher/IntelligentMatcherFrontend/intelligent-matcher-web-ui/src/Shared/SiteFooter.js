import React from 'react';
import { Header, Segment, Container, Grid, Menu, Divider } from 'semantic-ui-react'
import './SiteFooter.css';


function SiteFooter() {
    return (
        <div inverted color='grey' className="footerContainer" centered>
            <Segment inverted centered>
                <Header className="headerContent" centered>
                    <Grid container centered fitted>
                    <Grid.Row>
                    <Menu fitted centered inverted secondary>
                            <Menu.Item link fitted name="About" />
                            <Menu.Item link fitted name="contact us" />
                        </Menu>
                    </Grid.Row>
                    <Divider horizontal inverted fitted style={{fontSize: '10px'}}>InfiniMuse</Divider>
                    </Grid>
                </Header>
            </Segment>
        </div>
    )
}
export default SiteFooter;