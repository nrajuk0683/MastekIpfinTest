using AutoMapper;
using BeerApi.DTOs;
using BeerApi.Models;

namespace BeerApi.Mappings
{
    public class BreweryProfile : Profile
    {
        public BreweryProfile()
        {
            CreateMap<Brewery, BreweryDTO>().ReverseMap();
            CreateMap<BreweryBeer, BreweryBeerDTO>().ReverseMap();
        }
    }
}
