using System;

namespace EClinic.Dtos
{
    public record PatientDto
    {
        public Guid Id { get; init; }
        public string Name { get; set; }
        public DateTimeOffset CreatedDate { get; init; }
    }
}