using System;

namespace EClinic.Dtos
{
    public record CreatePatientDto
    {
        public string Name { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }
}