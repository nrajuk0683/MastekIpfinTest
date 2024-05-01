using BeerApi.Data;
using BeerApi.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BeerApi.Repositories
{
    public class BarRepository : IBarRepository
    {
        private readonly BeerContext _context;

        public BarRepository(BeerContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Bar>> GetAll()
        {
            return await _context.Bars.ToListAsync();
        }

        public async Task<Bar> GetById(int id)
        {
            return await _context.Bars.FirstOrDefaultAsync(b => b.BarId == id);
        }

        public async Task<Bar> Add(Bar bar)
        {
            _context.Bars.Add(bar);
            await _context.SaveChangesAsync();
            return bar;
        }

        public async Task Update(Bar bar)
        {
            _context.Bars.Update(bar);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(Bar bar)
        {
            _context.Bars.Remove(bar);
            await _context.SaveChangesAsync();
        }

        public async Task<BarBeer> AddBarBeerLink(BarBeer barBeer)
        {
            _context.BarBeers.Add(barBeer);
            await _context.SaveChangesAsync();
            return barBeer;
        }

        public async Task<Bar> GetBarByIdWithBeers(int barId)
        {
            return await _context.Bars
                .Include(b => b.BarBeers)
                .ThenInclude(bb => bb.Beer)
                .FirstOrDefaultAsync(b => b.BarId == barId);
        }

        public async Task<List<Bar>> GetAllBarsWithBeers()
        {
            return await _context.Bars
                .Include(b => b.BarBeers)
                .ThenInclude(bb => bb.Beer)
                .ToListAsync();
        }
    }
}
