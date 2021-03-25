import React from 'react';
import { AppBar, Icon, Toolbar, Typography, Grid, Container } from "@material-ui/core";
import { makeStyles, createStyles } from "@material-ui/core/styles";

const useStyles = makeStyles(() => ({
    typographyStyles: {
      fontSize: 13
    }
  }));

function SiteFooter() {
    const classes = useStyles();
    return (
        <AppBar position="static">
            <Container maxWidth="xl">
                <Toolbar>
                        <Typography className={classes.typographyStyles}>
                        InfiniMuse - 2021 ©
                        </Typography>        
                    </Toolbar>
            </Container>
        </AppBar>
        
        

  
    )
}
export default SiteFooter;