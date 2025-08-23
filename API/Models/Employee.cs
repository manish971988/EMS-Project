using System;

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
    [System.ComponentModel.DataAnnotations.MaxLength(255)]
    public string Name { get; set; }
    [System.ComponentModel.DataAnnotations.MaxLength(255)]
    public string Department { get; set; }
    [System.ComponentModel.DataAnnotations.MaxLength(255)]
    public string Designation { get; set; }
    public decimal Salary { get; set; }
    [System.ComponentModel.DataAnnotations.MaxLength(255)]
    public string Address { get; set; }
    public EmployeeStatus Status { get; set; }
    public int CreatedBy { get; set; }
    public User CreatedByUser { get; set; }
    public int UpdatedBy { get; set; }
    public User UpdatedByUser { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    }
}
