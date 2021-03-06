
  import React, { Component } from 'react';
  import '../.././../../index'
  import jwt from 'jwt-decode';
  import Cookies from 'js-cookie';

  export class ChangeTheme extends Component {
    static displayName = ChangeTheme.name;
  
    constructor(props) {
      super(props);
      this.state = {};
      this.changeTheme = this.changeTheme.bind(this);
      const idToken = Cookies.get('IdToken');
      const decodedIdToken = jwt(idToken);
      const userId = decodedIdToken.id;
      fetch(global.url + 'UserAccountSettings/GetTheme',
      {
          method: "POST",
          headers: {'Content-type':'application/json',
          'Authorization': 'Bearer ' + Cookies.get('AccessToken')},
          body: JSON.stringify(userId)
      }).
      then(r => r.json())
      .then(res=>{

          this.setState({theme:res.theme});
  
        
      }
  
      ); 


    }



  
    changeTheme() {
      const idToken = Cookies.get('IdToken');
      const decodedIdToken = jwt(idToken);
      const userId = decodedIdToken.id;


      var ChangeThemeModel = {id: parseInt(userId), theme: this.theme.value};
  
      fetch(global.url + 'UserAccountSettings/ChangeTheme',
      {
          method: "POST",
          headers: {'Content-type':'application/json',
          'Authorization': 'Bearer ' + Cookies.get('AccessToken')},
          body: JSON.stringify(ChangeThemeModel)
      }).
      then(r => r.json())
      .then(res=>{
        if(res){
          this.setState({message:'Theme has been changed'});
          window.location.reload(false);

        }else{
          this.setState({message:'THeme change has failed'});
  
        }
      }
  
      );
    }
  
    render() {
  
      return (
        <div>
          <h1>Change Theme</h1>
          <p>Theme Color:</p>
          <select class="ui search dropdown" ref={(input) => this.theme = input}>
          <option value="none" selected disabled hidden> 
          {this.state.theme}
                </option> 
            <option value="Dark">Dark</option>
            <option value="Light">Light</option>
        </select>

          <br />
          <br />
          <button class="ui button" onClick={this.changeTheme}>Change Theme</button>
          <p>{this.state.message}</p>
        </div>
      );
    }
  }
    
    
    
    
