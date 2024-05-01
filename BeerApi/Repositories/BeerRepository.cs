using BeerApi.Data;
using BeerApi.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace BeerApi.Repositories
{
    public class BeerRepository : IBeerRepository
    {
        private readonly BeerContext _context;

        public BeerRepository(BeerContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Beer>> GetAll()
        {
            return await _context.Beers.ToListAsync();
        }

        public async Task<Beer> GetById(int id)
        {
            return await _context.Beers.FirstOrDefaultAsync(b => b.BeerId == id);
        }

        public async Task<Beer> Add(Beer beer)
        {
            _context.Beers.Add(beer);
            await _context.SaveChangesAsync();
            return beer;
        }

        public async Task Update(Beer beer)
        {
            _context.Beers.Update(beer);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(Beer beer)
        {
            _context.Beers.Remove(beer);
            await _context.SaveChangesAsync();
        }
    }
}
