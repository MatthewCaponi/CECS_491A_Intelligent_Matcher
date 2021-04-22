import React, { Component } from 'react';
import '../.././../../App'


export class Confirm extends Component {
    static displayName = Confirm.name;
  
    constructor(props) {
  
      super(props);
  
      this.state = {  
          userId: 0,
          token: "",
          confirmStatus: "",
          message: "Account is being confirmed"
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
        }else{
            this.setState({message: "Account not confirmed"});

        }
    }

    
  render () {



    return (

      <div>{this.state.message}</div>
    );
  }
}
export default Confirm;