using System.ComponentModel.DataAnnotations;

namespace MrBestPizza.Dtos
{
    public class PizzaUpdatedDto
    {
        [Required(ErrorMessage = "You should provide a value for Name")]
        [MaxLength(35)]
        public string Name { get; set; } = string.Empty;
        [Required(ErrorMessage = "You should provide a value for Description")]
        [MaxLength(500)]
        public string? Description { get; set; }
        [Required(ErrorMessage = "You should provide a value for Price")]
        public decimal Price { get; set; }
    }
}
