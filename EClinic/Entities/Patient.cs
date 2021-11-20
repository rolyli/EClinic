using System;

namespace EClinic.Entities 
{
    public class Patient 
    {
        public Guid Id { get; init; }
        public string Name { get; set; }
        public DateTimeOffset CreatedDate { get; init; }
    }
}