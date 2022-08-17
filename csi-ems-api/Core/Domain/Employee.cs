using System;

namespace Csi.Ems.Api.Core.Domain
{
    public class Employee : IEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string Sex { get; set; }
        public string Designation { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
    }

}
