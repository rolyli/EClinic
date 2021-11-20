using System;

namespace EClinic.Dtos
{
    public record UpdateAppointmentDto
    {
        public Guid DoctorId { get; set; }
        public Guid PatientId { get; set; }
        public DateTimeOffset AppointmentTime { get; set; }    }
}