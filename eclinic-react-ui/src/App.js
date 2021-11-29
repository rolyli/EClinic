import logo from "./logo.svg";
import "./App.css";
import axios from "./axios";
import { useEffect, useState } from "react";

// Bootstrap
import { BrowserRouter as Router, Routes, Route, Link } from "react-router-dom";
import { Container, Row, Col, Button, Form, Card } from "react-bootstrap";

// Components
import Navbar from "./components/navbar";

// Pages
import Login from "./pages/Login";
import Appointment from "./pages/Appointment";


function App() {
  const [user, setUser] = useState(null);

  useEffect(() => {
    setUser(localStorage.getItem("user"));
  }, []);

  return (
    <div className="App">
      <Navbar />
      <Router>
        <Routes>
          <Route path="/" element={<Home user={user} />}></Route>
          <Route path="/login" element={<Login setUser={setUser} />}></Route>
          <Route path="/appointment" element={<Appointment />}></Route>
        </Routes>
      </Router>
    </div>
  );
}

function Home({ user }) {
  return (
    <Container>
      <h1>Welcome to EClinic</h1>
      <h2>Online clinic management system built for CSC3131</h2>
      {user === null ? (
        <Link to="/login">
          <Button className="mt-2">Login</Button>
        </Link>
      ) : (
        <Container className="d-flex">
          <Card style={{ width: "18rem" }} className="m-3">
            <Card.Body>
              <Card.Title>Appointments</Card.Title>
              <Card.Text>
                Request an appointment with a doctor at a given date
              </Card.Text>
              <Link to="/appointment">
                <Button variant="primary">Go</Button>
              </Link>
            </Card.Body>
          </Card>

          <Card style={{ width: "18rem" }} className="m-3">
            <Card.Body>
              <Card.Title>Messages</Card.Title>
              <Card.Text>
                Send a message to the clinic to be directed to the appropriate
                clinician
              </Card.Text>
              <Link to="/message">
                <Button variant="primary">Go</Button>
              </Link>
            </Card.Body>
          </Card>
        </Container>
      )}
    </Container>
  );
}

export default App;
