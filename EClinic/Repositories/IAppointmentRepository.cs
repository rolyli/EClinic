using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EClinic.Entities;

namespace EClinic.Repositories
{
    public interface IAppointmentRepository
    {
        Task CreateAppointmentAsync(Appointment appointment);
        Task<IEnumerable<Appointment>> GetAppointmentsAsync();
        Task<IEnumerable<Appointment>> GetAppointmentsByDoctorIdAsync(Guid id);

        Task<Appointment> GetAppointmentAsync(Guid id);
        
        Task UpdateAppointmentAsync(Appointment appointment);
        Task DeleteAppointmentAsync(Guid id);
    }
}