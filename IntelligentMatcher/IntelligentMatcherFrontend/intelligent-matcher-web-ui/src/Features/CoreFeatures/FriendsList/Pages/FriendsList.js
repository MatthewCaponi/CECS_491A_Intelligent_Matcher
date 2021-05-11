import React, { Component } from 'react';
import { Table, Grid } from 'semantic-ui-react'
import ReactDataGrid from 'react-data-grid';
import { Image } from 'semantic-ui-react'
import '../.././../../App'
import Cookies from 'js-cookie';
import _ from 'lodash'

export class FriendsList extends Component {
  static displayName = FriendsList.name;
  constructor(props) {

    super(props);

    this.state = {  friends: [],    
                    requests: [], 
                    outgoing: [], 
                    blocks: [],
                    blocking: [],

                    userId: 1,
                    friendRequestMessage: ''
           
                  };


               

      this.sort = this.sort.bind(this)

      this.getFriendData = this.getFriendData.bind(this);
      this.removeFriend = this.removeFriend.bind(this);
      this.createFriendRequest = this.createFriendRequest.bind(this);
      this.search = this.search.bind(this);

      this.getFriendData();

  }

  async getFriendData(){


  await fetch(global.url + 'FriendList/GetAllFriends',
  {
      method: "POST",
      headers: {'Content-type':'application/json'},
      body: JSON.stringify(this.state.userId)
  }).then(r => r.json()).then(res=>{
      this.setState({friends: res});
  }   
  ); 

  await fetch(global.url + 'FriendList/GetAllRequets',
  {
      method: "POST",
      headers: {'Content-type':'application/json',
      'Authorization': 'Bearer ' + Cookies.get('AccessToken')},
      body: JSON.stringify(this.state.userId)
  }).then(r => r.json()).then(res=>{
      this.setState({requests: res});
  }   
  ); 

  await fetch(global.url + 'FriendList/GetAllRequetsOutgoing',
  {
      method: "POST",
      headers: {'Content-type':'application/json',
      'Authorization': 'Bearer ' + Cookies.get('AccessToken')},
      body: JSON.stringify(this.state.userId)
  }).then(r => r.json()).then(res=>{
      this.setState({outgoing: res});
  }   
  ); 



  await fetch(global.url + 'FriendList/GetAllBlocks',
  {
      method: "POST",
      headers: {'Content-type':'application/json',
      'Authorization': 'Bearer ' + Cookies.get('AccessToken')},
      body: JSON.stringify(this.state.userId)
  }).then(r => r.json()).then(res=>{
      this.setState({blocks: res});
  }   
  ); 


  await fetch(global.url + 'FriendList/GetAllBlocking',
  {
      method: "POST",
      headers: {'Content-type':'application/json',
      'Authorization': 'Bearer ' + Cookies.get('AccessToken')},
      body: JSON.stringify(this.state.userId)
  }).then(r => r.json()).then(res=>{
      this.setState({blocking: res});
  }   
  ); 

}


async approveFriend(friendId){
  this.render();

  var IdsModel = {UserId: this.state.userId, FriendId: friendId};

  await fetch(global.url + 'FriendList/ApproveFriend',
  {
  method: "POST",
  headers: {'Content-type':'application/json',
  'Authorization': 'Bearer ' + Cookies.get('AccessToken')},
  body: JSON.stringify(IdsModel)
  }).
  then(r => r.json()).then(res=>{

  }
  );
  this.getFriendData();
  this.render();

}

async removeFriend(friendId){
  this.render();

  var IdsModel = {UserId: this.state.userId, FriendId: friendId};

  await fetch(global.url + 'FriendList/RemoveFriend',
  {
  method: "POST",
  headers: {'Content-type':'application/json',
  'Authorization': 'Bearer ' + Cookies.get('AccessToken')},
  body: JSON.stringify(IdsModel)
  }).
  then(r => r.json()).then(res=>{

  }
  );
  this.getFriendData();
  this.render();

}

async cancelFriendRequest(friendId){
  this.render();

  var IdsModel = {UserId: this.state.userId, FriendId: friendId};

  await fetch(global.url + 'FriendList/CancelFriendRequest',
  {
  method: "POST",
  headers: {'Content-type':'application/json',
  'Authorization': 'Bearer ' + Cookies.get('AccessToken')},
  body: JSON.stringify(IdsModel)
  }).
  then(r => r.json()).then(res=>{

  }
  );
  this.getFriendData();
  this.render();

}

async createFriendRequest(){

  var friendUsernames = [];

  console.log(this.state.friends.filter(x => x.username === this.friendusername.value).length)
  if(this.friendusername.value != ""){

  if(this.state.friends.filter(x => x.username === this.friendusername.value).length == 0){
    if(this.state.outgoing.filter(x => x.username === this.friendusername.value).length == 0){
      if(this.state.requests.filter(x => x.username === this.friendusername.value).length == 0){
        if(this.state.blocks.filter(x => x.username === this.friendusername.value).length == 0){
          if(this.state.blocking.filter(x => x.username === this.friendusername.value).length == 0){

    var IdsModel = {UserId: this.state.userId, FriendUsername: this.friendusername.value};

    await fetch(global.url + 'FriendList/CreateFriendRequest',
    {
    method: "POST",
    headers: {'Content-type':'application/json',
    'Authorization': 'Bearer ' + Cookies.get('AccessToken')},
    body: JSON.stringify(IdsModel)
    }).
    then(r => r.json()).then(res=>{
        if(res == true){
          this.setState({friendRequestMessage: ""})

          this.getFriendData();
          this.render();
          this.friendusername.value = "";
          this.setState({friendRequestMessage: "Friend Request Sent"})


        }else{
          this.setState({friendRequestMessage: "User Does Not Exist"})

        }
    }
    );

  }else{
    this.setState({friendRequestMessage: this.friendusername.value + "has blocked you" })
  }
  }else{
    this.setState({friendRequestMessage: "You have blocked " + this.friendusername.value})
  }
  }else{
    this.setState({friendRequestMessage: this.friendusername.value + " has already requested to be friends with you, check you friend requests"})
  }
       
  }else{
    this.setState({friendRequestMessage: "You are already requesting to be friends with " + this.friendusername.value})
  }

  }else{
    this.setState({friendRequestMessage: "You are already friends with " + this.friendusername.value})
  }




 
    
}



}

async blockFriend(friendId){

  var IdsModel = {UserId: this.state.userId, FriendId: friendId};

  await fetch(global.url + 'FriendList/BlockFriend',
  {
  method: "POST",
  headers: {'Content-type':'application/json',
  'Authorization': 'Bearer ' + Cookies.get('AccessToken')},
  body: JSON.stringify(IdsModel)
  }).
  then(r => r.json()).then(res=>{

  }
  );
  this.getFriendData();
  this.render();

}


sort(event, sortKey, functional){
  console.log("test")

  console.log(functional)
  if(functional == "uDec"){
    sortKey = "username"
    const data = this.state.friends;
    data.sort((a,b) => a[sortKey].localeCompare(b[sortKey]))
    this.setState({friends: data.reverse()})
  }
  if(functional == "uAsc"){
    sortKey = "username"

    const data = this.state.friends;
    data.sort((a,b) => a[sortKey].localeCompare(b[sortKey]))
    this.setState({friends: data})
  }

  if(functional == "dDec"){
    sortKey = "date"
    const data = this.state.friends;
    data.sort((a,b) => a[sortKey].localeCompare(b[sortKey]))
    this.setState({friends: data.reverse()})
  }
  if(functional == "dAsc"){
    sortKey = "date"

    const data = this.state.friends;
    data.sort((a,b) => a[sortKey].localeCompare(b[sortKey]))
    this.setState({friends: data.reverse()})
  }
  
}

async search(){
  await this.getFriendData();
  var newFriends =[];
  {this.state.friends.map(friend =>

    (friend.username.includes(this.searchValue.value)) ?
    (newFriends.push(friend) ) : ("")



  )}


  this.setState({friends: newFriends})
  this.render()

}




