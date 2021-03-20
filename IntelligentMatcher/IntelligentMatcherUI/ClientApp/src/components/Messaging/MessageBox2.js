
  render() {
    //setInterval(this.render, 500);
    const messagetablestyle = {
      height: "80vh",
      width: "100vh",
      backgroundcolor: "#2F3136",
      color: "#36393F"

    };


if(this.state.channelId == 0){
    

    return(<div>
      
      <input type="text" name="channelname"  ref={(input) => this.userselect = input} onChange={this.changeUser}/>


        <br />
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


<input type="text" name="channelname"  ref={(input) => this.userselect = input} onChange={this.changeUser}/>
        

        <br />


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
        
        
        
        
        <table style={messagetablestyle}>
        
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

    return(
    
    
    <div>

      
<input type="text" name="channelname"  ref={(input) => this.userselect = input} onChange={this.changeUser}/>
    
          <br />

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
        
        
        
        
        <table  style={messagetablestyle}>
        
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
