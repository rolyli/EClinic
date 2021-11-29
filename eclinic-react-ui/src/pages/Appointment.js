import { Container, Form, Dropdown, Table, Button } from "react-bootstrap";
import { useEffect, useState, useCallback } from "react";
import axios from "../axios";

function Appointment() {
  const [doctors, setDoctors] = useState([]);
  const [doctor, setDoctor] = useState(null);
  const [appointments, setAppointments] = useState([]);

  useEffect(() => {
    axios.get("/doctor").then((res) => setDoctors(res.data));
  }, []);

  useEffect(() => {
    if (doctor != null) {
      axios.get(`/appointment?DoctorId=${doctor.id}`).then((res) => {
        if (res.data) {
          /*
          let appointmentList = res.data.map((appointment) => {
            let appointmentTime = Date.parse(appointment.appointmentTime);
            appointment.appointmentTime = appointmentTime.toLocaleString();
            return appointment;
          });
          */

          setAppointments(res.data);
        }
      });
    }
  }, [doctor]);

  return (
    <Container>
      <h1>Make an appointment with a doctor</h1>
      <Form className="mt-5">
        <div className="bg-light m-3 p-3">
          <Form.Group className="mb-3">
            <Form.Label>Date</Form.Label>
            <Form.Control type="date" />
          </Form.Group>

          <Form.Group className="mb-3">
            <Form.Label>Start time</Form.Label>
            <Form.Control type="time" step="600" />
          </Form.Group>

          <Form.Group className="mb-3">
            <Form.Label>Doctor</Form.Label>
            <Dropdown>
              <Dropdown.Toggle variant="success" id="dropdown-basic">
                {doctor !== null ? doctor.name : <>Available doctors</>}
              </Dropdown.Toggle>

              <Dropdown.Menu>
                {doctors.map((doctor) => (
                  <Dropdown.Item
                    onClick={() => setDoctor(doctor)}
                    key={doctor.id}
                  >
                    {doctor.name}
                  </Dropdown.Item>
                ))}
              </Dropdown.Menu>
            </Dropdown>
          </Form.Group>

          <Form.Group>
            <Table striped bordered hover>
              <thead>
                <tr>
                  <th>Appointment Start</th>
                  <th>Appointment End</th>
                  <th>Appointment Duration (Minutes)</th>
                </tr>
              </thead>
              {appointments.map((appointment) => (
                <tbody key={appointment.id}>
                  <tr>
                    <td>{appointment.appointmentTime}</td>
                    <td>{appointment.appointmentEndTime}</td>
                    <td>{appointment.appointmentDurationMins}</td>
                  </tr>
                </tbody>
              ))}
            </Table>
          </Form.Group>
        </div>
        <Button className="m-3" variant="primary" type="submit">
          Submit
        </Button>
      </Form>
    </Container>
  );
}

export default Appointment;
