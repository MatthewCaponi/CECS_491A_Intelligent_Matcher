import UserManagement from "./Features/CoreFeatures/UserManagement/Pages/UserManagement";
import { Grid, Container } from 'semantic-ui-react'
import SiteHeader from './Shared/SiteHeader';

function App() {
  return (
    <div className="App">
      <SiteHeader />
      <Container >
        <UserManagement />
      </Container>
    </div>
  );
}

export default App;