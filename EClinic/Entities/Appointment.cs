using System;

namespace EClinic.Entities
{
    public class Appointment 
    {
        public Guid Id { get; init; }
        public Guid DoctorId { get; set; }
        public Guid PatientId { get; set; }
        public DateTimeOffset AppointmentTime { get; set; }
        
        //Calculated at runtime
        public DateTimeOffset AppointmentEndTime { get; set; }
        public int AppointmentDurationMins { get; set; }
        public DateTimeOffset CreatedDate { get; init; }
    }
}