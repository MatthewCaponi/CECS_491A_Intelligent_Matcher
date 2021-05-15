import React from 'react';
import ReactDOM from 'react-dom';
import App from './App';
import './index.css'
import 'semantic-ui-css/semantic.min.css'
import 'react-toastify/dist/ReactToastify.min.css';
import AuthnContextProvider from './Context/AuthnContext';
import ErrorBoundary from './Shared/ErrorBoundrary';

var idToken = "";
var decodedIdToken = "";
var userId = 0;
if (Cookies.get('IdToken') != null){
  idToken = Cookies.get('IdToken');
  decodedIdToken = jwt(idToken);
  userId = decodedIdToken.id;

  fetch(global.url + 'UserAccountSettings/GetFontStyle',
  {
      method: "POST",
      headers: {'Content-type':'application/json'},
      body: JSON.stringify(userId)
  }).
  then(r => r.json())
  .then(res=>{
    if(res.fontStyle == "Time-New Roman") {
      require('./Styles/Times.css');
    }
    if(res.fontStyle == "Oxygen") {
      require('./Styles/Oxygen.css');
    }
    if(res.fontStyle == "Helvetica") {
      require('./Styles/Helvetica.css');
    }
  }
  ); 

  fetch(global.url + 'UserAccountSettings/GetTheme',
  {
      method: "POST",
      headers: {'Content-type':'application/json'},
      body: JSON.stringify(userId)
  }).
  then(r => r.json())
  .then(res=>{
    if(res.theme == "Dark") {
      require('./Styles/darkmode.css');
    }
  }
  ); 
}





if (process.env.NODE_ENV === "development") {
  global.url = "http://localhost:5000/";
  global.urlRoute = "http://localhost:3000/";
  console.log("WE Are IN DEV");

}
else{
  console.log("WE Are IN build");

  global.url = "http://52.229.24.12:5000/";
  global.urlRoute = "http://infinimuse.com/";
}
console.log(process.env.NODE_ENV);









ReactDOM.render(
  <ErrorBoundary>
    <AuthnContextProvider>
      <App />
    </AuthnContextProvider>,
  </ErrorBoundary>,
  document.getElementById('root')
);
