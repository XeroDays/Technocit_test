using System.ComponentModel.DataAnnotations;

namespace Technocitt.Models
{
    public class MyProperty
    {
        public int ID { get; set; }
        [Required]
        public required string Name { get; set; }
    }
}
