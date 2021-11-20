using System;

namespace EClinic.Dtos
{
    public record PatientDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateTimeOffset CreatedDate { get; init; }
    }
}