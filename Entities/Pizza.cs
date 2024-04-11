using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MrBestPizza.Entities
{
    public class Pizza
    {
        [Key]
        public int PizzaId { get; set; }
        [Required]
        [MaxLength(30)] 
        public string Name { get; set; } = string.Empty;
        [Required]
        [MaxLength(150)]
        public string? Description { get; set;}
        [Required]
        public decimal Price { get; set;}
        [ForeignKey("CategoryId")]
        public int CategoryId { get; set;}
        public Category category { get; set; } = default!;


    }
}
