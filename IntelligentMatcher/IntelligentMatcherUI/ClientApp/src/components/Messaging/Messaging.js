import React, { Component } from 'react';
import { MessageBox } from './MessageBox';


export class Messaging extends Component {
  static displayName = Messaging.name;

  render () {
    return (
      <div>
          <MessageBox />

       </div>
    );
  }
}
