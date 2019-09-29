using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System;

namespace Scriptex.EmployeeTask.Data.Models
{
    [Table("Employees", Schema = "EmployeeTask")]
    public class Employee
    {
        public int Id { get; set; }

        [StringLength(70), Required]
        public string Name { get; set; }

        [Required]
        public int JobId { get; set; }

        [StringLength(254), Required]
        public string Email { get; set; }

        [Required]
        public string Mobile { get; set; }

        [Required]
        public string NationalId { get; set; }

        [Column(TypeName = "Char"), StringLength(1), Required]
        public string Gender { get; set; }

        [Column(TypeName = "Date"), Required]
        public DateTime CreationDate {get; set;}

        public bool IsActive { get; set; }

        public Job Job { get; set; }
    }
}
