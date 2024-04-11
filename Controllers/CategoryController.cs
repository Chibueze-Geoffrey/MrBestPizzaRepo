using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MrBestPizza.Dtos;
using MrBestPizza.Entities;
using MrBestPizza.Services;
using System.Text.Json;

namespace MrBestPizza.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/Categories")]
    public class CategoryController : Controller
    {
        private readonly IPizzaRepository _pizzaRepository;
        private readonly IMapper _mapper;
        const int maxPage = 20;

        public CategoryController(IPizzaRepository categoryRepository,
            IMapper mapper)
        {
            _pizzaRepository = categoryRepository
                ?? throw new ArgumentNullException(nameof(categoryRepository));
         
            _mapper = mapper 
                ?? throw new ArgumentNullException(nameof(mapper));
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Category>>> GetCategoriesAsync(string? name, 
            string? searchQuery, int pageNumber = 1, int pageSize = 10)
        {
            if(pageSize> maxPage)
            {
                pageSize = maxPage;
            }
           var (GetCategories,paginationMetaData) = await _pizzaRepository.GetCategoriesAsync(name , 
               searchQuery,pageNumber,pageSize);

            Response.Headers.Append("X-Pagination",
                JsonSerializer.Serialize(paginationMetaData));
            return Ok(_mapper.Map<IEnumerable<CategoryWithoutPizzaDto>>(GetCategories));
        }
        [HttpGet("{Id}")]
        public async Task<IActionResult> GetCategoryAsync(int Id, bool IncludePizza = false)
        {
            var GetCategory = await _pizzaRepository.GetCategoryAsync(Id, IncludePizza);
            if (GetCategory == null)
            {
                return NotFound();
            }
            if (IncludePizza)
            {
                return Ok(_mapper.Map<CategoryDto>(GetCategory));
            }
            return Ok(_mapper.Map<CategoryWithoutPizzaDto>(GetCategory));
        }
    }
}
