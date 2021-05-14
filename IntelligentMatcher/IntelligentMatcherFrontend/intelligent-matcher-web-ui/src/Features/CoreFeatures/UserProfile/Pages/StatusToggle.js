
import React from "react";
import '../.././../../index'


function StatusToggle() {
  

  fetch(global.url + 'UserProfile/SetOnline',
    {
        method: "POST",
        headers: {'Content-type':'application/json'},
        body: JSON.stringify(1)
    }).then(r => r.json()).then(res=>{
    }   
    ); 


    const onWindowOrTabClose = event => {
      fetch(global.url + 'UserProfile/SetOffline',
      {
          method: "POST",
          headers: {'Content-type':'application/json'},
          body: JSON.stringify(1)
      }).then(r => r.json()).then(res=>{
      }   
      ); 
    };

    window.addEventListener('beforeunload', onWindowOrTabClose);


  return true;
}

export default StatusToggle;