using System;

namespace EClinic.Dtos
{
    public record DoctorDto
    {
        public Guid Id { get; init; }
        public string Name { get; set; }
        public DateTimeOffset CreatedDate { get; init; }
    }
}