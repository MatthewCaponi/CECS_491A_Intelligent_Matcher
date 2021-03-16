import React, { Component } from 'react';

export class ChangeFontSize extends Component {
  static displayName = ChangeFontSize.name;

  constructor(props) {
    super(props);
    this.state = { fontsize: 0 };
    this.changeFontSize = this.changeFontSize.bind(this);
    fetch('useraccountsettings/getFontSize',
    {
        method: "POST",
        headers: {'Content-type':'application/json'},
        body: JSON.stringify("2")
    }).
    then(r => r.json())
    .then(res=>{

        this.setState({fontsize:res});

      
    }

    );  }



  changeFontSize() {
    var ChangeFontSizeModel = {id: 2, fontSize: this.fontSize.value};

    fetch('useraccountsettings/changefontsize',
    {
        method: "POST",
        headers: {'Content-type':'application/json'},
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
        <input type="text" name="Password" placeholder={this.state.fontsize} ref={(input) => this.fontSize = input}/>
        <br />
        <br />
        <button className="btn btn-primary" onClick={this.changeFontSize}>Change Font Size</button>

        <p>{this.state.message}</p>
      </div>
    );
  }
}
