import React, { Component } from 'react';
import '../.././../../index'
import jwt from 'jwt-decode';
import Cookies from 'js-cookie';
export class ChangeFontSize extends Component {
  static displayName = ChangeFontSize.name;

  constructor(props) {
    super(props);
    this.state = { fontsize: 0 };
    this.changeFontSize = this.changeFontSize.bind(this);
    const idToken = Cookies.get('IdToken');
    const decodedIdToken = jwt(idToken);
    const userId = decodedIdToken.id;
    fetch(global.url + 'UserAccountSettings/GetFontSize',
    {
        method: "POST",
        headers: {'Content-type':'application/json',
        'Authorization': 'Bearer ' + Cookies.get('AccessToken')},
        body: JSON.stringify(userId)
    }).
    then(r => r.json())
    .then(res=>{

        this.setState({fontsize:res});

      
    }

    );  }



  changeFontSize() {
    const idToken = Cookies.get('IdToken');
    const decodedIdToken = jwt(idToken);
    const userId = decodedIdToken.id;
    var ChangeFontSizeModel = {id: parseInt(userId), fontSize: this.fontSize.value};

    fetch(global.url + 'UserAccountSettings/ChangeFontSize',
    {
        method: "POST",
        headers: {'Content-type':'application/json',
        'Authorization': 'Bearer ' + Cookies.get('AccessToken')},
        body: JSON.stringify(ChangeFontSizeModel)
    }).
    then(r => r.json())
    .then(res=>{
      if(res){
        this.setState({message:'Font Size has been changed'});
      }else{
        this.setState({message:'Font Size change has failed'});

      }
    }

    );

  }

  render() {
    return (
      <div>
        <h1>Change Font Size</h1>
        <p>Font Size:</p>        
        <div class="ui action input">

        <input type="text" name="Password" placeholder={this.state.fontsize} ref={(input) => this.fontSize = input}/>
        </div>
        <br />
        <br />
        <button class="ui button" onClick={this.changeFontSize}>Change Font Size</button>

        <p>{this.state.message}</p>
      </div>
    );
  }
}
