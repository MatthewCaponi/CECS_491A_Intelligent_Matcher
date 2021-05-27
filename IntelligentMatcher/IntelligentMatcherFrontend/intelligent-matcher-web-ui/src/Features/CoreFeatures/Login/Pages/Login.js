import React, {useState, useEffect, useContext} from 'react';
import { Grid, Header, Divider, Label, Search, Container, Button, Checkbox, Input, Segment} from 'semantic-ui-react'
import { useHistory } from 'react-router-dom';
import { useLocation } from 'react-router-dom';
import { AuthnContext } from '../../../../Context/AuthnContext';
import jwt from 'jwt-decode';
import { getCookie } from 'react-use-cookie';
import '../.././../../index'

import './Login.css';
import { get } from 'http';
import { Alert } from 'bootstrap';

function Login() {
    const history = useHistory();
    const authnContext = useContext(AuthnContext);
    let location = useLocation()
    const [passwordShown, showPassword] = useState(true);
    let confMessage = "";
    const [isValidationError, setIsValidationError] = useState(false);
    const [validationError, setValidationError] = useState("");

    try{
        if(location.state.message == "UserConfirmed"){
            confMessage = "Account Confirmed. Please login";
        }
    }
    catch{
        
    }

    const [usernameState, setUsernameState] = useState("");
    const [passwordState, setPasswordState] = useState("");
    const [token, setToken] = useState('');

    function showPasswordHandler(){
        showPassword(!passwordShown);
    };

    function submitHandler(e){
        var LoginModel = {
            username: e.username,
            password: e.password,
            ipAddress: e.ipAddress
        }
        console.log(e.username)
        // e.preventDefault();
        if(e.username != "" && e.password != "" ){
            setUsernameState("");
            setPasswordState("");
            try {
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
                    if (res.status == 401){
                        setIsValidationError(true);
                        setValidationError("Username or Password was Invalid");
                    } else if (res.status == 400){
                        setIsValidationError(true);
                        setValidationError("Invalid input");
                    } else if (res.status == 500) {
                        setIsValidationError(true);
                        setValidationError("Something went wrong. Try again later.");
                    } else if (res.status == 429) {
                        setIsValidationError(true);
                        setValidationError("Too many attempts. Try again later.");
                    }else {
                        console.log(res);
                        var idCookie = getCookie('IdToken');
                        var accessCookie = getCookie('AccessToken');
                        const idToken = jwt(idCookie);
                        const accessToken = jwt(accessCookie);
                        setToken(idToken);
                        authnContext.login();
                        
                        history.push("/", { username: idToken.username, accountType: idToken.accountType, accountStatus: idToken.accountStatus });
                    }
                }
                else{
                    setIsValidationError(true);
                    setValidationError("Something went wrong. Try again later.");
                }
            }
            ); 
            } catch (error){
                console.log(error);
            }
           
        }
        else{
            setIsValidationError(true);
            setValidationError("Input cannot be empty");
        }
    }

    const gridStyle = {
        display: 'flex',
        alignItems: 'center',
        flexWrap: 'wrap'
      };

      const divStyle = {
        display: 'flex',
        alignItems: 'center',
        height: '90%',
        position: 'relative',
        flexWrap: 'wrap'
      };

      const userstyle = {
        position: 'absolute',
        margin: '10px'

      };

    return (
        <div style={divStyle}>
            <Grid container stackable columns={3} centered stretched style={gridStyle}>
                <Grid.Row>
                    <Grid.Column textAlign='center'>
                        <Header size="huge">Login</Header>
                    </Grid.Column>
                    </Grid.Row>
                    <Divider />
                    <Divider section />
                <Grid.Row>
                    {confMessage != "" && <Label color="green" prompt>{confMessage}</Label>}
                    {isValidationError == true ? <Label basic color="red">{validationError}</Label> : null}
                </Grid.Row>
                <Grid.Row>
                <Grid.Column mobile={16} tablet={8} computer={4}>
                    <Input value={usernameState} type="text" name="username" placeholder="Username" onClick={e => {
                        setIsValidationError(false);
                        setValidationError("");
                    }} onChange={e => {
                        setUsernameState(e.target.value);
                    }}/>
                </Grid.Column>  
                </Grid.Row>
                <Grid.Row>
                    <Grid.Column mobile={16} tablet={8} computer={4}>
                    <Input value={passwordState} type={passwordShown ? "password" : "text"} name="password" placeholder="Password" onClick={e => {
                        setIsValidationError(false);
                        setValidationError("");
                    }} onChange={e => {
                        setPasswordState(e.target.value);
                    }}/>
                    <div>
                        <Checkbox color="violet" style={userstyle} size='mini' onClick={showPasswordHandler} label="Show Password"></Checkbox>
                    </div>
                    </Grid.Column>
                </Grid.Row>
                <Grid.Row />
                <Grid.Row>
                    <Grid.Column mobile={8} tablet={4} computer={2}>
                        <Label link ribbon href={global.urlRoute + "ForgotUsername"}>Forgot Username</Label>
                    </Grid.Column>
                    <Grid.Column mobile={8} tablet={4} computer={2}>
                        <Label link ribbon="right" href={global.urlRoute + "ForgotPasswordValidation"}>Forgot Password</Label>
                    </Grid.Column>
                </Grid.Row>
                <Grid.Row>
                    <Grid.Column centered mobile={6} tablet={4} computer={2}>
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
                    </Grid.Column>
                    <Grid.Column centered mobile={6} tablet={4} computer={2}>
                        <Button href={global.urlRoute + "Registration"} compact size="large" circular inverted color="blue">Register</Button>
                    </Grid.Column>
                </Grid.Row>
            </Grid>
        </div>
        
    )

}

export default Login;