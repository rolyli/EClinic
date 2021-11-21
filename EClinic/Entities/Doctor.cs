using System;

namespace EClinic.Entities 
{
    public class Doctor 
    {
        public Guid Id { get; init; }
        public string Name { get; set; }
        public DateTimeOffset CreatedDate { get; init; }
        public string Username { get; set; }
        public string Password { get; set; }
    }
}