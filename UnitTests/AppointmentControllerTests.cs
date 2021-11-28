using System;
using System.Collections.Generic;
using EClinic.Controllers;
using EClinic.Dtos;
using EClinic.Entities;
using EClinic.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;
using Xunit.Abstractions;

namespace UnitTests
{
    public class AppointmentControllerTests 
    {
        private readonly Mock<IAppointmentRepository> repositoryStub = new();
        private readonly Mock<ILogger<AppointmentController>> loggerStub = new();
        private readonly Random rand = new();
        private readonly ITestOutputHelper output;

        public AppointmentControllerTests(ITestOutputHelper output)
        {
            this.output = output;
        }

        /*
        [Fact]
        public async void CreateAppointmentAsync_WithStartTimeOverlap_ReturnsBadRequest()
        {
            var existingAppointment = CreateAppointment(30, DateTimeOffset.UtcNow);
            var existingAppointments = new [] {existingAppointment};

            repositoryStub.Setup(repo => repo.GetAppointmentsAsync()).ReturnsAsync(existingAppointments);

            var controller = new AppointmentController(repositoryStub.Object, loggerStub.Object);
            
            // Same start time with increased duration
            var newAppointment = new CreateAppointmentDto()
            {
                DoctorId = existingAppointment.DoctorId,
                PatientId = Guid.NewGuid(),
                AppointmentDurationMins = 35,
                AppointmentTime = existingAppointment.AppointmentTime
            };

            var result2 = await controller.GetAppointmentsAsync(existingAppointment.DoctorId.ToString());
            var result = await controller.CreateAppointmentAsync(newAppointment);
        }
        */

        [Fact]
        public async void GetAppointmentAsync_WithUnexistingItem_ReturnsNotFound()
        {
            repositoryStub.Setup(repo => repo.GetAppointmentAsync(It.IsAny<Guid>()))
                .ReturnsAsync((Appointment)null);
            var controller = new AppointmentController(repositoryStub.Object, loggerStub.Object);

            var result = await controller.GetAppointmentAsync(Guid.NewGuid());

            Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public async void GetAppointmentAsync_WithExistingAppointment_ReturnsExpectedAppointment()
        {
            var expectedAppointment = CreateRandomAppointment();
            repositoryStub.Setup(repo => repo.GetAppointmentAsync(It.IsAny<Guid>()))
                .ReturnsAsync(expectedAppointment);
            
            var controller = new AppointmentController(repositoryStub.Object, loggerStub.Object);
            var result = await controller.GetAppointmentAsync(Guid.NewGuid());

            Assert.IsType<AppointmentDto>(result.Value);
        }

        private Appointment CreateAppointment(int appointmentDurationMins, DateTimeOffset appointmentStartTime)
        {
            return new()
            {
                Id = Guid.NewGuid(),
                AppointmentDurationMins = appointmentDurationMins,
                AppointmentTime = appointmentStartTime,
                AppointmentEndTime = appointmentStartTime.AddMinutes(appointmentDurationMins),
                DoctorId = Guid.NewGuid(),
                PatientId = Guid.NewGuid()
            };
        }

        private Appointment CreateRandomAppointment()
        {
            return new()
            {
                Id = Guid.NewGuid(),
                AppointmentDurationMins = rand.Next(30),
                AppointmentTime = DateTimeOffset.UtcNow,
                DoctorId = Guid.NewGuid(),
                PatientId = Guid.NewGuid()
            };
        }
    }
}
