import React, {useState, useEffect, useContext} from 'react';
import { Grid, Header, Divider, Label, Search, Container, Button} from 'semantic-ui-react'
import { useHistory } from 'react-router-dom';
import { useLocation } from 'react-router-dom';
import { AuthnContext } from '../../../../Context/AuthnContext';
import jwt from 'jwt-decode';
import { getCookie } from 'react-use-cookie';
import '../.././../../index'

import './Login.css';
import { get } from 'http';

function Login() {
    const history = useHistory();
    const authnContext = useContext(AuthnContext);
    let location = useLocation()
    let confMessage = "";

    try{
        if(location.state.message == "UserConfirmed"){
            confMessage = "Account Confirmed, please login";
        }
    }
    catch{
        
    }

    const [usernameState, setUsernameState] = useState("");
    const [passwordState, setPasswordState] = useState("");
    const [token, setToken] = useState('');


    function submitHandler(e){
        var LoginModel = e;
        // e.preventDefault();
        if(e.username != "" && e.password != "" ){
            fetch(global.url + 'Login/Login',
            {
            method: "POST",
            headers: {'Content-type':'application/json',
            'Accept': 'application/json',
        'Scope': 'id'},
        credentials: 'include',
            body: JSON.stringify(LoginModel)
            }).
            then(r => r.json()).then(res=>{
                if(res){
                    var idCookie = getCookie('IdToken');
                    var accessCookie = getCookie('AccessToken');
                    const idToken = jwt(idCookie);
                    const accessToken = jwt(accessCookie);
                    setToken(idToken);
                    authnContext.login();
                    
                    history.push("/", { username: idToken.username, accountType: idToken.accountType, accountStatus: idToken.accountStatus });
                }
                else{
                    alert(res);
                    console.log(res);
                }
            }
            );
        }
        else{
            alert("Input is Empty");
        }
    }

    return (
        <div>
            <Grid container centered>
            <Grid.Row></Grid.Row>
            <Grid.Row></Grid.Row>
            <Grid.Row></Grid.Row>
            <Grid.Row></Grid.Row>
            <Grid.Row></Grid.Row>
            <Grid.Row>
                <h1>Login</h1>
            </Grid.Row>
            <Grid.Row>
                {confMessage}
            </Grid.Row>
            <Grid.Row verticalAlign="middle">
                <div class="ui input">
                    <input type="text" name="username" placeholder="Username" onChange={e => setUsernameState(e.target.value)}/>
                </div>
            </Grid.Row>
            <Grid.Row verticalAlign="middle">
                <div class="ui input">
                    <input type="password" name="password" placeholder="Password" onChange={e => setPasswordState(e.target.value)}/>
                </div>
            </Grid.Row>
            <Grid.Row>
                <Grid.Column width={2}>
                <a href="http://localhost:3000/ForgotUsername">Forgot Username</a>
                </Grid.Column>
                <Grid.Column width={2}>
                <a href="http://localhost:3000/ForgotPasswordValidation">Forgot Password</a>
                </Grid.Column>
            </Grid.Row>
            <Grid.Row>
                <Button
                    onClick={()=>submitHandler({
                        username:usernameState,
                        password:passwordState,
                        ipAddress:"127.0.0.1"
                    })}
                    compact size="large"
                    circular inverted color="violet"
                >
                Login
                </Button>
                <Button href="http://localhost:3000/Registration" compact size="large" circular inverted color="blue">Register New User</Button>
            </Grid.Row>
            </Grid>
        </div>
        
    )

}

export default Login;