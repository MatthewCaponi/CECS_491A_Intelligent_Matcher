import { React, useReducer, useState, useContext } from 'react';
import { Header, Segment, Icon, Menu, Input, Container, Grid, Label, Dropdown, Button} from 'semantic-ui-react';
import './SiteHeader.css';
import { useCookies } from 'react-cookie';
import { useHistory } from 'react-router-dom';
import {AuthnContext } from '../Context/AuthnContext';

function SiteHeader() {
    const authnContext = useContext(AuthnContext);
    const [cookies, setCookie, removeCookie] = useCookies(['IdToken']);
    const history = useHistory();
    const buttonStyles = {
        backgroundColor: 'transparent',
        color: 'lightgrey'
    }

    const trigger = (
        <span>
            <Button icon="setting" className="message"/>
        </span>
    )

    function logout() {
        removeCookie('IdToken');
        authnContext.logout();
        history.push('/');
    }

    const authorized = true;
    return (
        <Segment inverted>
            <Grid columns={12}>
                <Grid.Column mobile={2} tablet={2} container textAlign="left" verticalAlign="center" width={1}>
                    <Header href="http://localhost:3000" inverted color="grey">                      
                        <Icon name='moon' />
                        InfiniMuse
                    </Header>  
                </Grid.Column>
                <Grid.Column container>
                <Menu inverted pointing secondary>
                    <Menu.Item href="http://localhost:3000" name='home' />
                    <Menu.Item link name='profile' />
                    <Menu.Item href="http://localhost:3000/ListingTable" name='Listings' />
                    </Menu>
                </Grid.Column>
                <Grid.Column container>
               
                </Grid.Column>
                <Grid.Column container />
                <Grid.Column container>
                    {/* <Button href="http://localhost:3000/Login" name='Login' circular inverted>
                        Login
                    </Button> */}
                </Grid.Column>
                <Grid.Column container only="computer"/>
                <Grid.Column container only="computer" />
                <Grid.Column container only="computer" only="tablet"/>
                <Grid.Column container floated="right" verticalAlign="center" width={2}>           
                    <Button href="http://localhost:3000/UserManagement" className={true ? 'adminVisible' : 'adminHidden'}circular inverted>
                        Admin
                    </Button>
                </Grid.Column>
                <Grid.Column container floated="right" width={2} widescreen={1} computer={2} tablet={2}>
                    <Grid columns={3}>
                        <Grid.Column >
                            <Button icon='conversation' className="message" />
                        </Grid.Column>
                        <Grid.Column >
                            <Button icon='bell' className="message"/>
                        </Grid.Column>
                        <Grid.Column >
                            <Dropdown
                                    trigger={trigger}
                                    direction="left"
                                    content = "none"
                                >
                                    <Dropdown.Menu>
                                    <Dropdown.Item icon='user' content='Account' />
                                    <Dropdown.Item icon='privacy' text='Privacy' />
                                    <Dropdown.Item icon='help' text='Help' />
                                    <Dropdown.Item icon='logout' text='Logout' onClick={logout} />
                                    </Dropdown.Menu>
                                </Dropdown>
                        </Grid.Column>
                    </Grid>
                </Grid.Column>
                <Grid.Column />
            </Grid>
        </Segment>
    )
}
export default SiteHeader;