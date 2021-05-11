import React, {useState, useEffect} from 'react';
import { Grid, Header, Divider, Label, Search, Container, Button } from 'semantic-ui-react'
import { useHistory } from 'react-router-dom';
import '../.././../../App'

import './Registration.css';

function Registration() {
    const [firstNameState, setFirstNameState] = useState("");
    const [surnameState, setSurnameState] = useState("");
    const [usernameState, setUsernameState] = useState("");
    const [passwordState, setPasswordState] = useState("");
    const [emailState, setEmailState] = useState("");
    const [dateOfBirthState, setDateOfBirthState] = useState("");
    const history = useHistory();

    function submitHandler(e){
        var RegistrationModel = e;
        // e.preventDefault();
        if(e.firstName != "" && e.surname != "" && e.username != "" && e.password != "" && e.emailAddress != "" &&
        e.dateOfBirth != "" && e.firstName.length <= 50 && e.surname.length <= 50 && e.username.length <= 50 &&
        e.emailAddress.length <= 50 && e.password.length <= 50 && e.password.length >= 8 && /\d/.test(e.password) &&
        /[A-Z]/.test(e.password) && /[a-z]/.test(e.password)){
            fetch(global.url + 'Registration/RegisterUser',
            {
            method: "POST",
            headers: {'Content-type':'application/json'},
            body: JSON.stringify(RegistrationModel)
            }).
            then(r => r.json()).then(res=>{
                if(res.success){
                    alert("Registration Success");
                    history.push("/ResendEmail", { accountId: res.accountId });
                }
                else{
                    alert(res.errorMessage);
                }
            }
            );
        }
        else if(e.firstName == "" || e.surname == "" || e.username == "" || e.password == "" || e.emailAddress == "" ||
        e.dateOfBirth == ""){
            alert("One of the inputs is empty!");
        }
        else if(e.firstName.length > 50 || e.surname.length > 50 || e.username.length > 50 ||
        e.password.length > 50 || e.emailAddress.length > 50){
            alert("One of the inputs is too long!")
        }
        else{
            alert("This password is invalid!")
        }
    }
    return (
        <Grid container>
        <Grid.Row>
            <h1>Registering A New User (Please Enter Your Information): </h1>
        </Grid.Row>
        <Grid.Row>
            <label htmlFor="firstName">
                First Name:
            </label>
        </Grid.Row>
        <Grid.Row verticalAlign="middle">
            <div class="ui input">
                <input type="text" name="firstname" placeholder="First Name" onChange={e => setFirstNameState(e.target.value)}/>
            </div>
        </Grid.Row>
        <Grid.Row>
            <label htmlFor="surname">
                Last Name:
            </label>
        </Grid.Row>
        <Grid.Row verticalAlign="middle">
            <div class="ui input">
                <input type="text" name="surname" placeholder="Last Name" onChange={e => setSurnameState(e.target.value)}/>
            </div>
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
                <input type="date" name="dateOfBirth" placeholder="MM/DD/YYYY"
                onChange={e => setDateOfBirthState(e.target.value)}/>
            </div>
        </Grid.Row>
        <Grid.Row>
            <Button
                onClick={()=>submitHandler({
                    firstName:firstNameState,
                    surname:surnameState,
                    username:usernameState,
                    password:passwordState,
                    emailAddress:emailState,
                    dateOfBirth:dateOfBirthState,
                    ipAddress:"127.0.0.1"
                })}
                compact size="tiny"
                circular inverted color="red"
            >
            Register
            </Button>
            <Button href={global.urlRoute + "Login"} compact size="tiny" circular inverted color="blue">
                Go Back to Login
            </Button>
        </Grid.Row>
        </Grid>
    )
}

export default Registration;