import React, {useState, useEffect} from 'react';
import { Grid, Header, Divider, Label, Search, Container, Button } from 'semantic-ui-react'
import './Login.css';
import { useHistory } from 'react-router-dom';

function ForgotPasswordCodeInput() {
    const history = useHistory();
    const [accountState, setAccountState] = useState(history.location.state.accountId);
    const [codeState, setCodeState] = useState("");
    //history.location.state.accountId

    function submitHandler(e){
        var ForgotPasswordCodeInputModel = e;
        // e.preventDefault();
        if(e.code != ""){
            fetch('http://localhost:5000/Login/ForgotPasswordCodeInput',
            {
            method: "POST",
            headers: {'Content-type':'application/json'},
            body: JSON.stringify(ForgotPasswordCodeInputModel)
            }).
            then(r => r.json()).then(res=>{
                if(res.success){
                    alert("Code Entered Successfully! Moving to Next Page!");
                    history.push("/ResetPassword", { accountId: res.accountId });
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
        <Grid container>
        <Grid.Row>
            <h1>Forgot Password (Please Enter The Code Emailed To You): </h1>
        </Grid.Row>
        <Grid.Row>
            <label htmlFor="code">
                Code:
            </label>
        </Grid.Row>
        <Grid.Row verticalAlign="middle">
            <div class="ui input">
                <input type="text" name="code" placeholder="Code" onChange={e => setCodeState(e.target.value)}/>
            </div>
        </Grid.Row>
        <Grid.Row>
            <Button
                onClick={()=>submitHandler({
                    code:codeState,
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

export default ForgotPasswordCodeInput;