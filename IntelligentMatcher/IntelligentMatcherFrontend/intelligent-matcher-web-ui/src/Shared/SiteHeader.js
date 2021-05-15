import { React, useReducer, useState, useContext, useEffect } from 'react';
import { Header, Segment, Icon, Menu, Input, Container, Grid, Label, Dropdown, Button} from 'semantic-ui-react';
import './SiteHeader.css';
import { useCookies } from 'react-cookie';
import { useHistory } from 'react-router-dom';
import jwt from 'jwt-decode';
import {AuthnContext } from '../Context/AuthnContext';
import '../App'


function SiteHeader() {
    const authnContext = useContext(AuthnContext);
    const [cookies, setCookie, removeCookie] = useCookies(['IdToken']);
    const [authorized, setAuthorized] = useState(false);
    const history = useHistory();
    const cookie = cookies['IdToken'];
    const decodedCookie = jwt(cookie);
    console.log(decodedCookie.role);
    const buttonStyles = {
        backgroundColor: 'transparent',
        color: 'lightgrey'
    }
    var admin = "admin";
    var user = "user";

    useEffect(() => {
        try {
          isAdmin()
          } catch (error) {
          console.log(error.message);
        }
        }, [])
    const trigger = (
        <span>
            <Button icon="setting" className="message"/>
        </span>
    )

    function isAdmin() {
        const userRole = decodedCookie.role;
        if (userRole == admin){
            setAuthorized(true);
        }
    }

    function logout() {
        removeCookie('IdToken');
        removeCookie('AccessToken');
        authnContext.logout();
        history.push('/');
    }

    return (
        <Segment inverted>
            <Grid columns={12}>
                <Grid.Column mobile={2} tablet={2} container textAlign="left" verticalAlign="center" width={1}>
                    <Header href={global.urlRoute} inverted color="grey">
                        <Icon name='moon' />
                        InfiniMuse
                    </Header>
                </Grid.Column>
                <Grid.Column container>
                <Menu inverted pointing secondary>
                    <Menu.Item href={global.urlRoute} name='home' />
                    <Menu.Item link name='profile' href={global.urlRoute + "profile"}/>
                    <Menu.Item href={global.urlRoute + "ListingTable"} name='Listings' />
                    </Menu>
                </Grid.Column>
                <Grid.Column container>

                </Grid.Column>
                <Grid.Column container />
                <Grid.Column container>
                </Grid.Column>
                <Grid.Column container only="computer"/>
                <Grid.Column container floated="right" verticalAlign="center" width={2}>
                </Grid.Column>
                <Grid.Column container only="computer" only="tablet"/>
                <Grid.Column container floated="right" verticalAlign="center" width={2}>
                    <Button href={global.urlRoute + "AdminDashboard"} onChange={isAdmin} className={authorized ? 'adminVisible' : 'adminHidden'}circular inverted>
                        Admin
                    </Button>
                </Grid.Column>
                <Grid.Column container floated="right" width={2} widescreen={1} computer={2} tablet={2}>
                    <Grid columns={3}>
                        <Grid.Column >
                            <Button href={global.urlRoute + "Messaging"} icon='conversation' className="message" />
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
                                    <Dropdown.Item icon='user' content='Account' href={global.urlRoute + "UserAccountSettings"}/>
                                    <Dropdown.Item icon='privacy' text='Privacy' />
                                    <Dropdown.Item icon='help' text='Help' href={global.urlRoute + "Help"}/>
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
