import React, { Component } from 'react';
import { MessageBox } from './MessageBox';
import { MessageBox2 } from './MessageBox2';


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
