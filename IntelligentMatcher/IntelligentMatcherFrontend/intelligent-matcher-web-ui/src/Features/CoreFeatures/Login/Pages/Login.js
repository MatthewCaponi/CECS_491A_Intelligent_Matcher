import React, {useState, useEffect} from 'react';
import { BrowserRouter as Redirect } from 'react-router-dom'
import { Grid, Header, Divider, Label, Search, Container, Button } from 'semantic-ui-react'
import './Login.css';

function Login() {
    const [usernameState, setUsernameState] = useState("");
    const [passwordState, setPasswordState] = useState("");
    function submitHandler(e){
        var LoginModel = e;
        // e.preventDefault();
        fetch('http://localhost:5000/Login/Login',
        {
        method: "POST",
        headers: {'Content-type':'application/json'},
        body: JSON.stringify(LoginModel)
        }).
        then(r => r.json()).then(res=>{
            if(res.success){
                alert("Successful Login for " + res.username);
            }
            else{
                alert(res.errorMessage);
            }
        }
        );
    }

    return (
        <div>
            <Grid container>
            <Grid.Row>
                <h1>Login</h1>
            </Grid.Row>
            <Grid.Row>
                <label htmlFor="username">
                    Username:
                </label>
            </Grid.Row>
            <Grid.Row>
                <a href="http://localhost:3000/ForgotUsername">Forgot Username</a>
            </Grid.Row>
            <Grid.Row verticalAlign="middle">
                <div class="ui input">
                    <input type="text" name="username" placeholder="Username" onChange={e => setUsernameState(e.target.value)}/>
                </div>
            </Grid.Row>
            <Grid.Row>
                <label htmlFor="password">
                    Password:
                </label>
            </Grid.Row>
            <Grid.Row>
                <a href="http://localhost:3000/ForgotPasswordValidation">Forgot Password</a>
            </Grid.Row>
            <Grid.Row verticalAlign="middle">
                <div class="ui input">
                    <input type="password" name="password" placeholder="Password" onChange={e => setPasswordState(e.target.value)}/>
                </div>
            </Grid.Row>
            <Grid.Row>
                <Button
                    onClick={()=>submitHandler({
                        username:usernameState,
                        password:passwordState,
                        ipAddress:"127.0.0.3"
                    })}
                    compact size="tiny"
                    circular inverted color="red"
                >
                Login
                </Button>
                <Button href="http://localhost:3000/Registration" compact size="tiny" circular inverted color="blue">Register New User</Button>
            </Grid.Row>
            </Grid>
        </div>
        
    )

}

export default Login;