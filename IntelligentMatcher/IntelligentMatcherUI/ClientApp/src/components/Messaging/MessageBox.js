import { Alert } from 'bootstrap';
import React, { Component } from 'react';


export class MessageBox extends Component {
  static displayName = MessageBox.name;

  constructor(props) {
    super(props);
    this.state = { channel: [], channelUsers: [], usersgroups: [], channelId: 0, selectedUser: 0, userId: 1, userRemoveSelect: 0, currentGroupOwner: 0};

    this.addUser = this.addUser.bind(this);
    this.removeUser = this.removeUser.bind(this);
    this.createChannel = this.createChannel.bind(this);
    this.deletChannel = this.deletChannel.bind(this);
    this.changeUser = this.changeUser.bind(this);

    this.sendMessage = this.sendMessage.bind(this);
    this.changeChannel = this.changeChannel.bind(this);


    setInterval(async () => {  this.getGroupData()  }, 100);

        

    this.getGroupData();

    
  }
  
  changeUser(){
    this.setState({userId: Number(this.userselect.value)});
    this.getGroupData();

  }

   async changeChannel(){
    await this.getGroupData();
    this.render();
    this.setState({channelId: Number(this.currentchannelselect.value)});


    await fetch('messaging/getchannelowner',
    {
        method: "POST",
        headers: {'Content-type':'application/json'},
        body: JSON.stringify(Number(this.currentchannelselect.value))
    }).
    then(r => r.json())
    .then(res=>{
        this.setState({currentGroupOwner: res});
    }

    );



    await this.getGroupData();
    this.render();

    this.currentchannelselect.value = String(this.state.channelId);


}



async deletChannel(){

    await fetch('messaging/deletechannel',
    {
        method: "POST",
        headers: {'Content-type':'application/json'},
        body: JSON.stringify(Number(this.currentchannelselect.value))
    }).
    then(r => r.json())
    .then(res=>{
    }

    );
    this.setState({channelId: 0});

    this.currentchannelselect.value = "0";
    this.state.currentGroupOwner = 0;
  
    window.location.reload();

}




async removeUser(id){
    var removeUserModel = {ChannelId: this.state.channelId,  UserId: id};


    await fetch('messaging/removeuserchannel',
    {
        method: "POST",
        headers: {'Content-type':'application/json'},
        body: JSON.stringify(removeUserModel)
    }).
    then(r => r.json())
    .then(res=>{
        this.username.value = ""
    }

    );
    this.getGroupData();
    this.render();     


}


    async getGroupData(){


        if(this.state.channelId != 0){
            await fetch('messaging/getchannelowner',
            {
                method: "POST",
                headers: {'Content-type':'application/json'},
                body: JSON.stringify(this.state.channelId)
            }).
            then(r => r.json())
            .then(res=>{
                this.setState({currentGroupOwner: res});
            }
        
            );
        }
        if(this.state.currentGroupOwner == -1){
          this.setState({channelId: 0});

          this.currentchannelselect.value = "0";
          this.state.currentGroupOwner = 0;
          window.location.reload();
    
        }

        

        await fetch('messaging/getuserchannels',
        {
            method: "POST",
            headers: {'Content-type':'application/json'},
            body: JSON.stringify(this.state.userId)
        }).
        then(r => r.json())
        .then(res=>{
            this.setState({usersgroups: res});

        }
    
        ); 



        await fetch('messaging/getmessages',
        {
            method: "POST",
            headers: {'Content-type':'application/json'},
            body: JSON.stringify(this.state.channelId)
        }).
        then(r => r.json())
        .then(res=>{
            this.setState({channel: res});
    
        }
    
        ); 

        await fetch('messaging/getchannelusers',
        {
            method: "POST",
            headers: {'Content-type':'application/json'},
            body: JSON.stringify(this.state.channelId)
        }).
        then(r => r.json())
        .then(res=>{
            this.setState({channelUsers: res});
    
        }
    
        ); 
        if(this.state.channelId != 0){
          var isRemoved = false;
          this.state.channelUsers.map((channelUsers) =>{  
              if(channelUsers.userId == this.state.userId){
                isRemoved = true;
              }
          });
  
          if(isRemoved == false){
            this.setState({channelId: 0});

            this.currentchannelselect.value = "0";
            this.state.currentGroupOwner = 0;
      
  
          }
        }
   
    }

    async addUser() {
        await this.getGroupData();
        this.render();
          if(this.username.value != ""){
    
        
        var AddUserModel = {ChannelId: this.state.channelId,  Username: this.username.value};
    
        fetch('messaging/addusertogroup',
        {
            method: "POST",
            headers: {'Content-type':'application/json'},
            body: JSON.stringify(AddUserModel)
        }).
        then(r => r.json())
        .then(res=>{
            this.username.value = ""
        }
    
        );
    
            this.getGroupData();
            this.render();     
  
      
          }
      }

