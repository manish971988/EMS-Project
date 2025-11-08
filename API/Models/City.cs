namespace EMS.API.Models
{
    public class City
    {
        public int Id { get; set; }
        [System.ComponentModel.DataAnnotations.MaxLength(255)]
        public string Name { get; set; }
    }
}
