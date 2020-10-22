using System;
using System.Diagnostics.CodeAnalysis;

namespace TestAPI.Entities
{
    public class Professional : IEntity
    {
        public Guid Id { get; set; }

        public string Name { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
    }
}
