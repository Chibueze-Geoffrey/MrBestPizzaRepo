using AutoMapper;

namespace MrBestPizza.Profiles
{
    public class CategoryProfile : Profile
    {
        public CategoryProfile()
        {
            CreateMap<Entities.Category,Dtos.CategoryWithoutPizzaDto>();
            CreateMap<Entities.Category, Dtos.CategoryDto>();
        }
    }
}
