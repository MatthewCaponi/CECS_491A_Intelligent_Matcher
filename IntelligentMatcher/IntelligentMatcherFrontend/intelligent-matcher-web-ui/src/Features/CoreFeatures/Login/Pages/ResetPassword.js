import React, {useState, useEffect} from 'react';
import { Grid, Header, Divider, Label, Search, Container, Button } from 'semantic-ui-react'
import './Login.css';

function ResetPassword(props) {
    const [accountState, setAccountState] = useState(1);
    const [passwordState, setPasswordState] = useState("");
    function submitHandler(e){
        var ResetPasswordModel = e;
        // e.preventDefault();
        fetch('http://localhost:5000/Login/ResetPassword',
        {
        method: "POST",
        headers: {'Content-type':'application/json'},
        body: JSON.stringify(ResetPasswordModel)
        }).
        then(r => r.json()).then(res=>{
            if(res.success){
                alert("Password Changed Successfully! Return to Login Page!");
            }
            else{
                alert(res.errorMessage);
            }
        }
        );
    }
    return (
        <Grid container>
        <Grid.Row>
            <h1>Reset Password: </h1>
        </Grid.Row>
        <Grid.Row>
            <label htmlFor="password">
                Password:
            </label>
        </Grid.Row>
        <Grid.Row>
            <p>
                - Password Must Be At Least 8 Characters Long   
            </p>
        </Grid.Row>
        <Grid.Row>
            <p>
                - Password Must Countain At Least 1 Number
            </p>
        </Grid.Row>
        <Grid.Row>
            <p>
                - Password Must Contain At Least 1 Capital Letter  
            </p>
        </Grid.Row>
        <Grid.Row>
            <p>
                - Password Must Countain At Least 1 Lowercase Letter
            </p>
        </Grid.Row>
        <Grid.Row verticalAlign="middle">
            <div class="ui input">
                <input type="password" name="password" placeholder="Password" onChange={e => setPasswordState(e.target.value)}/>
            </div>
        </Grid.Row>
        <Grid.Row>
            <Button
                onClick={()=>submitHandler({
                    password:passwordState,
                    accountId:accountState
                })}
                compact size="tiny"
                circular inverted color="red"
            >
            Submit
            </Button>
            <Button href="http://localhost:3000/Login" compact size="tiny" circular inverted color="blue">
                Go Back to Login
            </Button>
        </Grid.Row>
        </Grid>
    )
}

export default ResetPassword;