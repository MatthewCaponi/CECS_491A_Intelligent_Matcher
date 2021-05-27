import React, {useState, useEffect} from 'react';
import { Grid, Header, Divider, Label, Search, Container, Button, Input } from 'semantic-ui-react'
import './Login.css';
import { useHistory } from 'react-router-dom';
import '../.././../../index'

function ForgotPasswordCodeInput() {
    const history = useHistory();
    const [accountState, setAccountState] = useState(history.location.state.accountId);
    const [codeState, setCodeState] = useState("");

    //history.location.state.accountId

    function submitHandler(e){
        var ForgotPasswordCodeInputModel = e;
        // e.preventDefault();
        if(e.code != ""){
            fetch(global.url + 'Login/ForgotPasswordCodeInput',
            {
            method: "POST",
            headers: {'Content-type':'application/json',
            'Scope': 'id'},
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
                    <Grid.Column width={8} textAlign='center'>
                        <Header size="large">Enter The Code Sent to Your Email</Header>
                    </Grid.Column>
                </Grid.Row>
                <Divider />
                <Divider section />
                <Grid.Row verticalAlign="middle">
                    <Grid.Column  mobile={16} tablet={8} computer={4}>
                        <Input type="text" name="code" placeholder="Code" onChange={e => setCodeState(e.target.value)}/>
                    </Grid.Column>
                </Grid.Row>
                <Grid.Row>
                <Grid.Column centered mobile={6} tablet={4} computer={2}>
                    <Button
                        onClick={()=>submitHandler({
                            code:codeState,
                            accountId:accountState
                        })}
                        compact size="medium"
                        circular inverted color="blue"
                    >
                    Submit
                    </Button>
                </Grid.Column>
                <Grid.Column centered mobile={6} tablet={4} computer={2}>
                    <Button href={global.urlRoute} compact size="medium" circular inverted color="violet">
                        Login
                    </Button>
                </Grid.Column>  
                   
                </Grid.Row>
            </Grid>
        </div>
    )
}

export default ForgotPasswordCodeInput;