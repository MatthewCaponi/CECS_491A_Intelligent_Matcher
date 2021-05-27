import React, {useState, useEffect} from 'react';
import { Grid, Header, Divider, Label, Search, Container, Button, Checkbox, Input, Loader, Segment, Dimmer } from 'semantic-ui-react'
import { useHistory } from 'react-router-dom';
import '../.././../../index'

import './Registration.css';
import LoaderRow from '../Components/LoaderRow';

function Registration() {
    const [firstNameState, setFirstNameState] = useState("");
    const [surnameState, setSurnameState] = useState("");
    const [usernameState, setUsernameState] = useState("");
    const [passwordState, setPasswordState] = useState("");
    const [emailState, setEmailState] = useState("");
    const [dateOfBirthState, setDateOfBirthState] = useState("");
    const [passwordShown, showPassword] = useState(true);
    const history = useHistory();
    const [isValidationError, setIsValidationError] = useState(false);
    const [validationError, setValidationError] = useState("");
    const [isRegistering, setIsRegistering] = useState(false);

    function showPasswordHandler(){
        showPassword(!passwordShown);
    };
    function submitHandler(e){
        setIsRegistering(true);
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
      
    return (
        <div style={divStyle}>
            <Grid container stackable columns={3} centered stretched style={gridStyle}>
                <Grid.Row>
                    <Grid.Column textAlign='center'>
                        <Header size="huge">Registration</Header>
                    </Grid.Column>
                </Grid.Row>
                <Divider />
                <Divider section />
                    {isRegistering && <LoaderRow />}
                <Grid.Row/>
                <Grid.Row>
                    <Grid.Column mobile={16} tablet={8} computer={4}>
                        <Input type="text" name="firstname" placeholder="First Name" onChange={e => setFirstNameState(e.target.value)}/>
                    </Grid.Column>
                    <Grid.Column mobile={16} tablet={8} computer={4}>
                        <Input type="text" name="surname" placeholder="Last Name" onChange={e => setSurnameState(e.target.value)}/>
                    </Grid.Column>
                </Grid.Row>
                <Grid.Row>
                    <Grid.Column mobile={16} tablet={8} computer={4}>
                        <Input type="email" name="emailAddress" placeholder="Email Address" onChange={e => setEmailState(e.target.value)}/>
                    </Grid.Column>
                    <Grid.Column mobile={16} tablet={8} computer={4}>
                        <Input type="date" name="dateOfBirth" placeholder="MM/DD/YYYY" onChange={e => setDateOfBirthState(e.target.value)}/>
                    </Grid.Column>
                </Grid.Row>
                <Grid.Row centered>
                    <Grid.Column mobile={16} tablet={8} computer={4}>
                        <Input type="text" name="username" placeholder="Username" onChange={e => setUsernameState(e.target.value)}/>
                    </Grid.Column>
                    <Grid.Column mobile={16} tablet={8} computer={4}>
                        <Input type={passwordShown ? "password" : "text"} name="password" placeholder="Password" onChange={e => setPasswordState(e.target.value)}/> 
                    </Grid.Column>      
                </Grid.Row>
                <Grid.Column/>
                <Grid.Column>
                    <Checkbox compact size="mini" onClick={showPasswordHandler} label="Show Password"></Checkbox> 
                </Grid.Column>
                <Grid.Row>
                    <Grid.Column centered mobile={16} tablet={5} computer={2}>
                                <Button compact
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
                                circular inverted color="blue"
                            >
                            Register
                            </Button>
                    </Grid.Column>
                    <Grid.Column  centered mobile={16} tablet={5} computer={2}>
                        <Button circular ribbon="right" compact href={global.urlRoute}  size="large" inverted color="violet">
                            Back
                        </Button>
                    </Grid.Column>
                </Grid.Row>
            </Grid>  
        </div>
       
    )
}

export default Registration;