import React from 'react';
import { Grid, Header, Loader, Dimmer } from 'semantic-ui-react'

function LoaderRow() {

    return (
        <Grid.Row>
            <Dimmer inverted active>
                <Loader size="medium"><Header.Subheader>Registering</Header.Subheader></Loader>
            </Dimmer>
        </Grid.Row>
        
    )
}

export default LoaderRow;