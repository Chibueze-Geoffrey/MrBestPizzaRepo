using Microsoft.EntityFrameworkCore;
using MrBestPizza.Entities;
using MrBestPizza.MrBestDbContext;

namespace MrBestPizza.Services
{
    public class PizzaRepository : IPizzaRepository
    {
        private readonly BestDbContext _bestDbContext;

        public PizzaRepository(BestDbContext bestDbContext)
        {
            _bestDbContext = bestDbContext 
                ?? throw new ArgumentNullException(nameof(bestDbContext));
        }
        public async Task<bool> CategoryExistsAsync(int Id)
        {
            return await _bestDbContext.Categories.AnyAsync(c => c.Id == Id);
        }

        public async Task<IEnumerable<Category>> GetCategoriesAsync()
        {
            return await _bestDbContext.Categories.OrderBy(x=>x.Id).ToListAsync();
        }
        public async Task<(IEnumerable<Category>,PaginationMetadata)> GetCategoriesAsync(string? name, 
            string? searchQuery, int pageSize, int pageNumber)
        {
            // collection to start from
            var collection = _bestDbContext.Categories as IQueryable<Category>;
             if(!string.IsNullOrWhiteSpace(name))
             {
                name = name.Trim();
                collection= collection.Where(x=> x.Name == name);
             }
             if(!string.IsNullOrWhiteSpace(searchQuery))
            {
                searchQuery = searchQuery.Trim();
                collection = collection.Where(a => a.Name.Contains(searchQuery)
                || (a.Description !=null && a.Description.Contains(searchQuery)));
            }
            var totalItemCount = await collection.CountAsync();

            var paginationMetaData = new PaginationMetadata(totalItemCount,
                pageSize,pageNumber);

              var categoriesToReturn= await collection 
                .OrderBy(x => x.Name).
                Skip(pageSize * (pageNumber - 1)).
                Take(pageSize)
                .ToListAsync();

            return (categoriesToReturn, paginationMetaData);
        }
        public async Task<Category?> GetCategoryAsync(int categoryId,bool IncludePizza )
        {
            if(IncludePizza)
            {
                return await _bestDbContext.Categories.Include(x => x.pizzas).
                    Where(x => x.Id == categoryId).FirstOrDefaultAsync();
            }
            return await _bestDbContext.Categories.Where(c=>c.Id == categoryId).FirstOrDefaultAsync();  
        }

        public async Task<IEnumerable<Pizza>> GetPizzaAsync(int Id)
        {
            return await _bestDbContext.Pizzas.Where(b=>b.CategoryId == Id).ToListAsync();    
        }

        public async Task<Pizza?> GetPizzaAsync(int Id, int pizzaId)  
        {
            return await _bestDbContext.Pizzas.Where(c=>c.CategoryId==Id && c.PizzaId == pizzaId)
                .FirstOrDefaultAsync();
        }
       public async Task AddPizzaAsync(int Id, Pizza pizza)
        {
            var pizzaAdded = await GetCategoryAsync(Id, false);
            if(pizzaAdded != null)
            {
                pizzaAdded.pizzas.Add(pizza);
            }
          
        }
        public void DeletePizza(Pizza pizza)
        {
            _bestDbContext.Remove(pizza);
        }
        public async Task<bool> SaveChangesAsync()
        {
            return (await _bestDbContext.SaveChangesAsync() >= 0);
        }
    }
}
