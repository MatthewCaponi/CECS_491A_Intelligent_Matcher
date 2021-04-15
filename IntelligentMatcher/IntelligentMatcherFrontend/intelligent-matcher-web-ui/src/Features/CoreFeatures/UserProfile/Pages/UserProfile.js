import React, { Component } from 'react';
import { Table, Grid } from 'semantic-ui-react'
import ReactDataGrid from 'react-data-grid';
import ProfileData from "../Components/ProfileData";
import TopBar from "../Components/TopBar";

import _ from 'lodash'

export class UserProfile extends Component {
  static displayName = UserProfile.name;

  constructor(props) {

    super(props);

    this.state = {  
        userId: 1,
        viewingId: 0,

        visibility: false
        };
        this.getViewStatus = this.getViewStatus.bind(this);
        let url = window.location.href;
        url = url.split("id=")
        this.state.viewingId = parseInt(url[1]);   
        this.getViewStatus();
  }


  async getViewStatus(){

    var IdsModel = {UserId: this.state.userId, FriendId: this.state.viewingId};

    await fetch('http://localhost:5000/UserProfile/GetViewStatus',
    {
        method: "POST",
        headers: {'Content-type':'application/json'},
        body: JSON.stringify(IdsModel)
    }).then(r => r.json()).then(res=>{
        this.setState({visibility: res});
        console.log(this.state.visibility);
    }   
    ); 
}

  render () {


  
    return (
        <Grid centered divided='vertically'>

        <Grid.Row columns={1} >
            <Grid.Column width={9} centered>
            <TopBar />



            </Grid.Column>
    </Grid.Row>

    <Grid.Row columns={2}>
    <Grid.Column>
</Grid.Column>
<Grid.Column>
<div>
{
(this.state.visibility == true || this.state.viewingId == this.state.userId) ?
                                    ( <div>Change your profile picture         <ProfileData /></div>  
                                        ) : ("Private")}
                            

  </div>
</Grid.Column>

  </Grid.Row>
  </Grid>

    );
  }
}
export default UserProfile;