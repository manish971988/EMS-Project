using System;
using System.ComponentModel.DataAnnotations;

namespace EMS.API.Models
{
    public enum EmployeeStatus
    {
        Active,
        Inactive,
        Deleted
    }

    public class Employee
    {
    public int Id { get; set; }
    [Required]
    [MaxLength(255)]
    public string Name { get; set; } = string.Empty;
    [Required]
    [MaxLength(255)]
    public string Department { get; set; } = string.Empty;
    [Required]
    [MaxLength(255)]
    public string Designation { get; set; } = string.Empty;
    [Required]
    [Range(1, double.MaxValue, ErrorMessage = "Salary must be positive")]
    public decimal Salary { get; set; }
    [Required]
    [MaxLength(255)]
    public string Address { get; set; } = string.Empty;
    [Required]
    public EmployeeStatus Status { get; set; }
    public int CreatedBy { get; set; }
    public User? CreatedByUser { get; set; }
    public int? UpdatedBy { get; set; }
    public User? UpdatedByUser { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    }
}
