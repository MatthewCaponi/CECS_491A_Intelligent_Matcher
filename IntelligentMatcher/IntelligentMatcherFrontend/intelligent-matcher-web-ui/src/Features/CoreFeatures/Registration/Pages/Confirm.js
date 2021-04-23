import React, { Component } from 'react';
import '../.././../../App'
import { Grid, Header, Divider, Label, Search, Container, Button } from 'semantic-ui-react'
import { Redirect } from 'react-router'

import './Registration.css';

export class Confirm extends Component {
    static displayName = Confirm.name;
  
    constructor(props) {
  
      super(props);
  
      this.state = {  
          userId: 0,
          token: "",
          confirmStatus: "",
          message: "Account is being confirmed",
          navigate: false
          };
          let url = window.location.href;
          url = url.split("id=")
          let datas = url[1].split("?key=");
          this.state.userId = datas[0];
          this.state.token = datas[1];


          this.verifyAccount = this.verifyAccount.bind(this);

          this.verifyAccount();

    }


    async verifyAccount(){

        var tokenIdModel = {UserId: Number(this.state.userId),  Token:  this.state.token};


        await fetch(global.url + 'Registration/ConfirmUser',
        {
            method: "POST",
            headers: {'Content-type':'application/json'},
            body: JSON.stringify(tokenIdModel)
        }).then(r => r.json()) .then(res=>{
            this.setState({confirmStatus: res});
        }
        );
    
        if(this.state.confirmStatus == true){
            this.setState({message: "Account Confirmed, redirecting to login"});
            this.setState({ navigate: true });

        }else{
            this.setState({message: "Account not confirmed. Link is expired or invalid"});
        
   
       
        }
    }

    
  render () {

    if (this.state.navigate) {




      return <Redirect to={{ pathname:"/Login", state: { message: "UserConfirmed"}}}      push={true} />
    }

    return (
      <Grid container>

      <Grid.Row>
      <h1>{this.state.message}</h1>
  </Grid.Row>    
  </Grid>
  
  );
  }
}
export default Confirm;