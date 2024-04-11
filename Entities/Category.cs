using MrBestPizza.Dtos;
using System.ComponentModel.DataAnnotations;

namespace MrBestPizza.Entities
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }

        public ICollection<Pizza> pizzas { get; set; } = new List<Pizza>();
        public Category(string name)
        {
            Name = name;
        }
    }
}
