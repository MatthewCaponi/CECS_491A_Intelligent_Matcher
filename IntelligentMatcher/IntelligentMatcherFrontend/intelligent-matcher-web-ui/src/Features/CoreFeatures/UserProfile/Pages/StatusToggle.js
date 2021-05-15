
import React from "react";
import '../.././../../index'
import Cookies from 'js-cookie';
import jwt from 'jwt-decode';

function StatusToggle() {
  const idToken = Cookies.get('IdToken');
  const decodedIdToken = jwt(idToken);
  const userId = decodedIdToken.id;

  fetch(global.url + 'UserProfile/SetOnline',
    {
        method: "POST",
        headers: {'Content-type':'application/json',
        'Authorization': 'Bearer ' + Cookies.get('AccessToken')},
        body: JSON.stringify(parseInt(userId))
    }).then(r => r.json()).then(res=>{
    }   
    ); 


    const onWindowOrTabClose = event => {
      fetch(global.url + 'UserProfile/SetOffline',
      {
          method: "POST",
          headers: {'Content-type':'application/json',
          'Authorization': 'Bearer ' + Cookies.get('AccessToken')},
          body: JSON.stringify(parseInt(userId))
      }).then(r => r.json()).then(res=>{
      }   
      ); 
    };

    window.addEventListener('beforeunload', onWindowOrTabClose);


  return true;
}

export default StatusToggle;