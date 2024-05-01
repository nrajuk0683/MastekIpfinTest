using BeerApi.DTOs;
using BeerApi.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace BeerApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BeersController : ControllerBase
    {
        private readonly IBeerService _beerService;

        public BeersController(IBeerService beerService)
        {
            _beerService = beerService;
        }

        [HttpPost]
        public async Task<ActionResult<BeerDTO>> PostBeer(BeerDTO beerDTO)
        {
            var createdBeer = await _beerService.AddBeer(beerDTO);
            return CreatedAtAction(nameof(GetBeerById), new { id = createdBeer.BeerId }, createdBeer);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutBeer(int id, BeerDTO beerDTO)
        {
            await _beerService.UpdateBeer(id, beerDTO);
            return NoContent();
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<BeerDTO>>> GetBeers([FromQuery] decimal? gtAlcoholByVolume, [FromQuery] decimal? ltAlcoholByVolume)
        {
            var beers = await _beerService.GetBeers(gtAlcoholByVolume, ltAlcoholByVolume);
            return Ok(beers);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<BeerDTO>> GetBeerById(int id)
        {
            try
            {
                var beer = await _beerService.GetBeerById(id);
                return Ok(beer);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }
    }
}
