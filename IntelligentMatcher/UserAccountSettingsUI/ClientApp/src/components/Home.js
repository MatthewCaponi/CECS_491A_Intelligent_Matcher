import React, { Component } from 'react';
import { DeleteAccount } from './DeleteAccount';
import { ChangePassword } from './ChangePassword';
import { ChangeFontSize } from './ChangeFontSize';
import { ChangeFontStyle } from './ChangeFontStyle';
import { ChangeTheme } from './ChangeTheme';
import { ChangeEmail } from './ChangeEmail';

export class Home extends Component {
  static displayName = Home.name;

  render () {
    return (
      <div>
       <table>
        <td>
          <ChangeFontSize />
          <ChangeFontStyle />
          <ChangeTheme />

        </td>
        <td >
        &nbsp;       
        &nbsp;
        &nbsp;
        &nbsp;
        &nbsp;
        </td>
        <td>
        <ChangeEmail/>
        <ChangePassword />

          <DeleteAccount />
         </td>
        </table>
       </div>
    );
  }
}
