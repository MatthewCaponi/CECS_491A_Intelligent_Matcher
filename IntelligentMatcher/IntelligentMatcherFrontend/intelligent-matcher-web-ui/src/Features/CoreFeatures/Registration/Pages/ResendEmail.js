import React, {useState, useEffect} from 'react';
import { Grid, Header, Divider, Label, Search, Container, Button } from 'semantic-ui-react'
import { useHistory } from 'react-router-dom';

import './Registration.css';

function ResendEmail() {
    const history = useHistory();
    console.log(history);
    const [accountState, setAccountState] = useState(1);
    function submitHandler(e){
        var accountId = e;
        // e.preventDefault();
        fetch('http://localhost:5000/Registration/ResendEmail',
        {
        method: "POST",
        headers: {'Content-type':'application/json'},
        body: JSON.stringify(accountId)
        }).
        then(r => r.json()).then(res=>{
            if(res){
                alert("Email Has been sent.");
            }
            else{
                alert("Email was not sent. You may have entered a bad email.");
            }
        }
        );
    }
    return (
        <Grid container>
        <Grid.Row>
            <h1>Your account has been registered!{history.location.state.accountId} </h1>
        </Grid.Row>
        <Grid.Row>
            This account is still inactive because the email was not verified!
        </Grid.Row>
        <Grid.Row>
            If you have not received the email to verify your email, press the resend button.
        </Grid.Row>
        <Grid.Row>
            The link will expire 3 hours after you registered.
        </Grid.Row>
        <Grid.Row>
            <Button
                onClick={()=>submitHandler(accountState)}
                compact size="tiny"
                circular inverted color="red"
            >
            Resend Email
            </Button>
            <Button href="http://localhost:3000/Login" compact size="tiny" circular inverted color="blue">
                Go Back to Login
            </Button>
        </Grid.Row>
        </Grid>
    )
}

export default ResendEmail;