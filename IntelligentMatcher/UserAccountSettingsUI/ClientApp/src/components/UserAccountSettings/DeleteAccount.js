import React, { Component } from 'react';

export class DeleteAccount extends Component {
  static displayName = DeleteAccount.name;

  constructor(props) {
    super(props);
    this.state = { currentCount: 0 };
    this.deleteAccount = this.deleteAccount.bind(this);


  }

  deleteAccount() {
    var DeleteModel = {id: 1, password: this.textInput.value};

    fetch('useraccountsettings/delete',
    {
        method: "POST",
        headers: {'Content-type':'application/json'},
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
        <input type="password" name="Password" ref={(input) => this.textInput = input}/>
        <br />
        <br />
        <button className="btn btn-primary" onClick={this.deleteAccount}>Delete Account</button>
        <p>{this.state.message}</p>
      </div>
    );
  }
}
