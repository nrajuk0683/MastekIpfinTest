using BeerApi.DTOs;
using System.Collections.Generic;

namespace BeerApi.Services
{
    public interface IBreweryService
    {
        Task<BreweryDTO> AddBrewery(BreweryDTO breweryDTO);
        Task<BreweryDTO> UpdateBrewery(int id, BreweryDTO breweryDTO);
        Task<IEnumerable<BreweryDTO>> GetBreweries();
        Task<BreweryDTO> GetBreweryById(int id);
        Task<BreweryBeerDTO> AddBreweryBeerLink(BreweryBeerDTO breweryBeerDTO);
        Task<BreweryDTO> GetBreweryByIdWithBeers(int breweryId);
        Task<IEnumerable<BreweryDTO>> GetAllBreweriesWithBeers();
    }
}
