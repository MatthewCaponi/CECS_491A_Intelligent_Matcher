  
import React from 'react';
import { Header, Segment, Icon } from 'semantic-ui-react'


function SiteHeader() {
    return (
        <Segment inverted>
            <Header as='header' inverted color='grey'>
                <Icon name='moon' />
                InfiniMuse
            </Header>
        </Segment>
    )
}
export default SiteHeader;