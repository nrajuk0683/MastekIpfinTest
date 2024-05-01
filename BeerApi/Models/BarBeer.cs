namespace BeerApi.Models
{
    public class BarBeer
    {
        public int BarId { get; set; }
        public Bar Bar { get; set; }
        public int BeerId { get; set; }
        public Beer Beer { get; set; }
    }
}
