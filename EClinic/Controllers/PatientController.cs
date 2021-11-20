using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EClinic.Dtos;
using EClinic.Entities;
using EClinic.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace EClinic.Controllers
{
    [ApiController]
    [Route("patient")]

    public class PatientController : ControllerBase 
    {
        private readonly IPatientRepository repository;

        public PatientController(IPatientRepository repository)
        {
            this.repository = repository;
        }

        [HttpPost]
        public async Task<ActionResult<PatientDto>> CreatePatientAsync(CreatePatientDto patientDto)
        {
            Patient patient = new() {
                Id = Guid.NewGuid(),
                Name = patientDto.Name,
                Username = patientDto.Username,
                Password = patientDto.Password,
                CreatedDate = DateTimeOffset.UtcNow
            };

            await repository.CreatePatientAsync(patient);

            

            return CreatedAtAction(nameof(GetPatientAsync), new { id = patient.Id }, patient.AsDto());
        }

        [HttpDelete]
        public async Task<ActionResult> DeletePatientAsync(Guid id)
        {
            var existingPatient = await repository.GetPatientAsync(id);
            if (existingPatient == null) 
            {
                return NotFound();
            }
            await repository.DeletePatientAsync(id);
            return NoContent();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PatientDto>> GetPatientAsync(Guid id)
        {
            var patient = await repository.GetPatientAsync(id);

            if (patient is null)
            {
                return NotFound();
            }

            return patient.AsDto();
        }

        [HttpGet]
        public async Task<IEnumerable<PatientDto>> GetPatientsAsync() 
        {
            var patients =  (await repository.GetPatientsAsync()).Select( patient => patient.AsDto());
            return patients;
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdatePatientAsync(Guid id, UpdatePatientDto updatePatientDto)
        {
            var existingPatient = await repository.GetPatientAsync(id);

            if (existingPatient is null)
            {
                return NotFound();
            }

            existingPatient.Name = updatePatientDto.Name;

            await repository.UpdatePatientAsync(existingPatient);

            return NoContent();
        }
    }
}