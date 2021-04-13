import React, { Component } from 'react';
import { DeleteAccount } from '../Components/DeleteAccount';
import { ChangePassword } from '../Components/ChangePassword';
import { ChangeFontSize } from '../Components/ChangeFontSize';
import { ChangeFontStyle } from '../Components/ChangeFontStyle';
import { ChangeTheme } from '../Components/ChangeTheme';
import { ChangeEmail } from '../Components/ChangeEmail';
import { Table, Grid } from 'semantic-ui-react'

export class UserAccountSettings extends Component {
  static displayName = UserAccountSettings.name;

  render () {
    return (
      <Grid columns={2} divided width={10} >
       <Grid.Column  >

          <ChangeFontSize />
          <ChangeFontStyle />
          <ChangeTheme />

        </Grid.Column>

      <Grid.Column  >
        <ChangeEmail/>
        <ChangePassword />

          <DeleteAccount />
         </Grid.Column>
        </Grid>
    );
  }
}
export default UserAccountSettings;