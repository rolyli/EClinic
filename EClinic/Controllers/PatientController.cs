using System;
using System.Collections.Generic;
using System.Linq;
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
        public void CreatePatient(CreatePatientDto patientDto)
        {
            Patient patient = new() {
                Id = Guid.NewGuid(),
                Name = patientDto.Name,
                CreatedDate = DateTimeOffset.UtcNow
            };

            repository.CreatePatient(patient);
        }

        [HttpGet]
        public IEnumerable<PatientDto> GetPatients() 
        {
            var patients =  repository.GetPatients().Select( patient => patient.AsDto());
            return patients;
        }
    }
}