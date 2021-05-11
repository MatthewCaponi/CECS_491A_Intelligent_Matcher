import React, {createRef, useState} from 'react'

import { Grid,Statistic } from 'semantic-ui-react'


function AnalysisDashboard(props) {
    return (
          <div>
           <Statistic.Group>
              <Statistic color = 'green'>
                <Statistic.Value>12452</Statistic.Value>
                <Statistic.Label>Total Registered Accounts</Statistic.Label>
              </Statistic>
              <Statistic color = 'green'>
                <Statistic.Value>0</Statistic.Value>
                <Statistic.Label>Total Registered Accounts Today</Statistic.Label>
              </Statistic>
              <Statistic color = 'green'>
                <Statistic.Value>0</Statistic.Value>
                <Statistic.Label>Total Registered Accounts in the past Month</Statistic.Label>
              </Statistic>
              <Statistic color = 'green'>
                <Statistic.Value>0</Statistic.Value>
                <Statistic.Label>Total Registered Accounts in the past Year</Statistic.Label>
              </Statistic>
            </Statistic.Group>
           <br/>
           <Statistic.Group>
              <Statistic color = 'orange'>
                <Statistic.Value>0</Statistic.Value>
                <Statistic.Label>Total Accounts Suspended</Statistic.Label>
              </Statistic>
              <Statistic color= 'red'>
                <Statistic.Value>0</Statistic.Value>
                <Statistic.Label>Total Accounts Banned</Statistic.Label>
              </Statistic>
              <Statistic>
                <Statistic.Value>0</Statistic.Value>
                <Statistic.Label>Total Accounts Shadow-Banned</Statistic.Label>
              </Statistic>
            </Statistic.Group>
            </div>
          
          
        )
        
       
      
    

}


export default AnalysisDashboard;