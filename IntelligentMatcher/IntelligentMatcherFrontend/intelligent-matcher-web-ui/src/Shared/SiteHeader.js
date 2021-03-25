import React from 'react';
import { AppBar, Icon, Toolbar, Typography, Grid } from "@material-ui/core";
import { makeStyles, createStyles } from "@material-ui/core/styles";
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { faMoon } from '@fortawesome/free-solid-svg-icons';

const useStyles = makeStyles(() => ({
    typographyStyles: {
      flex: 0,
      fontSize: 20
    },
    spacingStyles: {
        marginLeft: 5,
        marginRight: 5,
        size: 200

    }
  }));

function SiteHeader() {
    const classes = useStyles();
    return (
        <AppBar position="static">
            <Toolbar>
                <FontAwesomeIcon icon={faMoon} className={classes.spacingStyles}/>
                <Typography className={classes.typographyStyles}>
                    InfiniMuse
                </Typography>        
            </Toolbar>
        </AppBar>
        
        

  
    )
}
export default SiteHeader;