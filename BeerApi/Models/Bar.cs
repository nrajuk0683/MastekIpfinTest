using System.Collections.Generic;

namespace BeerApi.Models
{
    public class Bar
    {
        public int BarId { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public ICollection<BarBeer> BarBeers { get; set; }
    }
}
