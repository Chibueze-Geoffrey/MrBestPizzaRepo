using MrBestPizza.Entities;

namespace MrBestPizza.Services
{
    public interface IPizzaRepository
    {
        Task<IEnumerable<Category>> GetCategoriesAsync();
        Task<(IEnumerable<Category>,PaginationMetadata)> GetCategoriesAsync(string? name,string? searchQuery,
            int pageSize, int pageNumber);
        Task <Category?> GetCategoryAsync(int Id, bool IncludePizza);
        Task<IEnumerable<Pizza>> GetPizzaAsync(int Id);
        Task <Pizza?> GetPizzaAsync( int Id, int pizzaId);
        Task <bool> CategoryExistsAsync(int Id);
        Task AddPizzaAsync(int Id,Pizza pizza);
        void DeletePizza (Pizza pizza);
        Task<bool> SaveChangesAsync();
      // Task<Category> GetCategoryAsync(string pizzaId, int categoryId);
    }
}
