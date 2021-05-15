import React, { Component } from 'react';
import { Table, Grid } from 'semantic-ui-react'
import ReactDataGrid from 'react-data-grid';
import ProfileData from "../Components/ProfileData";
import { Image } from 'semantic-ui-react'
import ImageUpload from "../Components/ImageUpload";
import '../.././../../index'
import { Redirect } from 'react-router'
import _ from 'lodash'
import Messaging from '../../Messaging/Pages/Messaging';
import Cookies from 'js-cookie';
import jwt from 'jwt-decode';
export class TopBar extends Component {
  static displayName = TopBar.name;

  constructor(props) {
    const idToken = Cookies.get('IdToken');
    const decodedIdToken = jwt(idToken);
    const userId = decodedIdToken.id;
    super(props);

    super(props);

    this.state = 
    {  
        userId: parseInt(userId),
        viewingId: 0,
        friendStatus: "",
        accountProfileData: [],
        otherData: [],
        file: ''   ,
        reportmessage: "",
        descriptionmessage: "",
        edit: "",
        navigate: ""
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
    this.reportUser = this.reportUser.bind(this);
    this.getAccountData = this.getAccountData.bind(this);
    this.routeChange = this.routeChange.bind(this);

    this.getFriendStatus();
    this.getAccountData();
  }


  routeChange() {
    this.setState({ navigate: true });
  }


async reportUser(){

    if(this.reporttext.value.length > 0){

        this.render();

        var IdsModel = {ReportingId: this.state.userId, ReportedId: this.state.viewingId, Report: this.reporttext.value};
  
        await fetch(global.url + 'UserProfile/ReportUser',
        {
            method: "POST",
            headers: {'Content-type':'application/json',
            'Authorization': 'Bearer ' + Cookies.get('AccessToken')},
            body: JSON.stringify(IdsModel)
        }).
        then(r => r.json()).then(res=>{
        }
        );

    this.reporttext.value = "";
    this.state.reportmessage = "User Reported";
    this.getAccountData();
    this.getFriendStatus();
    this.render();

    }
    else
    {
        this.state.reportmessage = "Please enter some data";
        this.render();
    }
}


  async cancelFriendRequest(){

    this.render();
  
    var IdsModel = {UserId: this.state.userId, FriendId: this.state.viewingId};
  
    await fetch(global.url + 'FriendList/CancelFriendRequest',
    {
    method: "POST",
    headers: {'Content-type':'application/json',
    'Authorization': 'Bearer ' + Cookies.get('AccessToken')},
    body: JSON.stringify(IdsModel)
    }).
    then(r => r.json()).then(res=>{});

    this.getAccountData();
    this.getFriendStatus();
    this.render();
  }

async createFriendRequest(){

      var IdsModel = {UserId: this.state.userId, FriendUsername: this.state.otherData.username};
  
      await fetch(global.url + 'FriendList/CreateFriendRequest',
      {
      method: "POST",
      headers: {'Content-type':'application/json',
      'Authorization': 'Bearer ' + Cookies.get('AccessToken')},
      body: JSON.stringify(IdsModel)
      }).
      then(r => r.json()).then(res=>{ });
  
      this.getAccountData();
      this.getFriendStatus();
  
      this.render();
      
  }
 
  
  


  async removeFriend(){

    this.render();
  
    var IdsModel = {UserId: this.state.userId, FriendId: this.state.viewingId};
  
    await fetch(global.url + 'FriendList/RemoveFriend',
    {
    method: "POST",
    headers: {'Content-type':'application/json',
    'Authorization': 'Bearer ' + Cookies.get('AccessToken')},
    body: JSON.stringify(IdsModel)
    }).
    then(r => r.json()).then(res=>{ });

    this.getAccountData();
    this.getFriendStatus();
    this.render(); 
  }

  async saveData(){
    var userProfileModel = {UserId: this.state.userId, Description: this.Description.value, Jobs: this.state.accountProfileData.jobs, Goals: this.state.accountProfileData.goals, Age: parseInt(this.state.accountProfileData.age), Gender: this.state.accountProfileData.gender, Ethnicity: this.state.accountProfileData.ethnicity, SexualOrientation: this.state.accountProfileData.sexualOrientation, Height: this.state.accountProfileData.height, Hobbies: this.state.accountProfileData.hobbies, Intrests: this.state.accountProfileData.intrests, Visibility: this.state.accountProfileData.visibility};

    await fetch(global.url + 'UserProfile/SaveUserProfile',
    {
    method: "POST",
    headers: {'Content-type':'application/json',
    'Authorization': 'Bearer ' + Cookies.get('AccessToken')},
    body: JSON.stringify(userProfileModel)
    }).
    then(r => r.json()).then(res=>{
        this.setState({descriptionmessage: "Account Information Updated"});

    }
    );    
    this.getAccountData();
    this.render();


}

  async getFriendStatus(){

    var IdsModel = {UserId: this.state.userId, FriendId: this.state.viewingId};

    await fetch(global.url + 'UserProfile/GetFriendStatus',
    {
        method: "POST",
        headers: {'Content-type':'application/json',
        'Authorization': 'Bearer ' + Cookies.get('AccessToken')},
        body: JSON.stringify(IdsModel)
    }).then(r => r.json()).then(res=>{
        this.setState({friendStatus: res});
    }   
    ); 
}

async getAccountData(){
    await fetch(global.url + 'UserProfile/GetUserProfile',
    {
        method: "POST",
        headers: {'Content-type':'application/json',
        'Authorization': 'Bearer ' + Cookies.get('AccessToken')},
        body: JSON.stringify(this.state.viewingId)
    }).then(r => r.json()).then(res=>{
        this.setState({accountProfileData: res});
    }   
    ); 

    await fetch(global.url + 'UserProfile/GetOtherData',
    {
        method: "POST",
        headers: {'Content-type':'application/json',
        'Authorization': 'Bearer ' + Cookies.get('AccessToken')},
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

    const circleImageStyle = 
    {
            position: 'relative',
            width: '20%',
            height: '20%',
            overflow: 'hidden',
            borderRadius: '100vh',
            width: '200px',
            height: '200px'
          
    }

    
    if (this.state.navigate) {
        return <Redirect to={{ pathname:"/Messaging"}}      push={true} />
    }

    var filePath = "\\uploaded\\";
    return (
        <Grid centered  divided='vertically'>
        <Grid.Row columns={2}>
            <Grid.Column textAlign='center'>
                <Grid.Row >
                {
                    (this.state.accountProfileData.photo != null && this.state.accountProfileData.photo != "") ?
                    ( <Image style={circleImageStyle}  src= {filePath +this.state.accountProfileData.photo}   />) : 
                    ( <Image avatar src='https://react.semantic-ui.com/images/wireframe/square-image.png' size='small' circular />)
                }              
                </Grid.Row>
                <Grid.Row>            
                {
                    (this.state.viewingId == this.state.userId) ?
                    (<ImageUpload/>) : 
                    ("")
                }    
                </Grid.Row>
                <Grid.Row >
                {
                    (this.state.friendStatus.status == "Friends" && this.state.userId != this.state.viewingId) ?
                    (<div><button class="ui button" onClick={this.removeFriend}>Remove Friend</button>   <button class="ui button" onClick={this.routeChange} >Message</button><br />  <textarea type="text" placeholder="report" name="reporttext"  ref={(input) => this.reporttext = input} maxlength="1000"></textarea><br /><button class="ui button"  onClick={this.reportUser}>Report</button><br />{this.state.reportmessage}</div>) : 
                    ("")
                }
                {
                    (this.state.friendStatus.status == "None" && this.state.userId != this.state.viewingId) ?
                    (<div><button class="ui button" onClick={this.createFriendRequest}>Add Friend</button><br />  <textarea type="text" name="reporttext" placeholder="report" ref={(input) => this.reporttext = input} maxlength="1000"></textarea><br /><button class="ui button"   onClick={this.reportUser}>Report</button><br />{this.state.reportmessage}</div>) : 
                    ("")
                }       
                {
                    (this.state.friendStatus.status == "Requested" && this.state.userId != this.state.viewingId)  ?
                    (<div><button class="ui button" onClick={this.cancelFriendRequest}>Cancel Request</button> <br /> <textarea type="text" name="reporttext" placeholder="report" ref={(input) => this.reporttext = input} maxlength="1000"></textarea><br /><button   class="ui button"  onClick={this.reportUser}>Report</button><br />{this.state.reportmessage}</div>) : 
                    ("")
                }           
                </Grid.Row>
            </Grid.Column>
            <Grid.Column>
                <Grid.Row >
                    {this.state.otherData.username}
                    <br />
                    {   
                        (this.state.accountProfileData.status == "Offline") ?
                        ("🔴") : 
                        ("🟢")
                    } 
                    {this.state.accountProfileData.status}
                </Grid.Row>
                <Grid.Row >
                    Joined: {this.state.otherData.joinDate}
                </Grid.Row>
 
                <Grid.Row >

                    {
                        (this.state.userId == this.state.viewingId && this.state.edit == "edit") ?
                        (       
                        <div>               
                            <form class="ui form">
                                Description:
                                <textarea type="text" name="description" defaultValue={this.state.accountProfileData.description} ref={(input) => this.Description = input} maxlength="1000"></textarea>
                            </form>
                            <button class="ui button" onClick={this.saveData}>Save Description</button> <button class="ui button" onClick={() => {        this.setState({edit: "no"})}}>Cancel</button>
                             <br /> 
                             {this.state.descriptionmessage}
                        </div>   
                         ) : 
                         (<div> 
                             <br /> 
                             {this.state.accountProfileData.description}
                         </div>
                         )
                    }
                    {
                        (this.state.userId == this.state.viewingId && this.state.edit != "edit" ) ?
                        (<div>  
                            <br />           
                            <button class="ui button" onClick={() => {        this.setState({edit: "edit"})}}>Edit</button>
                        </div>
                        ) : 
                        ("")
                    }

                </Grid.Row>
            </Grid.Column>
        </Grid.Row>
    </Grid>
    );
  }
}
export default TopBar;