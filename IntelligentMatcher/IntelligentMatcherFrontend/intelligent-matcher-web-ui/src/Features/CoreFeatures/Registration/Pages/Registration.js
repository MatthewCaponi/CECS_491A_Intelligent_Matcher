import React, {useState, useEffect} from 'react';
import { Grid, Header, Divider, Label, Search, Container, Button, Checkbox } from 'semantic-ui-react'
import { useHistory } from 'react-router-dom';
import '../.././../../index'

import './Registration.css';

function Registration() {
    const [firstNameState, setFirstNameState] = useState("");
    const [surnameState, setSurnameState] = useState("");
    const [usernameState, setUsernameState] = useState("");
    const [passwordState, setPasswordState] = useState("");
    const [emailState, setEmailState] = useState("");
    const [dateOfBirthState, setDateOfBirthState] = useState("");
    const [passwordShown, showPassword] = useState(true);
    const history = useHistory();

    function showPasswordHandler(){
        showPassword(!passwordShown);
    };
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
            headers: {'Content-type':'application/json',
            'Scope': 'id'},
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
        <Grid relaxed stackable columns={3} container centered>
            <Grid.Row></Grid.Row>
            <Grid.Row></Grid.Row>
            <Grid.Row></Grid.Row>
            <Grid.Row></Grid.Row>
            <Grid.Row></Grid.Row>
            <Grid.Row></Grid.Row>
            <Grid.Row></Grid.Row>
            <Grid.Row></Grid.Row>
            <Grid.Row></Grid.Row>
            <Grid.Row centered verticalAlign size="massive"><Header>Registration</Header></Grid.Row>
           
            <Grid.Row></Grid.Row>
            <Grid.Row></Grid.Row>
            <Grid.Row></Grid.Row>
            <Grid.Row></Grid.Row>
            <Grid.Row></Grid.Row>
            <Grid.Row>
            
                <Grid.Column width={4}>
                    <Grid.Row>
                        <div class="ui input">
                            <input type="text" name="firstname" placeholder="First Name" onChange={e => setFirstNameState(e.target.value)}/>
                        </div>
                    </Grid.Row>
                    <Grid.Row><Divider /></Grid.Row>
                    <Grid.Row verticalAlign="middle">
                        <div class="ui input">
                            <input type="text" name="username" placeholder="Username" onChange={e => setUsernameState(e.target.value)}/>
                        </div>
                    </Grid.Row>
                    <Grid.Row><Divider /></Grid.Row>
                    <Grid.Row verticalAlign="middle">
                        <div class="ui input">
                            <input type="email" name="emailAddress" placeholder="Email Address" onChange={e => setEmailState(e.target.value)}/>
                        </div>
                    </Grid.Row>
                </Grid.Column>
                <Grid.Column width={4}>
                    <Grid.Row verticalAlign="middle">
                        <div class="ui input">
                            <input type="text" name="surname" placeholder="Last Name" onChange={e => setSurnameState(e.target.value)}/>
                        </div>
                    </Grid.Row>
                    <Grid.Row><Divider /></Grid.Row>
                    <Grid.Row verticalAlign="middle" centered>
                        <div class="ui input">
                            <input type={passwordShown ? "password" : "text"} name="password" placeholder="Password" onChange={e => setPasswordState(e.target.value)}/>
                        </div>
                        <div>
                        <Checkbox color="violet" size='mini' onClick={showPasswordHandler} label="Show Password"></Checkbox>
                        </div>
                    </Grid.Row>
                    <Grid.Row><Divider /></Grid.Row>
                    <Grid.Row verticalAlign="middle">
                        <div class="ui input">
                            <input type="date" name="dateOfBirth" placeholder="MM/DD/YYYY"
                            onChange={e => setDateOfBirthState(e.target.value)}/>
                        </div>
                </Grid.Row>
                </Grid.Column>
            </Grid.Row>
            <Grid.Column></Grid.Column>
            <Grid.Column>
            <Grid.Row><Divider /></Grid.Row>
            <Grid.Column centered width={9}>
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
                     size="large"
                    circular inverted color="red"
                >
                Register
                </Button>
                <Button href={global.urlRoute}  size="large" circular inverted color="blue">
                    Go Back to Login
                </Button>
            </Grid.Row>
            </Grid.Column>
           
            </Grid.Column>
        <Grid.Column></Grid.Column>
        
        </Grid>
    )
}

export default Registration;