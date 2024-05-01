using AutoMapper;
using BeerApi.DTOs;
using BeerApi.Models;
using BeerApi.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BeerApi.Services
{
    public class BreweryService : IBreweryService
    {
        private readonly IBreweryRepository _breweryRepository;
        private readonly IMapper _mapper;

        public BreweryService(IBreweryRepository breweryRepository, IMapper mapper)
        {
            _breweryRepository = breweryRepository;
            _mapper = mapper;
        }

        public async Task<BreweryDTO> AddBrewery(BreweryDTO breweryDTO)
        {
            var breweryToAdd = _mapper.Map<Brewery>(breweryDTO);
            await _breweryRepository.Add(breweryToAdd);
            return _mapper.Map<BreweryDTO>(breweryToAdd);
        }

        public async Task<BreweryDTO> UpdateBrewery(int id, BreweryDTO breweryDTO)
        {
            var existingBrewery = await _breweryRepository.GetById(id);
            if (existingBrewery == null)
            {
                throw new ArgumentException("Brewery not found");
            }

            _mapper.Map(breweryDTO, existingBrewery);
            await _breweryRepository.Update(existingBrewery);
            return breweryDTO;
        }

        public async Task<IEnumerable<BreweryDTO>> GetBreweries()
        {
            var breweries = await _breweryRepository.GetAll();
            return _mapper.Map<IEnumerable<BreweryDTO>>(breweries);
        }

        public async Task<BreweryDTO> GetBreweryById(int id)
        {
            var brewery = await _breweryRepository.GetById(id);
            return _mapper.Map<BreweryDTO>(brewery);
        }

        public async Task<BreweryBeerDTO> AddBreweryBeerLink(BreweryBeerDTO breweryBeerDTO)
        {
            var breweryBeerToAdd = _mapper.Map<BreweryBeer>(breweryBeerDTO);
            await _breweryRepository.AddBreweryBeerLink(breweryBeerToAdd);
            return _mapper.Map<BreweryBeerDTO>(breweryBeerToAdd);
        }

        public async Task<BreweryDTO> GetBreweryByIdWithBeers(int breweryId)
        {
            var breweryWithBeers = await _breweryRepository.GetBreweryByIdWithBeers(breweryId);
            var breweryDTO = _mapper.Map<BreweryDTO>(breweryWithBeers);
            breweryDTO.Beers = _mapper.Map<IEnumerable<BeerDTO>>(breweryWithBeers.BreweryBeers.Select(bb => bb.Beer));

            return breweryDTO;
        }


        public async Task<IEnumerable<BreweryDTO>> GetAllBreweriesWithBeers()
        {
            var breweriesWithBeers = await _breweryRepository.GetAllBreweriesWithBeers();

            var breweryDTOs = breweriesWithBeers.Select(brewery =>
            {
                var breweryDTO = _mapper.Map<BreweryDTO>(brewery);
                breweryDTO.Beers = _mapper.Map<IEnumerable<BeerDTO>>(brewery.BreweryBeers.Select(bb => bb.Beer));
                return breweryDTO;
            });

            return breweryDTOs;
        }

    }
}
