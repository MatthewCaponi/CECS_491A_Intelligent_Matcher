import React, { Component } from 'react';

export class MessageBox extends Component {
  static displayName = MessageBox.name;

  constructor(props) {
    super(props);
    this.state = { fontsize: 0 };
    this.changeFontSize = this.changeFontSize.bind(this);
  }



  sendMessage() {
    var MessageModel = {ChannelId: 1, UserId: 1, Message: this.message.value};

    fetch('messaging/sendmessage',
    {
        method: "POST",
        headers: {'Content-type':'application/json'},
        body: JSON.stringify(MessageModel)
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
        <input type="text" name="message"  ref={(input) => this.message = input}/>

        <button className="btn btn-primary" onClick={this.sendMessage}>Send Message</button>

        <p>{this.state.message}</p>
      </div>
    );
  }
}
