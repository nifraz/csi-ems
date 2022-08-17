using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

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
        public string Phone { get; set; }
        public string Address { get; set; }
    }
}
