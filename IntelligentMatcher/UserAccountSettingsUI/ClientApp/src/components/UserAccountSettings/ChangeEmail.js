import React, { Component } from 'react';

export class ChangeEmail extends Component {
  static displayName = ChangeEmail.name;

  constructor(props) {
    super(props);
    this.state = { currentCount: 0 };
    this.changeEmail = this.changeEmail.bind(this);
  }

  changeEmail() {
    var ChangeEmailModel = {id: 1, oldPassword: this.email.value, password: this.password.value};

    fetch('useraccountsettings/changeemail',
    {
        method: "POST",
        headers: {'Content-type':'application/json'},
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
        <input type="text" name="Password" ref={(input) => this.email = input}/>
        <p>Enter you password:</p>
        <input type="password" name="Password" ref={(input) => this.password = input}/>
        <br />
        <br />
        <button className="btn btn-primary" onClick={this.changeEmail}>Change Email</button>
        <p>{this.state.message}</p>
      </div>
    );
  }
}
