import React, { Component } from 'react';
import { Table, Grid, Image } from 'semantic-ui-react'
import ReactDataGrid from 'react-data-grid';
import FriendsList from "../../FriendsList/Pages/FriendsList";
import '../.././../../index'
import Cookies from 'js-cookie';
import jwt from 'jwt-decode';

import _ from 'lodash'

export class UserListings extends Component {
  static displayName = UserListings.name;
  constructor(props) {
    const idToken = Cookies.get('IdToken');
    const decodedIdToken = jwt(idToken);
    const userId = decodedIdToken.id;
    super(props);

    this.state = {  
        viewingId: 0  ,
        userListings: [],
        userId: parseInt(userId),
        };


    let url = window.location.href;
    url = url.split("id=")
    this.state.viewingId = parseInt(url[1]);   

    this.getUserListings = this.getUserListings.bind(this);

    this.getUserListings();

}


async getUserListings(){
    await fetch(global.url + 'UserProfile/GetUserListings',
    {
        method: "POST",
        headers: {'Content-type':'application/json',
        'Authorization': 'Bearer ' + Cookies.get('AccessToken')},
        body: JSON.stringify(this.state.viewingId)
    }).then(r => r.json()).then(res=>{
        this.setState({userListings: res});
    }   

    ); 
}



  render () {

            return ( <div>                            {this.state.userListings.map(listing =>    
                
                <Table sortable striped onClick >

                    <Table.Body>
                        <Table.Row>    
                        </Table.Row>
                        <Table.Row>    
                            <Table.Cell >            
                                <h1> <a href={"/listing?id=" + listing.id} >{listing.title}  </a>  </h1>
                            </Table.Cell>
                        </Table.Row>
                        <Table.Row>    
                            <Table.Cell >            
                                {listing.details}  
                            </Table.Cell>
                        </Table.Row>
                    </Table.Body>
                </Table>   
                
                
                )
                
                
                }                
                
                
                
                </div> );
  }
}
export default UserListings;