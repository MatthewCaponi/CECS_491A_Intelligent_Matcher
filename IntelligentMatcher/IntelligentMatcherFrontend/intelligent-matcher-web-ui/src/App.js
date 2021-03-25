import UserManagement from "./Features/CoreFeatures/UserManagement/Pages/UserManagement";
import 'bootstrap/dist/css/bootstrap.min.css';
import SiteHeader from './Shared/SiteHeader';
import { Grid } from "@material-ui/core";
import { ThemeProvider } from "@material-ui/core/styles";
import theme from './Shared/Theme';
import { makeStyles } from "@material-ui/core/styles";
import SiteFooter from './Shared/SiteFooter';

const useStyles = makeStyles({
  layout: {
    position: 'fixed',
    bottom: 0,
    left: 0,
    right: 0,
    width: '100%'
  }
});

function App() {
  const classes = useStyles();
  return (
    <div className="App">
      <ThemeProvider theme={theme}>
      <Grid container direction="column" spacing={4}>
          <Grid item>
            <SiteHeader />
          </Grid>
          <Grid item/>
          <Grid item container>
            <Grid item sm={2} />
            <Grid item sm={8}>
              <UserManagement />
            </Grid>
            <Grid item sm={2} />
          </Grid> 
        </Grid>
        <Grid container className={classes.layout}>
            <SiteFooter/>
          </Grid>
    </ThemeProvider>
      
    </div>
  );
}

export default App;
