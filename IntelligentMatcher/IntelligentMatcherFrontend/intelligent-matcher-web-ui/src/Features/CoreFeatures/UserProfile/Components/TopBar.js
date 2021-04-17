import React, { Component } from 'react';
import { Table, Grid } from 'semantic-ui-react'
import ReactDataGrid from 'react-data-grid';
import ProfileData from "../Components/ProfileData";
import { Image } from 'semantic-ui-react'
import ImageUpload from "../Components/ImageUpload";

import _ from 'lodash'

export class TopBar extends Component {
  static displayName = TopBar.name;

  constructor(props) {

    super(props);

    this.state = {  
        userId: 1,
        viewingId: 0,
        friendStatus: "",
        accountProfileData: [],
        otherData: [],
        file: ''   

        };
        let url = window.location.href;
        url = url.split("id=")
        this.state.viewingId = parseInt(url[1]);   
        this.getFriendStatus = this.getFriendStatus.bind(this);
        this.saveData = this.saveData.bind(this);
        this.removeFriend = this.removeFriend.bind(this);
        this.cancelFriendRequest = this.cancelFriendRequest.bind(this);
        this.createFriendRequest = this.createFriendRequest.bind(this);
        this.setFile = this.setFile.bind(this);
        this.getAccountData = this.getAccountData.bind(this);
        this.getFriendStatus();
        this.getAccountData();
  }





  async cancelFriendRequest(){
    this.render();
  
    var IdsModel = {UserId: this.state.userId, FriendId: this.state.viewingId};
  
    await fetch('http://localhost:5000/FriendList/CancelFriendRequest',
    {
    method: "POST",
    headers: {'Content-type':'application/json'},
    body: JSON.stringify(IdsModel)
    }).
    then(r => r.json()).then(res=>{
  
    }
    );
      this.getAccountData();
      this.getFriendStatus();
  
      this.render();
  
  }

  async createFriendRequest(){

  
  
      var IdsModel = {UserId: this.state.userId, FriendUsername: this.state.otherData.username};
  
      await fetch('http://localhost:5000/FriendList/CreateFriendRequest',
      {
      method: "POST",
      headers: {'Content-type':'application/json'},
      body: JSON.stringify(IdsModel)
      }).
      then(r => r.json()).then(res=>{
  
      }
      );
  
      this.getAccountData();
      this.getFriendStatus();
  
      this.render();
      
  }
 
  
  


  async removeFriend(){
    this.render();
  
    var IdsModel = {UserId: this.state.userId, FriendId: this.state.viewingId};
  
    await fetch('http://localhost:5000/FriendList/RemoveFriend',
    {
    method: "POST",
    headers: {'Content-type':'application/json'},
    body: JSON.stringify(IdsModel)
    }).
    then(r => r.json()).then(res=>{
  
    }
    );
    this.getAccountData();
    this.getFriendStatus();

    this.render();
  
  }

  async saveData(){
    var userProfileModel = {UserId: this.state.userId, Description: this.Description.value, Jobs: this.state.accountProfileData.jobs, Goals: this.state.accountProfileData.goals, Age: parseInt(this.state.accountProfileData.age), Gender: this.state.accountProfileData.gender, Ethnicity: this.state.accountProfileData.ethnicity, SexualOrientation: this.state.accountProfileData.sexualOrientation, Height: this.state.accountProfileData.height, Hobbies: this.state.accountProfileData.hobbies, Intrests: this.state.accountProfileData.intrests, Visibility: this.state.accountProfileData.visibility};

    await fetch('http://localhost:5000/UserProfile/SaveUserProfile',
    {
    method: "POST",
    headers: {'Content-type':'application/json'},
    body: JSON.stringify(userProfileModel)
    }).
    then(r => r.json()).then(res=>{
  
    }
    );
    this.getAccountData();
    this.render();
}

  async getFriendStatus(){

    var IdsModel = {UserId: this.state.userId, FriendId: this.state.viewingId};

    await fetch('http://localhost:5000/UserProfile/GetFriendStatus',
    {
        method: "POST",
        headers: {'Content-type':'application/json'},
        body: JSON.stringify(IdsModel)
    }).then(r => r.json()).then(res=>{
        this.setState({friendStatus: res});
    }   
    ); 
}

async getAccountData(){
    await fetch('http://localhost:5000/UserProfile/GetUserProfile',
    {
        method: "POST",
        headers: {'Content-type':'application/json'},
        body: JSON.stringify(this.state.viewingId)
    }).then(r => r.json()).then(res=>{
        this.setState({accountProfileData: res});
    }   
    ); 

    await fetch('http://localhost:5000/UserProfile/GetOtherData',
    {
        method: "POST",
        headers: {'Content-type':'application/json'},
        body: JSON.stringify(this.state.viewingId)
    }).then(r => r.json()).then(res=>{
        this.setState({otherData: res});
    }   
    ); 
}
setFile(e) {    
    
                    this.setState({ file: e.target.files[0] });    
    
            } 
  render () {

    var filePath = "\\uploaded\\";
    return (
        <Grid centered  divided='vertically'>
        <Grid.Row columns={2}>
            <Grid.Column textAlign='center'>

            <Grid.Row >

            {
(this.state.accountProfileData.photo != null && this.state.accountProfileData.photo != "") ?
    (                                                         <Image avatar src= {filePath +this.state.accountProfileData.photo} size='small' circular />


      ) : (                                <Image avatar src='https://react.semantic-ui.com/images/wireframe/square-image.png' size='small' circular />
      )}

                
                
                
                </Grid.Row>

            <Grid.Row>            {
(this.state.viewingId == this.state.userId) ?
                                    (            <ImageUpload/>
                                      ) : ("")}</Grid.Row>
            <Grid.Row >

            {
                (this.state.friendStatus.status == "Friends" && this.state.userId != this.state.viewingId) ?(
                    <div><button class="ui button" onClick={this.removeFriend}>Remove Friend</button>   <button class="ui button" onClick={this.saveData}>Message</button></div>


                 ) : ("")
            }
            {
                (this.state.friendStatus.status == "None" && this.state.userId != this.state.viewingId) ?(
                    <button class="ui button" onClick={this.createFriendRequest}>Add Friend</button>
                 ) : ("")
            }       
            {
                (this.state.friendStatus.status == "Requested" && this.state.userId != this.state.viewingId)  ?(
                    <button class="ui button" onClick={this.cancelFriendRequest}>Cancel Request</button>
                 ) : ("")
            }         
                
                
                
                
                </Grid.Row>
            </Grid.Column>
            <Grid.Column>
                <Grid.Row >
                {this.state.otherData.username}
                <br />
                {   
                                    (this.state.accountProfileData.status == "Offline") ?
                                        ("🔴") : ("🟢")
                                } 
                        {this.state.accountProfileData.status}
                </Grid.Row>
                <Grid.Row >
                Joined: {this.state.otherData.joinDate}
                </Grid.Row>
                <Grid.Row >

            {
                (this.state.userId == this.state.viewingId
                    ) ?(       
                        <div>               
                        <form class="ui form">
                            Description:
                        <textarea type="text" name="description" defaultValue={this.state.accountProfileData.description} ref={(input) => this.Description = input}></textarea>

                        </form>
                                                <button class="ui button" onClick={this.saveData}>Save Data</button></div>   

                 ) : (<div> <br /> {this.state.accountProfileData.description}</div>)
            }
                </Grid.Row>

            </Grid.Column>
    </Grid.Row>

        </Grid>


    );
  }
}
export default TopBar;