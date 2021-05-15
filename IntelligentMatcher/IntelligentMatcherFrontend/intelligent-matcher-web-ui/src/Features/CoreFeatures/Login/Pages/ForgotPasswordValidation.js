import React, {useState, useEffect} from 'react';
import { Grid, Header, Divider, Label, Search, Container, Button } from 'semantic-ui-react'
import { useHistory } from 'react-router-dom';
import '../.././../../index'

import './Login.css';

function ForgotPasswordValidation() {
    const [usernameState, setUsernameState] = useState("");
    const [emailState, setEmailState] = useState("");
    const [dateOfBirthState, setDateOfBirthState] = useState("");
    const history = useHistory();

    function submitHandler(e){
        var ForgotInformationModel = e;
        // e.preventDefault();
        if(e.username != "" && e.emailAddress != "" && e.dateOfBirth != ""){
            fetch(global.url + 'Login/ForgotPasswordValidation',
            {
            method: "POST",
            headers: {'Content-type':'application/json',
            'Scope': 'id'},
            body: JSON.stringify(ForgotInformationModel)
            }).
            then(r => r.json()).then(res=>{
                if(res.success){
                    alert("A Code Has Been Emailed to You.");
                    history.push("/ForgotPasswordCodeInput", { accountId: res.accountId });
                }
                else{
                    alert(res.errorMessage);
                }
            }
            );
        }
        else{
            alert("Input is Empty");
        }
    }

    return (
        <Grid container centered>
        <Grid.Row></Grid.Row>
        <Grid.Row></Grid.Row>
        <Grid.Row></Grid.Row>
        <Grid.Row></Grid.Row>
        <Grid.Row></Grid.Row>
        <Grid.Row></Grid.Row>
        <Grid.Row></Grid.Row>
        <Grid.Row>
            <h1>Forgot Password: </h1>
        </Grid.Row>
        <Grid.Row></Grid.Row>
        <Grid.Row verticalAlign="middle">
            <div class="ui input">
                <input type="text" name="username" placeholder="Username" onChange={e => setUsernameState(e.target.value)}/>
            </div>
        </Grid.Row>
        <Grid.Row></Grid.Row>
        <Grid.Row verticalAlign="middle">
            <div class="ui input">
                <input type="email" name="emailAddress" placeholder="Email Address" onChange={e => setEmailState(e.target.value)}/>
            </div>
        </Grid.Row>
        <Grid.Row>
            <label htmlFor="dateOfBirth">
                Date Of Birth:
            </label>
        </Grid.Row>
        <Grid.Row verticalAlign="middle">
            <div class="ui input">
                <input type="date" name="dateOfBirth" placeholder="MM/DD/YYYY" onChange={e => setDateOfBirthState(e.target.value)}/>
            </div>
        </Grid.Row>
        <Grid.Row>
            <Button
                onClick={()=>submitHandler({
                    username:usernameState,
                    emailAddress:emailState,
                    dateOfBirth:dateOfBirthState
                })}
                compact size="tiny"
                circular inverted color="red"
            >
            Submit
            </Button>
            <Button href={global.urlRoute} compact size="tiny" circular inverted color="blue">
                Go Back To Login
            </Button>
        </Grid.Row>
        </Grid>
    )
}

export default ForgotPasswordValidation;