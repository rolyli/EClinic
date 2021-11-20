using System;

namespace EClinic.Dtos
{
    public record CreateAppointmentDto
    {
        public Guid DoctorId { get; set; }
        public Guid PatientId { get; set; }
        public DateTimeOffset AppointmentTime { get; set; }
        public int AppointmentDurationMins { get; set; }

    }
}