import React, { Component } from 'react';
import { Table, Grid, Form } from 'semantic-ui-react'
import ReactDataGrid from 'react-data-grid';
import ProfileData from "../Components/ProfileData";
import TopBar from "../Components/TopBar";
import UserListings from "../Components/UserListings";
import '../.././../../App'
import Cookies from 'js-cookie';
import '../.././../../index'
import jwt from 'jwt-decode';
import _ from 'lodash'

export class UserProfile extends Component {
  static displayName = UserProfile.name;

  constructor(props) {
    const idToken = Cookies.get('IdToken');
    const decodedIdToken = jwt(idToken);
    const userId = decodedIdToken.id;
    super(props);

    this.state = {
        userId: parseInt(userId),
        viewingId: 0,
        otherData: [],
        friendStatus: "",

        visibility: false
        };
        this.getViewStatus = this.getViewStatus.bind(this);
        let url = window.location.href;
        url = url.split("id=")
        if(url.length > 1){
          this.state.viewingId = parseInt(url[1]);   
  
      }else{
          this.state.viewingId = this.state.userId;
  
      }
        this.getViewStatus();

        
  

  }


  async getViewStatus(){

    var IdsModel = {UserId: this.state.userId, FriendId: this.state.viewingId};

    await fetch(global.url + 'UserProfile/GetViewStatus',
    {
        method: "POST",
        headers: {'Content-type':'application/json',
        'Authorization': 'Bearer ' + Cookies.get('AccessToken')},
        body: JSON.stringify(IdsModel)
    }).then(r => r.json()).then(res=>{
        this.setState({visibility: res});
        console.log("Vsibility: " + this.state.visibility);
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

  render () {




    if(this.state.friendStatus.status != "Blocked"){

    return (

        <Grid centered divided='vertically'>

        <Grid.Row columns={1} >
            <Grid.Column width={9} centered>
            <TopBar />



            </Grid.Column>
    </Grid.Row>

    <Grid.Row columns={2}>
    <Grid.Column>

    <UserListings />

</Grid.Column>
<Grid.Column>
<div>
{
(this.state.visibility == true || this.state.viewingId == this.state.userId) ?
                                    ( <ProfileData />
                                        ) : ("Private")}


  </div>
</Grid.Column>

  </Grid.Row>
  </Grid>

    );
  }
    else{


      return (
        <Grid className="segment centered">
                User Not Found
        </Grid>
    );
    }
  }
}
export default UserProfile;
