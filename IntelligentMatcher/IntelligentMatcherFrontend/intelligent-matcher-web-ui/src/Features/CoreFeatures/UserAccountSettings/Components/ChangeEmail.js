import React, { Component } from 'react';
import { Table, Grid } from 'semantic-ui-react'
import '../.././../../index'
import jwt from 'jwt-decode';
import Cookies from 'js-cookie';
export class ChangeEmail extends Component {
  static displayName = ChangeEmail.name;

  constructor(props) {
    super(props);
    this.state = { currentCount: 0 };
    this.changeEmail = this.changeEmail.bind(this);
  }

  changeEmail() {
    const idToken = Cookies.get('IdToken');
    const decodedIdToken = jwt(idToken);
    const userId = decodedIdToken.id;
    var ChangeEmailModel = {id: parseInt(userId), oldPassword: this.email.value, password: this.password.value};

    fetch(global.url + 'UserAccountSettings/ChangeEmail',
    {
        method: "POST",
        headers: {'Content-type':'application/json',
        'Authorization': 'Bearer ' + Cookies.get('AccessToken')},
        body: JSON.stringify(ChangeEmailModel)
    }).
    then(r => r.json())
    .then(res=>{
      if(res){
        this.setState({message:'Email has been changed'});
      }else{
        this.setState({message:'Email change has failed'});

      }
    }

    );
  }

  render() {
    return (
      <div>
        <h1>Change Your Email</h1>
        <p>Enter you email:</p>
        <div class="ui action input">

        <input type="text" name="Password" ref={(input) => this.email = input}/>
        </div>
        <p>Enter you password:</p>
        <div class="ui action input">

        <input type="password" name="Password" ref={(input) => this.password = input}/>
        </div>

        <br />
        <br />
        <button class="ui button" onClick={this.changeEmail}>Change Email</button>
        <p>{this.state.message}</p>
      </div>
    );
  }
}
