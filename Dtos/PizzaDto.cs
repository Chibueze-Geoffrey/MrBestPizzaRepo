namespace MrBestPizza.Dtos
{
    public class PizzaDto
    {
       
        public int PizzaId { get; set; }
       
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public decimal Price { get; set; }
       
    }
}
