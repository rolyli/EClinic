using System.Collections.Generic;
using EClinic.Entities;

namespace EClinic.Repositories
{
    public interface IPatientRepository 
    {
        void CreatePatient(Patient patient);
        IEnumerable<Patient> GetPatients();
    }
}