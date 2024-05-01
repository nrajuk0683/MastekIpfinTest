using BeerApi.Models;
using System.Collections;
using System.Collections.Generic;

namespace BeerApi.DTOs
{
    public class BreweryDTO
    {
        public int BreweryId { get; set; }
        public string Name { get; set; }
        public IEnumerable<BeerDTO> Beers { get; set; }
    }
}
