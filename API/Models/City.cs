using System.ComponentModel.DataAnnotations;

namespace EMS.API.Models
{
    public class City
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(255)]
        public string Name { get; set; } = string.Empty;
    }
}
