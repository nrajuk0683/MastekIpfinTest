using AutoMapper;
using BeerApi.DTOs;
using BeerApi.Models;

namespace BeerApi.Mappings
{
    public class BeerProfile : Profile
    {
        public BeerProfile()
        {
            CreateMap<Beer, BeerDTO>().ReverseMap(); ;
        }
    }
}
