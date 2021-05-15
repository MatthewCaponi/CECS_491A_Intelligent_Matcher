
  import React, { Component, useImperativeHandle } from 'react';
  import '../.././../../index'
  import jwt from 'jwt-decode';
  import Cookies from 'js-cookie';
  export class ChangeFontStyle extends Component {
    static displayName = ChangeFontStyle.name;
  
    constructor(props) {
      super(props);
      this.state = {};
      this.changeFontStyle = this.changeFontStyle.bind(this);
      const idToken = Cookies.get('IdToken');
      const decodedIdToken = jwt(idToken);
      const userId = decodedIdToken.id;
      fetch(global.url + 'UserAccountSettings/GetFontStyle',
      {
          method: "POST",
          headers: {'Content-type':'application/json',
          'Authorization': 'Bearer ' + Cookies.get('AccessToken')},
          body: JSON.stringify(userId)
      }).
      then(r => r.json())
      .then(res=>{

          this.setState({fontstyle:res.fontStyle});
  
        
      }
  
      ); 


    }



  
    changeFontStyle() {
      const idToken = Cookies.get('IdToken');
      const decodedIdToken = jwt(idToken);
      const userId = decodedIdToken.id;
      var ChangeFontStyleModel = {id: parseInt(userId), fontStyle: this.fontStyle.value};

      fetch(global.url + 'UserAccountSettings/ChangeFontStyle',
      {
          method: "POST",
          headers: {'Content-type':'application/json',
          'Authorization': 'Bearer ' + Cookies.get('AccessToken')},
          body: JSON.stringify(ChangeFontStyleModel)
      }).
      then(r => r.json())
      .then(res=>{
        if(res){
          this.setState({message:'Font Style has been changed'});
        }else{
          this.setState({message:'Font Style change has failed'});
  
        }
      }
  
      );
    }
  
    render() {
  
      return (
        <div>
          <h1>Change Font Style</h1>
          <p>Font Font Style:</p>
          <select class="ui search dropdown" ref={(input) => this.fontStyle = input}>
          <option value="none" selected disabled hidden> 
          {this.state.fontstyle}
                </option> 
                <option value="Default">Default</option>

            <option value="Time-New Roman">Time-New Roman</option>
            <option value="Oxygen">Oxygen</option>
            <option value="Helvetica">Helvetica</option>
        </select>

          <br />
          <br />
          <button class="ui button" onClick={this.changeFontStyle}>Change Font Style</button>
          <p>{this.state.message}</p>
        </div>
      );
    }
  }
    
    
    
    
