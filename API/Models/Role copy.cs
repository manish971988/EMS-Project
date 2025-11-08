namespace EMS.API.Models
{
    public class Role1
    {
        public int Id { get; set; }
        [System.ComponentModel.DataAnnotations.MaxLength(255)]
        public string Name { get; set; } // Admin | Supervisor
    }
}
