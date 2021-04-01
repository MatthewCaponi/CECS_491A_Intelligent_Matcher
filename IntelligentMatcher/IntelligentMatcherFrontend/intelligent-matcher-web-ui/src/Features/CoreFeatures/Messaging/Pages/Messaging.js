import React, { Component, useState } from 'react';
import { Table, Grid } from 'semantic-ui-react'
import { animateScroll } from "react-scroll";
import Picker from 'emoji-picker-react';

export class Messaging extends Component {
  static displayName = Messaging.name;

  constructor(props) {
    super(props);
    this.state = { channel: [], currentUsername: "", channelUsers: [], usersgroups: [], channelId: 0, selectedUser: 0, userId: 1, userRemoveSelect: 0, currentGroupOwner: 0, currentmessagecount: 0, currentEmoji: null};
    this.addUser = this.addUser.bind(this);
    this.removeUser = this.removeUser.bind(this);
    this.createChannel = this.createChannel.bind(this);
    this.deletChannel = this.deletChannel.bind(this);
    this.changeUser = this.changeUser.bind(this);
    this.sendMessage = this.sendMessage.bind(this);
    this.changeChannel = this.changeChannel.bind(this);
    this.scrollToBottom = this.scrollToBottom.bind(this);
    this.leaveChannel = this.leaveChannel.bind(this);

    this.addEmoji = this.addEmoji.bind(this);
    setInterval(async () => {  this.getGroupData()  }, 100);
    this.scrollToBottom();
    this.getGroupData();   
  }
  
  changeUser(){
    this.setState({userId: Number(this.userselect.value)});
    this.getGroupData();
  }

   async changeChannel(){

    await this.getGroupData();
    await this.render();
    this.setState({channelId: Number(this.currentchannelselect.value)});

    await fetch('http://localhost:5000/Messaging/GetChannelOwner',
    {
        method: "POST",
        headers: {'Content-type':'application/json'},
        body: JSON.stringify(Number(this.currentchannelselect.value))
    }).then(r => r.json()) .then(res=>{
        this.setState({currentGroupOwner: res});
    }
    );
    await this.getGroupData();
    
    await this.render();
    this.currentchannelselect.value = String(this.state.channelId);
    await this.scrollToBottom();

}

scrollToBottom() {
    animateScroll.scrollToBottom({
      containerId: "message-table"
    });
}

async deletChannel(){

    await fetch('http://localhost:5000/Messaging/DeleteChannel',
    {
        method: "POST",
        headers: {'Content-type':'application/json'},
        body: JSON.stringify(Number(this.currentchannelselect.value))
    });

    this.setState({channelId: 0});
    this.currentchannelselect.value = "0";
    this.state.currentGroupOwner = 0;
}


async removeMessage(id){
  
    await fetch('http://localhost:5000/Messaging/DeleteMessage',
      {
          method: "POST",
          headers: {'Content-type':'application/json'},
          body: JSON.stringify(Number(id))
        }).then(r => r.json()).then(res=>{
          this.username.value = ""
      }
    );
    await this.getGroupData();
    await this.render();     
  }

async removeUser(id, username){


  




  var removeUserModel = {ChannelId: this.state.channelId,  UserId: id};


  await fetch('http://localhost:5000/Messaging/RemoveUserChannel',
    {
        method: "POST",
        headers: {'Content-type':'application/json'},
        body: JSON.stringify(removeUserModel)
    }).then(r => r.json()).then(res=>{
        var MessageModel = {ChannelId: this.state.channelId, UserId: 0, Message: username + " was removed from the channel by " + this.state.currentUsername};

        fetch('http://localhost:5000/Messaging/SendMessage',
        {
        method: "POST",
        headers: {'Content-type':'application/json'},
        body: JSON.stringify(MessageModel)
        }).
        then(r => r.json()).then(res=>{
        }
        );
        this.username.value = ""
    }
  );
  await this.getGroupData();
  await this.render();     
}

async leaveChannel(){

    var removeUserModel = {ChannelId: this.state.channelId,  UserId: this.state.userId};

    await fetch('http://localhost:5000/Messaging/RemoveUserChannel',
      {
          method: "POST",
          headers: {'Content-type':'application/json'},
          body: JSON.stringify(removeUserModel)
      }).then(r => r.json()).then(res=>{
        var MessageModel = {ChannelId: this.state.channelId, UserId: 0, Message: this.state.currentUsername + " left the channel"};

        fetch('http://localhost:5000/Messaging/SendMessage',
        {
        method: "POST",
        headers: {'Content-type':'application/json'},
        body: JSON.stringify(MessageModel)
        }).
        then(r => r.json()).then(res=>{
        }
        );
      }
    );





    await this.getGroupData();
    await this.render();     
  }

