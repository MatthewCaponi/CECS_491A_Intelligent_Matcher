import React, { Component } from 'react';
import '../.././../../index'
import jwt from 'jwt-decode';
import Cookies from 'js-cookie';
export class DeleteAccount extends Component {
  static displayName = DeleteAccount.name;

  constructor(props) {
    super(props);
    this.state = { currentCount: 0 };
    this.deleteAccount = this.deleteAccount.bind(this);


  }

  deleteAccount() {
    const idToken = Cookies.get('IdToken');
    const decodedIdToken = jwt(idToken);
    const userId = decodedIdToken.id;
    var DeleteModel = {id: parseInt(userId), password: this.textInput.value};

    fetch(global.url + 'UserAccountSettings/DeleteAccount',
    {
        method: "POST",
        headers: {'Content-type':'application/json',
        'Authorization': 'Bearer ' + Cookies.get('AccessToken')},
        body: JSON.stringify(DeleteModel)
    }).
    then(r => r.json())
    .then(res=>{
      if(res){
        this.setState({message:'Account has been deleted'});
      }else{
        this.setState({message:'Account deletion failed'});

      }
    }

    );
  }

  render() {
    return (
      <div>
        <h1>Delete Your Account</h1>
        <p>Enter you password:</p>
        <div class="ui action input">

        <input type="password" name="Password" ref={(input) => this.textInput = input}/>
        </div>
        <br />
        <br />
        <button class="ui button" onClick={this.deleteAccount}>Delete Account</button>
        <p>{this.state.message}</p>
      </div>
    );
  }
}
