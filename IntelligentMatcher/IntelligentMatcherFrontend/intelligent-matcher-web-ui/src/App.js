import UserManagement from "./Features/CoreFeatures/UserManagement/Pages/UserManagement";
import { BrowserRouter as Router, Route, Switch } from 'react-router-dom'
import { Grid, Container } from 'semantic-ui-react'
import SiteHeader from './Shared/SiteHeader';

function App() {
  return (
    <div className="App">
      <SiteHeader />
      <Container >
        <Router>
          <Switch>
                <Route path="/UserManagement">
                  <UserManagement />
                </Route>
              </Switch>
        </Router>
      </Container>
    </div>
  );
}

export default App;