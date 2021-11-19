using System;

namespace EClinic.Entities 
{
    public class Patient 
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateTimeOffset CreatedDate { get; init; }
    }
}