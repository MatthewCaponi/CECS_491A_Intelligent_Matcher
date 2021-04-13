import React, { Component } from 'react';
import { Table, Grid } from 'semantic-ui-react'
import ReactDataGrid from 'react-data-grid';

import _ from 'lodash'

export class UserProfile extends Component {
  static displayName = UserProfile.name;
  constructor(props) {

    super(props);

    this.state = {  
        viewingId: 'UserId'  ,
        accountProfileData: []
        };


    let url = window.location.href;
    url = url.split("id=")
    this.state.viewingId = parseInt(url[1]);   

    this.getAccountData = this.getAccountData.bind(this);
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
}



  render () {
    this.getAccountData();


  
    return (
        
  <div>
                     
{this.state.accountProfileData.userId}
  </div>
    );
  }
}
export default UserProfile;