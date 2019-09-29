using System.ComponentModel.DataAnnotations;
using Kernel.Classes;

namespace Scriptex.EmployeeTask.Data.ViewModels.Employee
{
    public class EmployeePostVM
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public int JobId { get; set; }
        [Required, EmailAddress]
        public string Email { get; set; }
        [Required, RegularExpression(Regex.Mobile)]
        public string Mobile { get; set; }
        [Required, RegularExpression(Regex.NationalId)]
        public string NationalId { get; set; }
        [Required, StringLength(1)]
        public string Gender { get; set; }
        public bool IsActive { get; set; }
    }
}
