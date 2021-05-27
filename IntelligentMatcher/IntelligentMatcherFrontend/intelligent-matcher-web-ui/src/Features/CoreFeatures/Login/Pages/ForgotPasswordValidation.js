import React, {useState, useEffect} from 'react';
import { Grid, Header, Divider, Label, Search, Container, Button, Input } from 'semantic-ui-react'
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
                            <Header size="huge">Forgot Password</Header>
                    </Grid.Column>
                </Grid.Row>
                <Divider />
                <Divider section />
                <Grid.Row></Grid.Row>
                <Grid.Row>
                    <Grid.Column mobile={16} tablet={8} computer={4}>
                        <Input type="text" name="username" placeholder="Username" onChange={e => setUsernameState(e.target.value)}/>
                    </Grid.Column>
                </Grid.Row>
                <Grid.Row>
                    <Grid.Column mobile={16} tablet={8} computer={4}>
                            <Input type="email" name="emailAddress" placeholder="Email Address" onChange={e => setEmailState(e.target.value)}/>
                    </Grid.Column>
                </Grid.Row>
                <Grid.Row>
                    <Grid.Column mobile={16} tablet={8} computer={4}>
                        <Input type="date" name="dateOfBirth" placeholder="MM/DD/YYYY" onChange={e => setDateOfBirthState(e.target.value)}/>
                    </Grid.Column>
                </Grid.Row>
                <Grid.Row>
                    <Grid.Column centered mobile={6} tablet={4} computer={2}>
                        <Button
                                onClick={()=>submitHandler({
                                    username:usernameState,
                                    emailAddress:emailState,
                                    dateOfBirth:dateOfBirthState
                                })}
                                compact size="large"
                                circular inverted color="blue"
                            >
                            Submit
                        </Button>
                    </Grid.Column>
                    <Grid.Column centered mobile={6} tablet={4} computer={2}>
                        <Button href={global.urlRoute} compact size="large" circular inverted color="violet">
                            Login
                        </Button>
                    </Grid.Column>  
                </Grid.Row>
            </Grid>
        </div>
    )
}

export default ForgotPasswordValidation;