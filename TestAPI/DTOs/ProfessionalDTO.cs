using System;
using System.Runtime.Serialization;
using Tnf.Dto;

namespace TestAPI.DTOs
{
    public class ProfessionalDTO : BaseDto
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Phone { get; set; }

        public string Email { get; set; }
    }
}
