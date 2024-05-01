namespace BeerApi.DTOs
{
    public class BeerDTO
    {
        public int BeerId { get; set; }
        public string Name { get; set; }
        public decimal PercentageAlcoholByVolume { get; set; }
        public int BreweryId { get; set; }
    }
}
