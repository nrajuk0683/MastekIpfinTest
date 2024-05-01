using BeerApi.DTOs;
using System.Collections.Generic;

namespace BeerApi.Services
{
    public interface IBeerService
    {
        Task<BeerDTO> AddBeer(BeerDTO beerDTO);
        Task UpdateBeer(int id, BeerDTO beerDTO);
        Task<IEnumerable<BeerDTO>> GetBeers(decimal? gtAlcoholByVolume, decimal? ltAlcoholByVolume);
        Task<BeerDTO> GetBeerById(int id);
    }
}
