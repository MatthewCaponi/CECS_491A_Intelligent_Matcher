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

    this.sendMessage = this.sendMessage.bind(this);
    this.changeChannel = this.changeChannel.bind(this);


    setInterval(async () => {  this.getGroupData()  }, 100);

        

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
    this.getGroupData();
    this.render();     

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



if(this.state.channelId == 0){
    

    return(<div>
      
                  <select ref={(input) => this.currentchannelselect = input} onChange={this.changeChannel}>
        
                  <option value="none" selected disabled hidden> 
                  Select Channel
                        </option> 
                        <button className="btn btn-primary" onClick={this.sendMessage}>Delete Channel</button>

                        {this.state.usersgroups.map(usersgroups =>
                    <option value={usersgroups.id}>{usersgroups.name}</option>
        
          )}
        
        
        
                </select>
                <button className="btn btn-primary" onClick={this.deletChannel}>Delete Channel</button>

                <input type="text" name="channelname" placeholder="Channel Name"  ref={(input) => this.channelname = input}/>
        
        <button className="btn btn-primary" onClick={this.createChannel}>Create Channel</button>
        
        
        <p>Please Select a Channel</p>


              </div>);
}















if(this.state.userId == this.state.currentGroupOwner){





    return(<div>
        <table>
        
        
        <td>
                  <select ref={(input) => this.currentchannelselect = input} onChange={this.changeChannel}>
        
                  <option value="none" selected disabled hidden> 
                  Select Channel
                        </option> 
                        <button className="btn btn-primary" onClick={this.sendMessage}>Delete Channel</button>

                        {this.state.usersgroups.map(usersgroups =>
                    <option value={usersgroups.id}>{usersgroups.name}</option>
        
          )}
        
        
        
                </select>
                <button className="btn btn-primary" onClick={this.deletChannel}>Delete Channel</button>

                <input type="text" name="channelname" placeholder="Channel Name"  ref={(input) => this.channelname = input}/>
        
        <button className="btn btn-primary" onClick={this.createChannel}>Create Channel</button>
        
        
        
        
        <table className='table table-striped' aria-labelledby="tabelLabel">
        
        <tbody>
          {this.state.channel.map(channel =>
            <tr key={channel.id}>
              <td>{channel.message}</td>
              <td>{channel.username}({channel.time})</td>

            </tr>
          )}
        </tbody>
        </table>
        <input type="text" name="message"  ref={(input) => this.message = input}/>
        
        <button className="btn btn-primary" onClick={this.sendMessage}>Send Message</button>
        
        </td>
        <td>
        
        
        
        
                <table className='table table-striped' aria-labelledby="tabelLabel">
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
        
        <input type="text" name="username"  ref={(input) => this.username = input}/>
        
        <button className="btn btn-primary" onClick={this.addUser}>Add User</button>
        </td>
        </table>
              </div>);
}else{

    return(<div>
        <table>
        
        
        <td>
                  <select ref={(input) => this.currentchannelselect = input} onChange={this.changeChannel}>
        
                  <option value="none" selected disabled hidden> 
                  Select Channel
                        </option> 
        
                        {this.state.usersgroups.map(usersgroups =>
                    <option value={usersgroups.id}>{usersgroups.name}</option>
        
          )}
        
        
        
                </select>
        
                <input type="text" name="channelname" placeholder="Channel Name"  ref={(input) => this.channelname = input}/>
        
        <button className="btn btn-primary" onClick={this.createChannel}>Create Channel</button>
        
        
        
        
        <table className='table table-striped' aria-labelledby="tabelLabel">
        
        <tbody>
          {this.state.channel.map(channel =>
            <tr key={channel.id}>
              <td>{channel.message}</td>
              <td>{channel.username}({channel.time})</td>
            </tr>
          )}
        </tbody>
        </table>
        <input type="text" name="message"  ref={(input) => this.message = input}/>
        
        <button className="btn btn-primary" onClick={this.sendMessage}>Send Message</button>
        
        </td>
        <td>
        
        
        
        
                <table className='table table-striped' aria-labelledby="tabelLabel">
        <tbody>
          {this.state.channelUsers.map(channelUsers =>
            <tr>     
            
             <td>{channelUsers.username}</td>
  
            </tr>
          )}
        </tbody>
        </table>
        
        
        </td>
        </table>
              </div>);
    
}













  }
}
