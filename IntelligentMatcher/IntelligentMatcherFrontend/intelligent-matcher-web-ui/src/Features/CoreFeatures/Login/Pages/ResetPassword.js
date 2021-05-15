import React, {useState, useEffect} from 'react';
import { Grid, Header, Divider, Label, Search, Container, Button, Checkbox } from 'semantic-ui-react'
import './Login.css';
import { useHistory } from 'react-router-dom';
import '../.././../../index'

function ResetPassword() {
    const history = useHistory();
    const [accountState, setAccountState] = useState(history.location.state.accountId);
    const [passwordState, setPasswordState] = useState("");
    //history.location.state.accountId
    const [passwordShown, showPassword] = useState(true);

    function showPasswordHandler(){
        showPassword(!passwordShown);
    };

    
    const userstyle = {
        position: 'absolute',
        margin: '10px'

      };
      
    function submitHandler(e){
        var ResetPasswordModel = e;
        // e.preventDefault();
        if(e.password != "" && e.password.length <= 50 && e.password.length >= 8 && /\d/.test(e.password) &&
        /[A-Z]/.test(e.password) && /[a-z]/.test(e.password)){
            fetch(global.url + 'Login/ResetPassword',
            {
            method: "POST",
            headers: {'Content-type':'application/json',
            'Scope': 'id'},
            body: JSON.stringify(ResetPasswordModel)
            }).
            then(r => r.json()).then(res=>{
                if(res.success){
                    alert("Password Changed Successfully! Return to Login Page!");
                    history.push("/");

                }
                else{
                    alert(res.errorMessage);
                }
            }
            );
        }
        else if (e.password == ""){
            alert("Input is empty!");
        }
        else if (e.password.length > 50){
            alert("Input is too long!")
        }
        else{
            alert("The password is invalid!");
        }
    }

    return (
        <Grid container>
        <Grid.Row>
            <h1>Reset Password: </h1>
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
                            <input type={passwordShown ? "password" : "text"} name="password" placeholder="Password" onChange={e => setPasswordState(e.target.value)}/>
                        </div>
                        <div>
                        <Checkbox  style={userstyle} color="violet" size='mini' onClick={showPasswordHandler} label="Show Password"></Checkbox>
            </div>
        </Grid.Row>
        <Grid.Row>
            <Button
                onClick={()=>submitHandler({
                    password:passwordState,
                    accountId:accountState
                })}
                compact size="tiny"
                circular inverted color="red"
            >
            Submit
            </Button>
            <Button href={global.urlRoute + ""} compact size="tiny" circular inverted color="blue">
                Go Back to Login
            </Button>
        </Grid.Row>
        </Grid>
    )
}

export default ResetPassword;