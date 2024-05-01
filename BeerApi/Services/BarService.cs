using AutoMapper;
using BeerApi.DTOs;
using BeerApi.Models;
using BeerApi.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BeerApi.Services
{
    public class BarService : IBarService
    {
        private readonly IBarRepository _barRepository;
        private readonly IMapper _mapper;

        public BarService(IBarRepository barRepository, IMapper mapper)
        {
            _barRepository = barRepository;
            _mapper = mapper;
        }

        public async Task<BarDTO> AddBar(BarDTO barDTO)
        {
            var barToAdd = _mapper.Map<Bar>(barDTO);
            await _barRepository.Add(barToAdd);
            return _mapper.Map<BarDTO>(barToAdd);
        }

        public async Task UpdateBar(int id, BarDTO barDTO)
        {
            var existingBar =  await _barRepository.GetById(id);
            if (existingBar == null)
            {
                throw new ArgumentException("Bar not found");
            }

            _mapper.Map(barDTO, existingBar);
            await _barRepository.Update(existingBar);
        }

        public async Task<IEnumerable<BarDTO>> GetBars()
        {
            var bars = await _barRepository.GetAll();
            return _mapper.Map<IEnumerable<BarDTO>>(bars);
        }

        public async Task<BarDTO> GetBarById(int id)
        {
            var bar = await _barRepository.GetById(id);
            return _mapper.Map<BarDTO>(bar);
        }

        public async Task<BarBeerDTO> AddBarBeerLink(BarBeerDTO barBeerDTO)
        {
            var barBeerToAdd = _mapper.Map<BarBeer>(barBeerDTO);
            await _barRepository.AddBarBeerLink(barBeerToAdd);
            return _mapper.Map<BarBeerDTO>(barBeerToAdd);
        }

        public async Task<BarDTO> GetBarByIdWithBeers(int barId)
        {
            var barWithBeers = await _barRepository.GetBarByIdWithBeers(barId);

            // Map the bar entity to a DTO, including associated beers
            var barDTO = _mapper.Map<BarDTO>(barWithBeers);
            barDTO.Beers = _mapper.Map<IEnumerable<BeerDTO>>(barWithBeers.BarBeers.Select(bb => bb.Beer));

            return barDTO;
        }


        public async Task<List<BarDTO>> GetAllBarsWithBeers()
        {
            var barsWithBeers = await _barRepository.GetAllBarsWithBeers();

            var barDTOs =  barsWithBeers.Select(bar =>
            {
                var barDTO = _mapper.Map<BarDTO>(bar);
                barDTO.Beers = _mapper.Map<List<BeerDTO>>(bar.BarBeers.Select(bb => bb.Beer));
                return barDTO;
            });

            return barDTOs.ToList();
        }

    }
}
