using BeerApi.Data;
using BeerApi.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace BeerApi.Repositories
{
    public class BreweryRepository : IBreweryRepository
    {
        private readonly BeerContext _context;

        public BreweryRepository(BeerContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Brewery>> GetAll()
        {
            return await _context.Breweries.ToListAsync();
        }

        public async Task<Brewery> GetById(int id)
        {
            return await _context.Breweries.FirstOrDefaultAsync(b => b.BreweryId == id);
        }

        public async Task<Brewery> Add(Brewery brewery)
        {
            await _context.Breweries.AddAsync(brewery);
            await _context.SaveChangesAsync();
            return brewery;
        }

        public async Task<Brewery> Update(Brewery brewery)
        {
            _context.Breweries.Update(brewery);
            await _context.SaveChangesAsync();
            return brewery;
        }

        public async Task Delete(Brewery brewery)
        {
            _context.Breweries.Remove(brewery);
            await _context.SaveChangesAsync();
        }
        public async Task<BreweryBeer> AddBreweryBeerLink(BreweryBeer breweryBeer)
        {
            await _context.BreweryBeers.AddAsync(breweryBeer);
            await _context.SaveChangesAsync();
            return breweryBeer;
        }

        public async Task<Brewery> GetBreweryByIdWithBeers(int breweryId)
        {
            return await _context.Breweries
                .Include(b => b.BreweryBeers)
                .ThenInclude(bb => bb.Beer)
                .FirstOrDefaultAsync(b => b.BreweryId == breweryId);
        }

        public async Task<IEnumerable<Brewery>> GetAllBreweriesWithBeers()
        {
            return await _context.Breweries
                .Include(b => b.BreweryBeers)
                .ThenInclude(bb => bb.Beer)
                .ToListAsync();
        }
    }
}
