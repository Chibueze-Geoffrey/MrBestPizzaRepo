using Microsoft.EntityFrameworkCore;
using MrBestPizza.Entities;
using MrBestPizza.MrBestDbContext;

namespace MrBestPizza.Services
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly BestDbContext _bestDbContext;

        public CategoryRepository(BestDbContext bestDbContext)
        {
            _bestDbContext = bestDbContext
                ?? throw new ArgumentNullException(nameof(bestDbContext));
        }
        public async Task<IEnumerable<Category>> GetCategoriesAsync()
        {
            return await _bestDbContext.Categories.ToListAsync();
        }
    }
}
