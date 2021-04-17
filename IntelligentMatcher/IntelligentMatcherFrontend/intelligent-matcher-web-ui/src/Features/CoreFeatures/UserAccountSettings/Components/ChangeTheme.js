
  import React, { Component } from 'react';

  export class ChangeTheme extends Component {
    static displayName = ChangeTheme.name;
  
    constructor(props) {
      super(props);
      this.state = {};
      this.changeTheme = this.changeTheme.bind(this);
      fetch('http://localhost:5000/useraccountsettings/getTheme',
      {
          method: "POST",
          headers: {'Content-type':'application/json'},
          body: JSON.stringify("1")
      }).
      then(r => r.json())
      .then(res=>{

          this.setState({theme:res.theme});
  
        
      }
  
      ); 


    }



  
    changeTheme() {
      var ChangeThemeModel = {id: 1, theme: this.theme.value};
  
      fetch('http://localhost:5000/useraccountsettings/changetheme',
      {
          method: "POST",
          headers: {'Content-type':'application/json'},
          body: JSON.stringify(ChangeThemeModel)
      }).
      then(r => r.json())
      .then(res=>{
        if(res){
          this.setState({message:'Theme has been changed'});
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
    
    
    
    
