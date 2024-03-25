using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using MrBestPizza.Dtos;
using MrBestPizza.Entities;
using MrBestPizza.Services;

namespace MrBestPizza.Controllers
{
    [Route("api/Categories/{Id}/pizzas")]
    [Authorize]
    [ApiController]
    public class PizzaController : ControllerBase
    {
        private readonly IPizzaRepository _pizzaRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<PizzaController> _logger;

        public PizzaController(IPizzaRepository pizzaRepository,
            IMapper mapper, ILogger<PizzaController> logger)
        {
            _pizzaRepository = pizzaRepository
                ?? throw new ArgumentNullException(nameof(pizzaRepository));

            _mapper = mapper
                ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger 
                ?? throw new ArgumentNullException(nameof(logger));
        }
        [HttpGet]
        public async Task <ActionResult<IEnumerable<Pizza>>> GetPizzaAsync(int Id)
        {
            if(!await _pizzaRepository.CategoryExistsAsync(Id))
            {
                _logger.LogInformation("Info Not Found");
                return NotFound();
            }
            var GetPizzas = await _pizzaRepository.GetPizzaAsync(Id);
            return Ok(_mapper.Map<IEnumerable<PizzaDto>>(GetPizzas));
        }
        [HttpGet("{pizzaId}", Name="GetPizza")]
        public async Task <ActionResult<PizzaDto>> GetPizza(int Id, int pizzaId)
        {
            if(!await _pizzaRepository.CategoryExistsAsync(Id))
            {
                return NotFound();
            }
            var getArticle =await _pizzaRepository.GetPizzaAsync(Id, pizzaId);
            if (getArticle == null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<PizzaDto>(getArticle));
        }
        [HttpPost]
        public async Task <ActionResult<PizzaDto>> AddPizza(int Id, PizzaCreated pizzaCreated)
        {
          if(!await _pizzaRepository.CategoryExistsAsync(Id))
            {
                return NotFound();
            }
            var pizzaAdded = _mapper.Map<Entities.Pizza>(pizzaCreated);
            await _pizzaRepository.AddPizzaAsync(Id, pizzaAdded);
            await _pizzaRepository.SaveChangesAsync();
            var finalPizza = _mapper.Map<Dtos.PizzaDto>(pizzaAdded);
            return CreatedAtRoute("GetPizza",
                new
                {
                    Id = Id,
                    pizzaId = finalPizza.PizzaId
                },
               finalPizza);
        }
        [HttpPut("{pizzaId}")]
        public async Task <ActionResult>PizzaUpdateAll(int Id,int pizzaId, PizzaUpdatedDto pizzaUpdatedDto)
        {
            if(!await _pizzaRepository.CategoryExistsAsync(Id))
            {
                return NotFound();
            }
            var getPizza = await _pizzaRepository.GetPizzaAsync(Id, pizzaId);
            if (getPizza == null)
            {
                return NotFound();
            }
            _mapper.Map(pizzaUpdatedDto, getPizza);
            await _pizzaRepository.SaveChangesAsync();
            return NoContent();
        }
        [HttpPatch("{pizzaId}")]
        public async Task <ActionResult> PartialPizzaUpdate(int Id, int pizzaId,
            JsonPatchDocument<PizzaUpdatedDto> pizzaPartiallyUpdated)
        {
            if(!await _pizzaRepository.CategoryExistsAsync(Id))
            {
                return NotFound();
            }
            var getPizza = await _pizzaRepository.GetPizzaAsync(Id, pizzaId);
            if (getPizza == null)
            {
                return NotFound();
            }
            var pizzaToPatch = _mapper.Map<PizzaUpdatedDto>(getPizza);
            pizzaPartiallyUpdated.ApplyTo(pizzaToPatch, ModelState);
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if(!TryValidateModel(pizzaToPatch))
            {
                return BadRequest(ModelState);
            }
            _mapper.Map(pizzaToPatch, getPizza);
            await _pizzaRepository.SaveChangesAsync();
            return NoContent();
        }
        [HttpDelete("{pizzaId}")]
        public async Task<ActionResult>DeletePizza(int Id, int pizzaId)
        {
            if(!await _pizzaRepository.CategoryExistsAsync(Id))
            {
                return NotFound();
            }
            var getPizza =await _pizzaRepository.GetPizzaAsync(Id, pizzaId);
            if (getPizza == null)
            {
                return NotFound();
            }
            _pizzaRepository.DeletePizza(getPizza);
            await _pizzaRepository.SaveChangesAsync();
            return NoContent();
        }
    }
}
