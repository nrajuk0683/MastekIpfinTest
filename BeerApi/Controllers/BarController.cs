using BeerApi.DTOs;
using BeerApi.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BeerApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BarController : ControllerBase
    {
        private readonly IBarService _barService;

        public BarController(IBarService barService)
        {
            _barService = barService;
        }

        [HttpPost]
        public async Task<ActionResult<BarDTO>> PostBar(BarDTO barDTO)
        {
            if (string.IsNullOrWhiteSpace(barDTO.Name))
            {
                ModelState.AddModelError("BarName", "BarName cannot be empty.");
                return BadRequest(ModelState);
            }
            var createdBar = await _barService.AddBar(barDTO);
            return CreatedAtAction(nameof(GetBarById), new { id = createdBar.BarId }, createdBar);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutBar(int id, BarDTO barDTO)
        {
            if (string.IsNullOrWhiteSpace(barDTO.Name))
            {
                ModelState.AddModelError("BarName", "BarName cannot be empty.");
                return BadRequest(ModelState);
            }
            await _barService.UpdateBar(id, barDTO);
            return NoContent();
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<BarDTO>>> GetBars()
        {
            var bars = await _barService.GetBars();
            return Ok(bars);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<BarDTO>> GetBarById(int id)
        {
            var bar = await _barService.GetBarById(id);
            if (bar == null)
            {
                return NotFound();
            }
            return Ok(bar);
        }

        [HttpPost("beer")]
        public async Task<ActionResult<BarBeerDTO>> AddBarBeerLink(BarBeerDTO barBeerDTO)
        {
            var createdBarBeerLink = await _barService.AddBarBeerLink(barBeerDTO);
            return CreatedAtAction(nameof(GetBarByIdWithBeers), new { barId = createdBarBeerLink.BarId }, createdBarBeerLink);
        }

        [HttpGet("{barId}/beer")]
        public async Task<ActionResult<BarDTO>> GetBarByIdWithBeers(int barId)
        {
            var barWithBeers = await _barService.GetBarByIdWithBeers(barId);
            if (barWithBeers == null)
            {
                return NotFound();
            }
            return Ok(barWithBeers);
        }

        [HttpGet("beer")]
        public async Task<ActionResult<IEnumerable<BarDTO>>> GetAllBarsWithBeers()
        {
            var barsWithBeers = await _barService.GetAllBarsWithBeers();
            return Ok(barsWithBeers);
        }
    }
}
