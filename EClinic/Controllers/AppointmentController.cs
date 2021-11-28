using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EClinic.Dtos;
using EClinic.Entities;
using EClinic.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace EClinic.Controllers
{
    [ApiController]
    [Route("appointment")]

    public class AppointmentController : ControllerBase 
    {
        private readonly IAppointmentRepository repository;
        private readonly ILogger<AppointmentController> logger;

        public AppointmentController(IAppointmentRepository repository, ILogger<AppointmentController> logger)
        {
            this.repository = repository;
            this.logger = logger;
        }

        [HttpPost]
        public async Task<ActionResult<AppointmentDto>> CreateAppointmentAsync(CreateAppointmentDto appointmentDto)
        {
            // Business logic where it checks whether the timeslot is available
            var existingAppointments = await GetAppointmentsAsync(appointmentDto.DoctorId.ToString());
            var endTime = appointmentDto.AppointmentTime.AddMinutes(appointmentDto.AppointmentDurationMins);
            Console.WriteLine(endTime);
                        Console.WriteLine(appointmentDto.AppointmentDurationMins);

            var dateClash = false;

            foreach (AppointmentDto item in existingAppointments)
            {
                if ((appointmentDto.AppointmentTime >= item.AppointmentTime) && (appointmentDto.AppointmentTime <= item.AppointmentEndTime))
                {
                    // Start time is within existing appointment's duration
                    dateClash = true;
                }

                if ((endTime >= item.AppointmentTime) && (endTime <= item.AppointmentEndTime))
                {
                    // End time is within existing appointment's duration
                    dateClash = true;
                }

                if ((appointmentDto.AppointmentTime <= item.AppointmentTime) && (endTime >= item.AppointmentEndTime))
                {
                    // Start and end time encapsulates existing appointments duration
                    dateClash = true;
                }
            }

            if (dateClash == true)
            {
                return BadRequest();
            }

            Appointment appointment = new() {
                Id = Guid.NewGuid(),
                DoctorId = appointmentDto.DoctorId,
                PatientId = appointmentDto.PatientId,
                AppointmentTime = appointmentDto.AppointmentTime,
                AppointmentEndTime = endTime,
                AppointmentDurationMins = appointmentDto.AppointmentDurationMins,
                CreatedDate = DateTimeOffset.UtcNow
            };

            await repository.CreateAppointmentAsync(appointment);

            return CreatedAtAction(nameof(GetAppointmentAsync), new { id = appointment.Id }, appointment.AsDto());
        }

        [HttpDelete]
        public async Task<ActionResult> DeleteAppointmentAsync(Guid id)
        {
            var existingAppointment = await repository.GetAppointmentAsync(id);
            if (existingAppointment == null) 
            {
                return NotFound();
            }
            await repository.DeleteAppointmentAsync(id);
            return NoContent();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<AppointmentDto>> GetAppointmentAsync(Guid id)
        {
            var appointment = await repository.GetAppointmentAsync(id);

            if (appointment is null)
            {
                return NotFound();
            }

            return appointment.AsDto();
        }

        [HttpGet]
        public async Task<IEnumerable<AppointmentDto>> GetAppointmentsAsync(string DoctorId = null, string PatientId = null) 
        {
            // Get by DoctorId
            if (!string.IsNullOrWhiteSpace(DoctorId)) 
            {
                return (await repository.GetAppointmentsByDoctorIdAsync(Guid.Parse(DoctorId))).Select( appointment => appointment.AsDto());
            }

            // Get by PatientId
            if (!string.IsNullOrWhiteSpace(PatientId)) 
            {
                return (await repository.GetAppointmentsByPatientIdAsync(Guid.Parse(PatientId))).Select( appointment => appointment.AsDto());
            }

            // Get all
            var appointments =  (await repository.GetAppointmentsAsync()).Select( appointment => appointment.AsDto());

            logger.LogInformation($"{DateTime.UtcNow.ToString("hh:mm:ss")}: Retrieved {appointments.Count()} appointments");            
            return appointments;
        }

        /*
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateAppointmentAsync(Guid id, UpdateAppointmentDto updateAppointmentDto)
        {
            var existingAppointment = await repository.GetAppointmentAsync(id);

            if (existingAppointment is null)
            {
                return NotFound();
            }

            existingAppointment.Name = updateAppointmentDto.Name;

            await repository.UpdateAppointmentAsync(existingAppointment);

            return NoContent();
        }
        */
    }
}