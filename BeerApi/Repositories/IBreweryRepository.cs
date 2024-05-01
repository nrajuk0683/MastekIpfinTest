using BeerApi.Models;
using System.Collections.Generic;

namespace BeerApi.Repositories
{
    public interface IBreweryRepository
    {
        Task<IEnumerable<Brewery>> GetAll();
        Task<Brewery> GetById(int id);
        Task<Brewery> Add(Brewery brewery);
        Task<Brewery> Update(Brewery brewery);
        Task Delete(Brewery brewery);
        Task<BreweryBeer> AddBreweryBeerLink(BreweryBeer breweryBeer);
        Task<Brewery> GetBreweryByIdWithBeers(int breweryId);
        Task<IEnumerable<Brewery>> GetAllBreweriesWithBeers();
    }
}
