using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EClinic.Entities;

namespace EClinic.Repositories
{
    public interface IPatientRepository 
    {
        Task CreatePatientAsync(Patient patient);
        Task<IEnumerable<Patient>> GetPatientsAsync();
        Task<Patient> GetPatientAsync(Guid id);
        Task UpdatePatientAsync(Patient patient);
        Task DeletePatientAsync(Guid id);
        
        
    }
}