using BeerApi.Models;
using System.Collections.Generic;

namespace BeerApi.Repositories
{
    public interface IBeerRepository
    {
        Task<IEnumerable<Beer>> GetAll();
        Task<Beer> GetById(int id);
        Task<Beer> Add(Beer beer);
        Task Update(Beer beer);
        Task Delete(Beer beer);
    }
}
