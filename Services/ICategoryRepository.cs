using MrBestPizza.Entities;

namespace MrBestPizza.Services
{
    public interface ICategoryRepository
    {
        Task <IEnumerable<Category>> GetCategoriesAsync ();
    }
}
