using System;

namespace EMS.API.Models
{
    public class User1
    {
        public int Id { get; set; }
        [System.ComponentModel.DataAnnotations.MaxLength(255)]
        public string Name { get; set; }
        [System.ComponentModel.DataAnnotations.MaxLength(255)]
        public string Email { get; set; }
        [System.ComponentModel.DataAnnotations.MaxLength(255)]
        public string PasswordHash { get; set; }
        public int RoleId { get; set; }
        public Role Role { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
