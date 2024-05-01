using System.Collections.Generic;

namespace BeerApi.Models
{
    public class Beer
    {
        public int BeerId { get; set; }
        public string Name { get; set; }
        public decimal PercentageAlcoholByVolume { get; set; }
        public int BreweryId { get; set; }
        public Brewery Brewery { get; set; }
        public ICollection<BarBeer> BarBeers { get; set; }
    }
}
