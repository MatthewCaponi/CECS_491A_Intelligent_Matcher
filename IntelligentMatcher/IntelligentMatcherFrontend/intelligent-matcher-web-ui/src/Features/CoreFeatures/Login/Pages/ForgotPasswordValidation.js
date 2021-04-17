import React, {useState, useEffect} from 'react';
import { Grid, Header, Divider, Label, Search, Container, Button } from 'semantic-ui-react'
import './Login.css';

function ForgotPasswordValidation() {
    const [usernameState, setUsernameState] = useState("");
    const [emailState, setEmailState] = useState("");
    const [dateOfBirthState, setDateOfBirthState] = useState("2000-01-01");
    function submitHandler(e){
        var ForgotInformationModel = e;
        // e.preventDefault();
        fetch('http://localhost:5000/Login/ForgotPasswordValidation',
        {
        method: "POST",
        headers: {'Content-type':'application/json'},
        body: JSON.stringify(ForgotInformationModel)
        }).
        then(r => r.json()).then(res=>{
            if(res.success){
                alert("Enter: " + res.code + " in the next page.");
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
            <h1>Forgot Password (Please Enter Your Information): </h1>
        </Grid.Row>
        <Grid.Row>
            <label htmlFor="username">
                Username:
            </label>
        </Grid.Row>
        <Grid.Row verticalAlign="middle">
            <div class="ui input">
                <input type="text" name="username" placeholder="Username" onChange={e => setUsernameState(e.target.value)}/>
            </div>
        </Grid.Row>
        <Grid.Row>
            <label htmlFor="emailAddress">
                Email Address:
            </label>
        </Grid.Row>
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
            <Button href="http://localhost:3000/Login" compact size="tiny" circular inverted color="blue">Go Back</Button>
            <Button href="http://localhost:3000/ForgotPasswordCodeInput" compact size="tiny" circular inverted color="blue">
                To Enter The Code
            </Button>
        </Grid.Row>
        </Grid>
    )
}

export default ForgotPasswordValidation;