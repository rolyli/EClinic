using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EClinic.Entities;

namespace EClinic.Repositories
{
    public interface IDoctorRepository 
    {
        Task CreateDoctorAsync(Doctor doctor);
        Task<IEnumerable<Doctor>> GetDoctorsAsync();
        Task<Doctor> GetDoctorAsync(Guid id);
        Task UpdateDoctorAsync(Doctor doctor);
        Task DeleteDoctorAsync(Guid id);
        
        
    }
}