import React, {useState, useEffect} from 'react';
import { Grid, Header, Divider, Label, Search, Container, Button } from 'semantic-ui-react'
import './Login.css';
import { useHistory } from 'react-router-dom';
import '../.././../../index'

function ForgotPasswordCodeInput() {
    const history = useHistory();
    const [accountState, setAccountState] = useState(0);
    const [codeState, setCodeState] = useState("");
    if(history.location.state == undefined){
        history.push("/");
    }
    else{
        setAccountState(history.location.state.accountId);
    }
    //history.location.state.accountId

    function submitHandler(e){
        var ForgotPasswordCodeInputModel = e;
        // e.preventDefault();
        if(e.code != ""){
            fetch(global.url + 'Login/ForgotPasswordCodeInput',
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
        <Grid container centered>
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
            <Button href={global.urlRoute} compact size="tiny" circular inverted color="blue">
                Go Back to Login
            </Button>
        </Grid.Row>
        </Grid>
    )
}

export default ForgotPasswordCodeInput;