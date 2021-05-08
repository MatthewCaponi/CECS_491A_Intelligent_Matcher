import React, {useState, useEffect} from 'react';
import { Grid, Header, Divider, Label, Search, Container, Button } from 'semantic-ui-react'
import { useHistory } from 'react-router-dom';
import { useLocation } from 'react-router-dom';

import './Login.css';

function Login() {
    const history = useHistory();

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
            if(res.success)
            {
                var model =  {Username: res.username};
                fetch("http://localhost:5000/TrackData/TrackLogin", {
                    method: "POST",
                    headers: {'Content-type':'application/json'},
                    body: JSON.stringify(model)
                    });
                
                alert("Successful Login for " + res.username);
                history.push("/Home", { username: res.username, accountType: res.accountType, accountStatus: res.accountStatus });

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
                {confMessage}
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