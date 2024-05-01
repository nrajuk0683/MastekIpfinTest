using AutoMapper;
using BeerApi.DTOs;
using BeerApi.Models;
using BeerApi.Repositories;
using System.Collections.Generic;
using System.Linq;

namespace BeerApi.Services
{
    public class BeerService : IBeerService
    {
        private readonly IBeerRepository _beerRepository;
        private readonly IMapper _mapper;

        public BeerService(IBeerRepository beerRepository, IMapper mapper)
        {
            _beerRepository = beerRepository;
            _mapper = mapper;
        }

        public async Task<BeerDTO> AddBeer(BeerDTO beerDTO)
        {
            var beer = _mapper.Map<Beer>(beerDTO);
            await _beerRepository.Add(beer);
            return _mapper.Map<BeerDTO>(beer);
        }

        public async Task UpdateBeer(int id, BeerDTO beerDTO)
        {
            var existingBeer = await _beerRepository.GetById(id);
            if (existingBeer == null)
            {
                throw new KeyNotFoundException($"Beer with ID {id} not found");
            }

            _mapper.Map(beerDTO, existingBeer);
            await _beerRepository.Update(existingBeer);
        }

        public async Task<IEnumerable<BeerDTO>> GetBeers(decimal? gtAlcoholByVolume, decimal? ltAlcoholByVolume)
        {
            IEnumerable<Beer> beers = await _beerRepository.GetAll();

            if (gtAlcoholByVolume.HasValue)
            {
                beers = beers.Where(b => b.PercentageAlcoholByVolume > gtAlcoholByVolume.Value);
            }

            if (ltAlcoholByVolume.HasValue)
            {
                beers = beers.Where(b => b.PercentageAlcoholByVolume < ltAlcoholByVolume.Value);
            }

            return _mapper.Map<IEnumerable<BeerDTO>>(beers);
        }

        public async Task<BeerDTO> GetBeerById(int id)
        {
            var beer = await  _beerRepository.GetById(id);
            if (beer == null)
            {
                throw new KeyNotFoundException($"Beer with ID {id} not found");
            }

            return _mapper.Map<BeerDTO>(beer);
        }
    }
}