  render () {



    
  const friendstable = {
    display: 'block',

    height: '50vh',
    overflowY: "auto"
  };
  const friendrequests = {
    display: 'block',
    height: '20vh',
    overflowY: "auto"
  };

  const pendingrequests = {
    display: 'block',
    height: '20vh',

    overflowY: "auto"
  };



  var filePath = "\\uploaded\\";

    return (
  <div>
                        <div class="ui action input">
                            <input type="text" name="friendusername" placeholder="Enter Friends Username" ref={(input) => this.friendusername = input}></input>
                        </div>
                        <button class="ui button" onClick={this.createFriendRequest}>Send Friend Request</button>
                        <br />
                        {this.state.friendRequestMessage}
                        <br />
                        <select class="ui search dropdown" name="sorting" ref={(input) => this.currentsort = input} id="sorting" onChange={e => this.sort(e, 'username', this.currentsort.value)}>
                        <option value="" selected disabled hidden>Sort</option>

                          <option value="uDec">Username Decending</option>
                          <option value="uAsc">UserName Ascending</option>
                          <option value="aAsc">Date Ascending</option>
                          <option value="dAsc">Date Ascending</option>

                        </select>

                        <div class="ui action input">

                          <input type="text" name="searchValue" placeholder="Search..." ref={(input) => this.searchValue = input} onChange={this.search} onClick={this.search}></input>
                        </div>


                        
<Table sortable striped style={friendstable}>

                        <Table.Body>
                            <Table.Row>    
                            <Table.Cell >Friends</Table.Cell>
                            </Table.Row>
                            {this.state.friends.map(friend =>
                            <Table.Row>        
                                <Table.Cell>

                                {
(friend.userProfileImage != null && friend.userProfileImage != "") ?
                                    (                    
                                      
                         
                                      
                                      <Image style={{'font-size':42}}  src= {filePath + friend.userProfileImage}  circular />



                                  
                                      ) : (                                <Image avatar src='https://react.semantic-ui.com/images/wireframe/square-image.png' circular />
                                      )}


                                </Table.Cell> 

                                <Table.Cell>

                                <a href={"/profile?id=" + friend.userId} >{friend.username} </a>

                                <br />
                {   
                                    (friend.status == "Offline") ?
                                        ("🔴") : ("🟢")
                                } 
                        {friend.status}
                            
                            </Table.Cell> 
                            <Table.Cell>
                            <a onClick={() => {
                        this.removeFriend(friend.userId);
                        }}  
                        style={{cursor: 'pointer'}}>
                          Remove Friend
                        </a>
                            
                            </Table.Cell>
                            <Table.Cell>
                            <a onClick={() => {
                        this.blockFriend(friend.userId);
                        }}  
                        style={{cursor: 'pointer'}}>
                          Block Friend
                        </a>                            
                            </Table.Cell>
                        </Table.Row>
                        )}
                    </Table.Body>
                </Table>        

                <Table stackable striped style={friendrequests}>
                        <Table.Body>
                            <Table.Row>        
                                <Table.Cell>Requests</Table.Cell>
                            </Table.Row>
                            {this.state.requests.map(friend =>
                            <Table.Row>  
                                                  <Table.Cell>

{
(friend.userProfileImage != null && friend.userProfileImage != "") ?
    (                                           <Image avatar src= {filePath + friend.userProfileImage}  circular />

      ) : (                                <Image avatar src='https://react.semantic-ui.com/images/wireframe/square-image.png' circular />
      )}


</Table.Cell>       
                                <Table.Cell>
                                  
                                <a href={"/profile?id=" + friend.userId} >{friend.username} </a>
                                <br />
                {   
                                    (friend.status == "Offline") ?
                                        ("🔴") : ("🟢")
                                } 
                        {friend.status}
                            
                            </Table.Cell>
                            <Table.Cell>
                            <a onClick={() => {
                        this.approveFriend(friend.userId);
                        }}  
                        style={{cursor: 'pointer'}}>
                          Accept Request
                        </a>
                            
                            </Table.Cell>
                            <Table.Cell>
                            <a onClick={() => {
                        this.blockFriend(friend.userId);
                        }}  
                        style={{cursor: 'pointer'}}>
                          Block User
                        </a>                            
                            </Table.Cell>
                        </Table.Row>
                        )}
                    </Table.Body>
                </Table > 

                               <Table stackable striped style={pendingrequests} >
                        <Table.Body>
                            <Table.Row>        
                                <Table.Cell>Pending</Table.Cell>
                            </Table.Row>
                            {this.state.outgoing.map(friend =>
                            <Table.Row> 
                                                  <Table.Cell>

{
(friend.userProfileImage != null && friend.userProfileImage != "") ?
    (                                           <Image avatar src= {filePath + friend.userProfileImage}  circular />

      ) : (                                <Image avatar src='https://react.semantic-ui.com/images/wireframe/square-image.png' circular />
      )}


</Table.Cell>        
                                <Table.Cell>
                                <a href={"/profile?id=" + friend.userId} >{friend.username} </a>
                                <br />
                {   
                                    (friend.status == "Offline") ?
                                        ("🔴") : ("🟢")
                                } 
                        {friend.status}
                            
                            </Table.Cell>
                            <Table.Cell>
                            <a onClick={() => {
                        this.cancelFriendRequest(friend.userId);
                        }}  
                        style={{cursor: 'pointer'}}>
                          Cancel Request
                        </a>                            
                            </Table.Cell>
                        </Table.Row>
                        )}
                    </Table.Body>
                </Table>     

  </div>
    );
  }
}
export default FriendsList;