using System.ComponentModel.DataAnnotations;

namespace EMS.API.Models
{
    public class EmpDepartment
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(255)]
        public string DepartmentName { get; set; } = string.Empty;
        [MaxLength(500)]
        public string? DepartmentDesc { get; set; }
        [Required]
        [MaxLength(50)]
        public string DepartmentCode { get; set; } = string.Empty;
    }
}
