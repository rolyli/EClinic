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
                Name = patient.Name
            };
        }
    }
}