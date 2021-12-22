import React from "react";
import Navbar from "react-bootstrap/Navbar";
import Container from "react-bootstrap/Container";
import Nav from "react-bootstrap/Nav";
import { BrowserRouter as Router, Routes, Route, Link } from "react-router-dom";

const NavigationBar = () => {
  return (
      <Navbar bg="light" expand="lg" className="mb-5">
        <Container>
          <Navbar.Brand>EClinic</Navbar.Brand>
          <Navbar.Toggle aria-controls="basic-navbar-nav" />
          <Navbar.Collapse id="basic-navbar-nav">
            <Nav className="me-auto">
              <Nav.Link href="/">
                Home
              </Nav.Link>
              <Nav.Link href="/appointment">Appointment</Nav.Link>
              <Nav.Link href="#link">Messages</Nav.Link>
            </Nav>
          </Navbar.Collapse>
        </Container>
      </Navbar>
  );
};

export default NavigationBar;
