using System.ComponentModel.DataAnnotations;

namespace Technocitt.Database.Tables
{
    public class CustomProperty
    {
        [Key]
        public int ID { get; set; }
        [Required]
        public string Name { get; set; }
    }
}
