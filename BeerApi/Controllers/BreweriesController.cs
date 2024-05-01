using BeerApi.DTOs;
using BeerApi.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace BeerApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BreweriesController : ControllerBase
    {
        private readonly IBreweryService _breweryService;

        public BreweriesController(IBreweryService breweryService)
        {
            _breweryService = breweryService;
        }

        [HttpPost]
        public async Task<ActionResult<BreweryDTO>> PostBrewery(BreweryDTO breweryDTO)
        {
            var createdBrewery = await _breweryService.AddBrewery(breweryDTO);
            return CreatedAtAction(nameof(GetBreweryById), new { id = createdBrewery.BreweryId }, createdBrewery);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutBrewery(int id, BreweryDTO breweryDTO)
        {
            await _breweryService.UpdateBrewery(id, breweryDTO);
            return NoContent();
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<BreweryDTO>>> GetBreweries()
        {
            var breweries = await _breweryService.GetBreweries();
            return Ok(breweries);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<BreweryDTO>> GetBreweryById(int id)
        {
            var brewery = await _breweryService.GetBreweryById(id);
            if (brewery == null)
            {
                return NotFound();
            }
            return Ok(brewery);
        }

        [HttpPost("beer")]
        public async Task<ActionResult<BreweryBeerDTO>> AddBreweryBeerLink(BreweryBeerDTO breweryBeerDTO)
        {
            var createdBreweryBeerLink = await _breweryService.AddBreweryBeerLink(breweryBeerDTO);
            return CreatedAtAction(nameof(GetBreweryByIdWithBeers), new { breweryId = createdBreweryBeerLink.BreweryId }, createdBreweryBeerLink);
        }

        [HttpGet("{breweryId}/beer")]
        public async Task<ActionResult<BreweryDTO>> GetBreweryByIdWithBeers(int breweryId)
        {
            var breweryWithBeers = await _breweryService.GetBreweryByIdWithBeers(breweryId);
            if (breweryWithBeers == null)
            {
                return NotFound();
            }
            return Ok(breweryWithBeers);
        }

        [HttpGet("beer")]
        public async Task<ActionResult<IEnumerable<BreweryDTO>>> GetAllBreweriesWithBeers()
        {
            var breweriesWithBeers = await  _breweryService.GetAllBreweriesWithBeers();
            return Ok(breweriesWithBeers);
        }
    }
}
