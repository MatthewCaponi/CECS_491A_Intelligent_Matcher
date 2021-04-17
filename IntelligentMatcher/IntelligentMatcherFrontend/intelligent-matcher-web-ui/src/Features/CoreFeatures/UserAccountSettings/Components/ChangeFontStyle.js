
  import React, { Component } from 'react';
  import '../.././../../App'

  export class ChangeFontStyle extends Component {
    static displayName = ChangeFontStyle.name;
  
    constructor(props) {
      super(props);
      this.state = {};
      this.changeFontStyle = this.changeFontStyle.bind(this);
      fetch(global.url + 'useraccountsettings/getFontStyle',
      {
          method: "POST",
          headers: {'Content-type':'application/json'},
          body: JSON.stringify("1")
      }).
      then(r => r.json())
      .then(res=>{

          this.setState({fontstyle:res.fontStyle});
  
        
      }
  
      ); 


    }



  
    changeFontStyle() {
      var ChangeFontStyleModel = {id: 1, fontStyle: this.fontStyle.value};
  
      fetch('useraccountsettings/changefontstyle',
      {
          method: "POST",
          headers: {'Content-type':'application/json'},
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
            <option value="Time-New Roman">Time-New Roman</option>
            <option value="Serif">Serif</option>
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
    
    
    
    
