import UserManagement from "./Features/CoreFeatures/UserManagement/Pages/UserManagement";
import 'bootstrap/dist/css/bootstrap.min.css';
import Container from 'react-bootstrap/Container';
import SiteHeader from './Shared/SiteHeader';

function App() {
  return (
    <div className="App">
      <Container >
        <SiteHeader />
        <UserManagement />
      </Container>
    </div>
  );
}

export default App;
