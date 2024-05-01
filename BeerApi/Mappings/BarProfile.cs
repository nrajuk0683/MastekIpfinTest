using AutoMapper;
using BeerApi.DTOs;
using BeerApi.Models;

namespace BeerApi.Mappings
{
    public class BarProfile : Profile
    {
        public BarProfile()
        {
            CreateMap<Bar, BarDTO>().ReverseMap();
            CreateMap<BarBeer, BarBeerDTO>().ReverseMap();
        }
    }
}
