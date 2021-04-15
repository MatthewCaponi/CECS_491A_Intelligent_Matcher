import React, { Component } from 'react';
import { Table, Grid } from 'semantic-ui-react'
import ReactDataGrid from 'react-data-grid';

import _ from 'lodash'

export class ProfileData extends Component {
  static displayName = ProfileData.name;
  constructor(props) {

    super(props);

    this.state = {  
        viewingId: 0  ,
        accountProfileData: [],
        userId: 1
        };


    let url = window.location.href;
    url = url.split("id=")
    this.state.viewingId = parseInt(url[1]);   
    this.saveData = this.saveData.bind(this);

    this.getAccountData = this.getAccountData.bind(this);

    this.getAccountData();

}



async saveData(){
    var userProfileModel = {UserId: this.state.userId, Description: this.state.accountProfileData.description, Jobs: this.Jobs.value, Goals: this.Goals.value, Age: parseInt(this.Age.value), Gender: this.Gender.value, Ethnicity: this.Ethnicity.value, SexualOrientation: this.SexualOrientation.value, Height: this.Height.value, Hobbies: this.Hobbies.value, Intrests: this.Intrests.value, Visibility: this.Visibility.value};

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

    const renderTable = () => {

        if(this.state.userId == this.state.viewingId){
            return (
    
                <div>

                
                <Table>
                <Table.Body>
      
                         <Table.Row>        
                            <Table.Cell>
                            Jobs:
                            <form class="ui form">

                            <textarea type="text" name="jobs" defaultValue={this.state.accountProfileData.jobs} ref={(input) => this.Jobs = input}></textarea>
                            </form>
                            </Table.Cell>
                         </Table.Row>
                         <Table.Row>        
                            <Table.Cell>
                            Goals:
                            <form class="ui form">

                            <textarea type="text" name="goals" defaultValue={this.state.accountProfileData.goals} ref={(input) => this.Goals = input}></textarea>
                            </form>

                            </Table.Cell>
                         </Table.Row>
                         <Table.Row>        
                            <Table.Cell>
                            Hobbies:
                            <form class="ui form">

                            <textarea type="text" name="goals" defaultValue={this.state.accountProfileData.hobbies} ref={(input) => this.Hobbies = input}></textarea>
                            </form>
                            </Table.Cell>
                         </Table.Row>
                
                         <Table.Row>        
                            <Table.Cell>
                            Intrests:
                            <form class="ui form">

                            <textarea type="text" name="goals" defaultValue={this.state.accountProfileData.intrests} ref={(input) => this.Intrests = input}></textarea>
                            </form>

                            </Table.Cell>
                         </Table.Row>
                         <Table.Row>        
                            <Table.Cell>
                            Age:
                            <form class="ui form">

                            <input type="number" name="description" defaultValue={this.state.accountProfileData.age} ref={(input) => this.Age = input}></input>
                            </form>

                            </Table.Cell>
                         </Table.Row>
                         <Table.Row>        
                            <Table.Cell>
                            Gender:

                            <select class="ui search dropdown" name="sorting" ref={(input) => this.Gender = input} id="sorting" >
                                        <option value={this.state.accountProfileData.gender} selected disabled hidden>{this.state.accountProfileData.gender}</option>
                
                                          <option value="Male">Male</option>
                                          <option value="Female">Female</option>
                
                                        </select>            
                                </Table.Cell>
                         </Table.Row>
                
                         <Table.Row>        
                            <Table.Cell>
                            Ethnicity:
                            <form class="ui form">

                            <input type="text" name="goals" defaultValue={this.state.accountProfileData.ethnicity} ref={(input) => this.Ethnicity = input}></input>
                            </form>
                            </Table.Cell>
                         </Table.Row>
                
                         <Table.Row>        
                            <Table.Cell>
                            Sexual Orientation:
                            <form class="ui form">

                            <input type="text" name="goals" defaultValue={this.state.accountProfileData.sexualOrientation} ref={(input) => this.SexualOrientation = input}></input>
                            </form>
                            </Table.Cell>
                         </Table.Row>
                         <Table.Row>        
                            <Table.Cell>
                            Height:
                            <form class="ui form">

                            <input type="text" name="goals" defaultValue={this.state.accountProfileData.height} ref={(input) => this.Height = input}></input>
                            </form>
                            </Table.Cell>
                         </Table.Row>
                
            
                
                         <Table.Row>        
                            <Table.Cell>
                            Profile Privacy Settings:
                            <select class="ui search dropdown" name="sorting" ref={(input) => this.Visibility = input} id="sorting" >
                                        <option value={this.state.accountProfileData.visibility} selected disabled hidden>{this.state.accountProfileData.visibility}</option>
                
                                          <option value="Private">Private</option>
                                          <option value="Friends">Friends</option>
                                          <option value="Public">Public</option>
                
                                        </select>            
                                </Table.Cell>
                         </Table.Row>
                         <Table.Row>        
                            <Table.Cell>           
                            <button class="ui button" onClick={this.saveData}>Save Data</button>
                            </Table.Cell>
                         </Table.Row>
                     </Table.Body>
                
                </Table>
                  </div>
            );
        }

        if(this.state.userId != this.state.viewingId){
            return (
    
                <div>

                
                <Table>
                <Table.Body>
         
                    

                 
                   
                        {    
                        (this.state.accountProfileData.jobs != null && this.state.accountProfileData.goals != "") ?(
                        <Table.Row>        
                             <Table.Cell>   
                                            
                                 Jobs:  <br /> 
                                {  this.state.accountProfileData.jobs}                                                                         
                            </Table.Cell>
                         </Table.Row> 
                         ) : ("")
                         }
                        {    
                        (this.state.accountProfileData.goals != null && this.state.accountProfileData.goals != "") ?(
                        <Table.Row>        
                             <Table.Cell>   
                                            
                                 Goals:  <br /> 
                                {  this.state.accountProfileData.goals}                                                                         
                            </Table.Cell>
                         </Table.Row> 
                         ) : ("")
                         }
                        {    
                        (this.state.accountProfileData.hobbies != null && this.state.accountProfileData.hobbies != "") ?(
                        <Table.Row>        
                             <Table.Cell>   
                                            
                                 Hobbies:  <br /> 
                                {  this.state.accountProfileData.hobbies}                                                                         
                            </Table.Cell>
                         </Table.Row> 
                         ) : ("")
                         }
                
                        {    
                        (this.state.accountProfileData.intrests != null && this.state.accountProfileData.intrests != "") ?(
                        <Table.Row>        
                             <Table.Cell>   
                                            
                                 Intrests:  <br /> 
                                {  this.state.accountProfileData.intrests}                                                                         
                            </Table.Cell>
                         </Table.Row> 
                         ) : ("")
                         }
                
                        {    
                        (this.state.accountProfileData.age != null && this.state.accountProfileData.age != 0) ?(
                        <Table.Row>        
                             <Table.Cell>   
                                            
                                 Age:  <br /> 
                                {  this.state.accountProfileData.age}                                                                         
                            </Table.Cell>
                         </Table.Row> 
                         ) : ("")
                         }  
                        {    
                        (this.state.accountProfileData.gender != null && this.state.accountProfileData.gender != "") ?(
                        <Table.Row>        
                             <Table.Cell>   
                                            
                                 Gender:  <br /> 
                                {  this.state.accountProfileData.gender}                                                                         
                            </Table.Cell>
                         </Table.Row> 
                         ) : ("")
                         }
                
                        {    
                        (this.state.accountProfileData.ethnicity != null && this.state.accountProfileData.ethnicity != "") ?(
                        <Table.Row>        
                             <Table.Cell>   
                                            
                                 Ethnicity:  <br /> 
                                {  this.state.accountProfileData.ethnicity}                                                                         
                            </Table.Cell>
                         </Table.Row> 
                         ) : ("")
                         }
                
                        {    
                        (this.state.accountProfileData.sexualOrientation != null && this.state.accountProfileData.sexualOrientation != "") ?(
                        <Table.Row>        
                             <Table.Cell>   
                                            
                                 Sexual Orientation:  <br /> 
                                {  this.state.accountProfileData.sexualOrientation}                                                                         
                            </Table.Cell>
                         </Table.Row> 
                         ) : ("")
                         }
                        {    
                        (this.state.accountProfileData.height != null && this.state.accountProfileData.height != "") ?(
                        <Table.Row>        
                             <Table.Cell>   
                                            
                                 Height:  <br /> 
                                {  this.state.accountProfileData.height}                                                                         
                            </Table.Cell>
                         </Table.Row> 
                         ) : ("")
                         }
                
              
                      
                    
                     </Table.Body>
                
                </Table>
                  </div>
            );
        }

    }

  
    return (
    renderTable()
    );
  }
}
export default ProfileData;