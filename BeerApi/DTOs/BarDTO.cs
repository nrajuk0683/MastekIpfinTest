using System.Collections.Generic;

namespace BeerApi.DTOs
{
    public class BarDTO
    {
        public int BarId { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public IEnumerable<BeerDTO> Beers { get; set; }
    }
}
