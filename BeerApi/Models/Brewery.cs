using System.Collections.Generic;

namespace BeerApi.Models
{
    public class Brewery
    {
        public int BreweryId { get; set; }
        public string Name { get; set; }
        public ICollection<Beer> Beers { get; set; }
        public ICollection<BreweryBeer> BreweryBeers { get; set; }
    }
}
