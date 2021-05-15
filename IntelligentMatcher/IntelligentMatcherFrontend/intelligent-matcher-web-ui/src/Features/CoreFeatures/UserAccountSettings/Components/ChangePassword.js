import React, { Component } from 'react';
import '../.././../../index'
import jwt from 'jwt-decode';
import Cookies from 'js-cookie';
export class ChangePassword extends Component {
  static displayName = ChangePassword.name;

  constructor(props) {
    super(props);
    this.state = { currentCount: 0 };
    this.changePassword = this.changePassword.bind(this);
  }

  changePassword() {
    const idToken = Cookies.get('IdToken');
    const decodedIdToken = jwt(idToken);
    const userId = decodedIdToken.id;
    var ChangePasswordModel = {id: parseInt(userId), oldPassword: this.oldPassword.value, newPassword: this.newPassword.value};

    fetch(global.url + 'UserAccountSettings/PasswordChange',
    {
        method: "POST",
        headers: {'Content-type':'application/json',
        'Authorization': 'Bearer ' + Cookies.get('AccessToken')},
        body: JSON.stringify(ChangePasswordModel)
    }).
    then(r => r.json())
    .then(res=>{
      if(res){
        this.setState({message:'Password has been changed'});
      }else{
        this.setState({message:'Password change has failed'});

      }
    }

    );
  }

  render() {
    return (
      <div>
        <h1>Change Your Password</h1>
        <p>Enter you current password:</p>
        <div class="ui action input">

        <input type="password" name="Password" ref={(input) => this.oldPassword = input}/>
        </div>
        <p>Enter you new password:</p>
        <div class="ui action input">

        <input type="password" name="Password" ref={(input) => this.newPassword = input}/>
        </div>
        <br />
        <br />
        <button class="ui button" onClick={this.changePassword}>Change Passowrd</button>
        <p>{this.state.message}</p>
      </div>
    );
  }
}