      async createChannel() {
        await this.getGroupData();
        this.render();
          if(this.channelname.value != ""){
    
        
        var ChannelModel = {OwnerId: this.state.userId, Name: this.channelname.value};
    
        await fetch('messaging/createchannel',
        {
            method: "POST",
            headers: {'Content-type':'application/json'},
            body: JSON.stringify(ChannelModel)
        }).
        then(r => r.json())
        .then(res=>{
            this.channelname.value = ""
        }
    
        );
    
            this.getGroupData();
            this.render();     

      
          }
      }
    



  async sendMessage() {
    await this.getGroupData();
    this.render();
      if(this.message.value != ""){

    
    var MessageModel = {ChannelId: this.state.channelId, UserId: this.state.userId, Message: this.message.value};

    await fetch('messaging/sendmessage',
    {
        method: "POST",
        headers: {'Content-type':'application/json'},
        body: JSON.stringify(MessageModel)
    }).
    then(r => r.json())
    .then(res=>{
        this.message.value = ""
    }

    );

        this.getGroupData();
        this.render();     
 
  
      }
  }

  render() {
    //setInterval(this.render, 500);
    const container = {
      backgroundColor: '#202225'

    };
    const chatchild = {
      backgroundColor: '#202225',

      width: '70%',
      float: 'left',
  };
  const sidebarcild = {
    float: 'left',
  };
  const channelsettings = {
    backgroundColor: '#18191C',

    width: '100%',
    height: '5vh'
  };
  const messages = {
    backgroundColor: '#36393F',

    width: '100%',
    height: '75vh'
  };
  const messageinput = {
    backgroundColor: '#202225',

    width: '100%',
    height: '5vh'
  };
  const users = {
    backgroundColor: '#2F3136',

    width: '100%',
    height: '75vh'
  };
  const adduser = {
    backgroundColor: '#2F3136',

    width: '100%',
    height: '10vh'
  };

  const messagetablestyle = {
    color: "white",
    display: 'block',
    height: '75vh',
    overflowY: "auto"


  };
  const usertablestyle = {
    color: "white",
    display: 'block',
    height: '75vh',
    overflowY: "auto"


  };
    return(

  <div>
    <input type="text" name="channelname"  ref={(input) => this.userselect = input} onChange={this.changeUser}/>
  <div style={container}>

    <div style={chatchild}>
      <div style={channelsettings}>
      <form class="form-inline justify-content-center">

        <select className=" w-25" ref={(input) => this.currentchannelselect = input} onChange={this.changeChannel}>
          <option value="none" selected disabled hidden> 
            Select Channel
          </option> 
          <button className=" btn btn-dark  w-25" onClick={this.sendMessage}>Delete Channel</button>
            {this.state.usersgroups.map(usersgroups =>
              <option value={usersgroups.id}>{usersgroups.name}</option>
            )}
          </select>
          <button className=" btn btn-dark w-25" onClick={this.deletChannel}>Delete Channel</button>
          <input type="text" name="channelname" className=" w-25" placeholder="Channel Name"  ref={(input) => this.channelname = input}/>
          <button className=" btn btn-dark  w-25" onClick={this.createChannel}>Create Channel</button>
          </form>

        </div>
      <div style={messages}>
        <table className="messages" style={messagetablestyle}  > 
          <tbody>
            {this.state.channel.map(messageData =>
            <tr>
              <tr>
                <td>{messageData.username}({messageData.time}):</td>
              </tr>
              <tr>
                <td>{messageData.message}</td>
              </tr>
            </tr>
            )}
          </tbody>
        </table>
      </div>
      <div style={messageinput}>
        <input type="text" name="message" className="input-dark w-75 h-100" ref={(input) => this.message = input}/>      
        <button className="btn btn-dark w-25 h-100"  onClick={this.sendMessage}>Send Message</button>
      </div>

    </div>
    <div style={sidebarcild}>
    <div style={users}>
      <table className='table table-striped' aria-labelledby="tabelLabel" style={usertablestyle}>
        <tbody>
          {this.state.channelUsers.map(channelUsers =>
            <tr>        
              <td>{channelUsers.username}</td>
              <td> <a           onClick={() => {
                this.removeUser(channelUsers.userId);
                  }}  style={{cursor: 'pointer'}}>Remove</a>
              </td>
            </tr>
            )}
        </tbody>
      </table>
    </div>
    <div style={adduser}>        
      <input type="text" className="w-100 h-50" name="username"  ref={(input) => this.username = input}/>
      <br />      
      <button className="btn btn-dark w-100 h-50" onClick={this.addUser}>Add User</button>
    </div>

    </div>

  </div>

</div>

    );
  }
}
