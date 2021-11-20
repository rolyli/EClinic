using System;

namespace EClinic.Dtos
{
    public record LoginPatientDto
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}