    async getGroupData(){
      if(this.state.channelId != 0){
        await fetch('http://localhost:5000/Messaging/GetChannelOwner',
          {
            method: "POST",
            headers: {'Content-type':'application/json'},
            body: JSON.stringify(this.state.channelId)
          }).then(r => r.json()).then(res=>{
              this.setState({currentGroupOwner: res});
            }
          );
          await fetch('http://localhost:5000/Messaging/GetAllUsersInGroup',
          {
              method: "POST",
              headers: {'Content-type':'application/json'},
              body: JSON.stringify(this.state.channelId)
          }).then(r => r.json()).then(res=>{
              this.setState({channelUsers: res});
          }   
          ); 
          await fetch('http://localhost:5000/Messaging/GetChannelMessages',
          {
            method: "POST",
            headers: {'Content-type':'application/json'},
            body: JSON.stringify(this.state.channelId)
          }).then(r => r.json()).then(res=>{
              this.setState({channel: res});
          }
          ); 

        }
      if(this.state.currentGroupOwner == -1){
        this.setState({channelId: 0});
        this.currentchannelselect.value = "0";
        this.state.currentGroupOwner = 0;
      }

      await fetch('http://localhost:5000/Messaging/GetUserChannels',
        {
          method: "POST",
          headers: {'Content-type':'application/json'},
          body: JSON.stringify(this.state.userId)
        }).then(r => r.json()).then(res=>{
            this.setState({usersgroups: res});
          }
        ); 


            if(this.state.currentmessagecount != this.state.channel.length){
                await this.scrollToBottom();

            }
          this.state.currentmessagecount = this.state.channel.length;


        
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
    addEmoji(){
        this.message.value = this.message.value + this.state.currentEmoji;
    }

async addUser() {
  await this.getGroupData();
  this.render();
  if(this.username.value != ""){  
    var AddUserModel = {ChannelId: this.state.channelId,  Username: this.username.value};

    await fetch('http://localhost:5000/Messaging/AddUserChannel',
    {
        method: "POST",
        headers: {'Content-type':'application/json'},
        body: JSON.stringify(AddUserModel)
    }).then(r => r.json()).then(res=>{

        var MessageModel = {ChannelId: this.state.channelId, UserId: 0, Message: this.username.value +  " was added by " + this.state.currentUsername};

        fetch('http://localhost:5000/Messaging/SendMessage',
        {
        method: "POST",
        headers: {'Content-type':'application/json'},
        body: JSON.stringify(MessageModel)
        }).
        then(r => r.json()).then(res=>{
        }
        );

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
      await fetch('http://localhost:5000/Messaging/CreateChannel',
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
    await this.render();
      if(this.message.value != ""){
        var MessageModel = {ChannelId: this.state.channelId, UserId: this.state.userId, Message: this.message.value};

        await fetch('http://localhost:5000/Messaging/SendMessage',
        {
        method: "POST",
        headers: {'Content-type':'application/json'},
        body: JSON.stringify(MessageModel)
        }).
        then(r => r.json()).then(res=>{
          this.message.value = ""
        }
        );
        await this.getGroupData();
        await this.render();  
        
        this.scrollToBottom();

      }

      this.scrollToBottom();

  }

  render() {


  const messagetablestyle = {
    display: 'block',
    height: '60vh',
    overflowY: "auto"
  };
  const usertablestyle = {
    display: 'block',
    height: '60vh',
    overflowY: "auto"
  };

  const messagestyle = {

  };

  const datestyle = {
    color: 'grey',
    fontSize: '12px'

  };
  const userstyle = {
    color: 'grey',
    fontSize: '16px',
    fontWeight: '500',
    color: '#DCCDC4'
  };
  const deletestyle = {
    color: 'grey',
    fontSize: '10px',
    fontWeight: '500',
    color: '#DCCDC4',
    cursor: 'pointer'

  };
  const renderUserTable = () => {

    this.state.channelUsers.map(channelUsers =>{
        if(channelUsers.userId == this.state.userId){
            this.state.currentUsername = channelUsers.username;
        }

    }
    )




    if(this.state.userId == this.state.currentGroupOwner){
      return <Table stackable striped style={usertablestyle}>
              <Table.Body>
                <Table.Row>        
                  <Table.Cell>Channel Users</Table.Cell>
                </Table.Row>
                  {this.state.channelUsers.map(channelUsers =>
                <Table.Row>        
                  <Table.Cell>{                     (channelUsers.userId == this.state.currentGroupOwner) ?(
                                channelUsers.username + " (Owner)"  ) : (channelUsers.username)}</Table.Cell>
                  <Table.Cell> <a           onClick={() => {
                      this.removeUser(channelUsers.userId, channelUsers.username);
                      }}  style={{cursor: 'pointer'}}>Remove</a>
                    </Table.Cell>
                </Table.Row>
                      )}
              </Table.Body>
            </Table>;
            }
            
     if(this.state.userId != this.state.currentGroupOwner){
        return <Table stackable striped style={usertablestyle}>
                  <Table.Body>
                    <Table.Row>        
                      <Table.Cell>Channel Users</Table.Cell>
                    </Table.Row>
                  {this.state.channelUsers.map(channelUsers =>
                    <Table.Row>        
                      <Table.Cell>{

                            (channelUsers.userId == this.state.currentGroupOwner) ?(
                                channelUsers.username + " (Owner)"  ) : (channelUsers.username)
                            
                      }</Table.Cell>
                    </Table.Row>
                    )}
                </Table.Body>
              </Table>;
            }

              if(this.state.channelId == 0){
                 return  <Table stackable striped style={usertablestyle}>
                  <Table.Body>
                      <Table.Row>        
                        <Table.Cell>Select A channel to view users</Table.Cell>
                      </Table.Row>
                  </Table.Body>
                </Table>;          
              }
            }


            const renderSendMessage = () => {

                const onEmojiClick = (event, emojiObject) => {
                    this.state.currentEmoji = emojiObject.emoji;
                    this.addEmoji();

                  };
                
              if(this.state.channelId != 0){
                  return (  
                      <div>
                  <div class="ui fluid action input">
                        <input type="text" name="message" placeholder="Message" placeholder="Message..."  ref={(input) => this.message = input}></input>
                        <button class="ui button" onClick={this.sendMessage}>Send Message</button>
                        <span>
                 </span>

                    </div>
                      <Picker onEmojiClick={onEmojiClick}
                        pickerStyle={{ width: '100%' }} 
                        groupVisibility={{
                            flags: false,
                            smileys_people: false,
                            animals_nature: false,
                            food_drink: false,
                            travel_places: false,
                            symbols: false,
                            flags: false,
                            recently_used: false,
                            activities: false,

                          }}/>
                      </div>
                    );
                                    
                  
             
              }
          
            }

            const printMessage = (messageData) => {

                if(messageData.userId == 0){
                    return              <div>  
                <Table.Row>
    
                    <Table.Cell>
                    <span style={datestyle}>{messageData.message}</span> 

                    </Table.Cell>
                </Table.Row></div>
                }
                
                return              <div>  <Table.Row>

                <Table.Cell>

                <div><a href={"profile?id="+ messageData.userId}><span style={userstyle}>{messageData.username}</span>
                            </a> <span style={datestyle}> {messageData.time}   </span></div>
                
                </Table.Cell>



            </Table.Row>
            <Table.Row>

                <Table.Cell>
                <span style={messagestyle}>{messageData.message}</span> 
                <br /> 
                <a onClick={() => {
                this.removeMessage(messageData.id);
                }}  style={{cursor: 'pointer'}}>
                <span style={deletestyle}>{ (messageData.userId == this.state.userId) ?("Delete" ) : ("")}
                </span>
                </a>
                </Table.Cell>
            </Table.Row></div>

            }

            const renderChannelToggle = () => {
                if(this.state.channelId == 0){
          
                    return        <div>

                    <select class="ui search dropdown" ref={(input) => this.currentchannelselect = input} onChange={this.changeChannel}>
                      <option value="none" selected disabled hidden> 
                        Select Channel
                      </option> 
                        {this.state.usersgroups.map(usersgroups =>
                          <option value={usersgroups.id}>{usersgroups.name}</option>
                        )}
                      </select>
  
                      <div class="ui action input">
  
                      <input type="text" name="channelname" placeholder="Message" placeholder="Channel Name..."  ref={(input) => this.channelname = input}></input>
                      </div>
  
                      <button class="ui button" onClick={this.createChannel}>Create Channel</button>
                      </div>
            
                    ;
               
                }
              if(this.state.userId != this.state.currentGroupOwner){
          
                  return        <div>

                  <select class="ui search dropdown" ref={(input) => this.currentchannelselect = input} onChange={this.changeChannel}>
                    <option value="none" selected disabled hidden> 
                      Select Channel
                    </option> 
                      {this.state.usersgroups.map(usersgroups =>
                        <option value={usersgroups.id}>{usersgroups.name}</option>
                      )}
                    </select>
                    <button class="ui button" onClick={this.leaveChannel}>Leave Channel</button>

                    <div class="ui action input">

                    <input type="text" name="channelname" placeholder="Message" placeholder="Channel Name..."  ref={(input) => this.channelname = input}></input>
                    </div>

                    <button class="ui button" onClick={this.createChannel}>Create Channel</button>
                    </div>
          
                  ;
             
              }

              
              if(this.state.userId == this.state.currentGroupOwner){
          
                return        <div >

                <select  class="ui  search dropdown" ref={(input) => this.currentchannelselect = input} onChange={this.changeChannel}>
                  <option value="none" selected disabled hidden> 
                    Select Channel
                  </option> 
                    {this.state.usersgroups.map(usersgroups =>
                      <option value={usersgroups.id}>{usersgroups.name}</option>
                    )}
                  </select>
                  <button class="ui button" onClick={this.deletChannel}>Delete Channel</button>
         
                  <div class="ui action input">

                    <input type="text" name="channelname" placeholder="Message" placeholder="Channel Name..."  ref={(input) => this.channelname = input}></input>
                    </div>                  
                    <button class="ui button" onClick={this.createChannel}>Create Channel</button>
                  </div>
        
                ;
           
            }

          
            }


    return(





        

  <div>
    <input type="text" name="channelname"  ref={(input) => this.userselect = input} onChange={this.changeUser}/>

    <Grid columns={2} divided width={10}  container centered>

        <Grid.Row>

            <Grid.Column  width={10}>
            {renderChannelToggle()}

            <Table stackable size="large" style={userstyle} color="black" style={messagetablestyle} id="message-table" >
                <Table.Body>
                        {this.state.channel.map(messageData =>
   
                  
                            <Table.Row>
           
                            {printMessage(messageData)}

                            </Table.Row>

                        )}
                </Table.Body>
            </Table>
            {renderSendMessage()}

            </Grid.Column>
            <Grid.Column  width={3} height={5}>
                {renderUserTable()}
                <div class="ui input">
                    <input type="text"     name="username" placeholder="Username" ref={(input) => this.username = input}/>
                </div>

                 <br />    

                 <div class="ui fluid animated button" tabindex="0" onClick={this.addUser}>
                    <div class="visible  content">Add User</div>
                    <div class="hidden content">
                        <i class="right arrow icon"></i>
                    </div>
                </div>  
            </Grid.Column>

        </Grid.Row>

    </Grid>


</div>

    );
  }
}

export default Messaging;