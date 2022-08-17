using System;
using System.ComponentModel.DataAnnotations;

namespace Csi.Ems.Api.Models
{
    public class EmployeeModel
    {
        public Guid Id { get; set; }
        [Required]
        [MaxLength(128)]
        public string Name { get; set; }
        [Required]
        [EmailAddress]
        [MaxLength(128)]
        public string Email { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string Sex { get; set; }
        public string Designation { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
    }
}
