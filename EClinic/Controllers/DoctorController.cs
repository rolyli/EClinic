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
    [Route("doctor")]

    public class DoctorController : ControllerBase 
    {
        private readonly IDoctorRepository repository;

        public DoctorController(IDoctorRepository repository)
        {
            this.repository = repository;
        }

        [HttpPost]
        public async Task<ActionResult<DoctorDto>> CreateDoctorAsync(CreateDoctorDto doctorDto)
        {
            Doctor doctor = new() {
                Id = Guid.NewGuid(),
                Name = doctorDto.Name,
                CreatedDate = DateTimeOffset.UtcNow
            };

            await repository.CreateDoctorAsync(doctor);

            

            return CreatedAtAction(nameof(GetDoctorAsync), new { id = doctor.Id }, doctor.AsDto());
        }

        [HttpDelete]
        public async Task<ActionResult> DeleteDoctorAsync(Guid id)
        {
            var existingDoctor = await repository.GetDoctorAsync(id);
            if (existingDoctor == null) 
            {
                return NotFound();
            }
            await repository.DeleteDoctorAsync(id);
            return NoContent();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<DoctorDto>> GetDoctorAsync(Guid id)
        {
            var doctor = await repository.GetDoctorAsync(id);

            if (doctor is null)
            {
                return NotFound();
            }

            return doctor.AsDto();
        }

        [HttpGet]
        public async Task<IEnumerable<DoctorDto>> GetDoctorsAsync() 
        {
            var doctors =  (await repository.GetDoctorsAsync()).Select( doctor => doctor.AsDto());
            return doctors;
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateDoctorAsync(Guid id, UpdateDoctorDto updateDoctorDto)
        {
            var existingDoctor = await repository.GetDoctorAsync(id);

            if (existingDoctor is null)
            {
                return NotFound();
            }

            existingDoctor.Name = updateDoctorDto.Name;

            await repository.UpdateDoctorAsync(existingDoctor);

            return NoContent();
        }
    }
}