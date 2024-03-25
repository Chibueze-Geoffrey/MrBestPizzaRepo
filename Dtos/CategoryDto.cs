namespace MrBestPizza.Dtos
{
    public class CategoryDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public ICollection<PizzaDto> pizzas { get; set; } = new List<PizzaDto>();
      public int NumberOfPizza
        {
            get
            {
                return pizzas.Count;
            }
        }
    }
}
