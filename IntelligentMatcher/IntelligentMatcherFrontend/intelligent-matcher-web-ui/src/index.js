import React from 'react';
import ReactDOM from 'react-dom';
import App from './App';
import './index.css'
import 'semantic-ui-css/semantic.min.css'


global.url = "http://localhost:5000/";
global.urlRoute = "http://localhost:3000/";




fetch(global.url + 'UserAccountSettings/GetFontStyle',
{
    method: "POST",
    headers: {'Content-type':'application/json'},
    body: JSON.stringify("1")
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
    body: JSON.stringify("1")
}).
then(r => r.json())
.then(res=>{
  if(res.theme == "Dark") {
    require('./Styles/darkmode.css');
  }
}
); 






ReactDOM.render(
  <App />,
  document.getElementById('root')
);
