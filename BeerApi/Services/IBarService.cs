using BeerApi.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BeerApi.Services
{
    public interface IBarService
    {
        Task<BarDTO> AddBar(BarDTO barDTO);
        Task UpdateBar(int id, BarDTO barDTO);
        Task<IEnumerable<BarDTO>> GetBars();
        Task<BarDTO> GetBarById(int id);
        Task<BarBeerDTO> AddBarBeerLink(BarBeerDTO barBeerDTO);
        Task<BarDTO> GetBarByIdWithBeers(int barId);
        Task<List<BarDTO>> GetAllBarsWithBeers();
    }
}
