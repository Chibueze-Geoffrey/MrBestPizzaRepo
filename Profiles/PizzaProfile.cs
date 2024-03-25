using AutoMapper;

namespace MrBestPizza.Profiles
{
    public class PizzaProfile : Profile
    {
        public PizzaProfile()
        {
            CreateMap<Entities.Category, Dtos.PizzaDto>();
            CreateMap<Entities.Pizza, Dtos.PizzaDto>();
            CreateMap< Dtos.PizzaCreated,Entities.Pizza>();
            CreateMap<Dtos.PizzaUpdatedDto,Entities.Pizza>();
            CreateMap<Entities.Pizza, Dtos.PizzaUpdatedDto>();
        }
    }
}
