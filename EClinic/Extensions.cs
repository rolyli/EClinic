using EClinic.Dtos;
using EClinic.Entities;

namespace EClinic
{
    public static class Extensions
    {
        public static PatientDto AsDto (this Patient patient)
        {
            return new PatientDto
            {
                Id = patient.Id,
                Name = patient.Name,
                CreatedDate = patient.CreatedDate
            };
        }

        public static DoctorDto AsDto (this Doctor doctor)
        {
            return new DoctorDto
            {
                Id = doctor.Id,
                Name = doctor.Name,
                CreatedDate = doctor.CreatedDate
            };
        }

        public static AppointmentDto AsDto (this Appointment appointment)
        {
            return new AppointmentDto
            {
                Id = appointment.Id,
                DoctorId = appointment.DoctorId,
                PatientId = appointment.PatientId,
                AppointmentTime = appointment.AppointmentTime,
                AppointmentDurationMins = appointment.AppointmentDurationMins,
                AppointmentEndTime = appointment.AppointmentEndTime,
                CreatedDate = appointment.CreatedDate
            };
        }
    
    }
}