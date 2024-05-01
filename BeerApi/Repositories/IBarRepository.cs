using BeerApi.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BeerApi.Repositories
{
    public interface IBarRepository
    {
        Task<IEnumerable<Bar>> GetAll();
        Task<Bar> GetById(int id);
        Task<Bar> Add(Bar bar);
        Task Update(Bar bar);
        Task Delete(Bar bar);
        Task<BarBeer> AddBarBeerLink(BarBeer barBeer);
        Task<Bar> GetBarByIdWithBeers(int barId);
        Task<List<Bar>> GetAllBarsWithBeers();
    }
}
