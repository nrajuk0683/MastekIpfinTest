using AutoMapper;
using BeerApi.DTOs;
using BeerApi.Models;
using BeerApi.Repositories;
using BeerApi.Services;
using Moq;
using System.Collections.Generic;
using System.Linq;
using Xunit;

public class BreweryServiceTests
{
    private readonly BreweryService _breweryService;
    private readonly Mock<IBreweryRepository> _mockBreweryRepository;
    private readonly IMapper _mapper;

    public BreweryServiceTests()
    {
        _mockBreweryRepository = new Mock<IBreweryRepository>();

        var config = new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<BreweryDTO, Brewery>().ReverseMap();
            cfg.CreateMap<BeerDTO, Beer>().ReverseMap();
            cfg.CreateMap<BreweryBeerDTO, BreweryBeer>().ReverseMap();
        });
        _mapper = config.CreateMapper();

        _breweryService = new BreweryService(_mockBreweryRepository.Object, _mapper);
    }

    [Fact]
    public async Task AddBrewery_ReturnsAddedBrewery()
    {
        // Arrange
        var breweryDTO = new BreweryDTO { Name = "Test Brewery" };
        var breweryToAdd = _mapper.Map<Brewery>(breweryDTO);
        _mockBreweryRepository.Setup(repo => repo.Add(It.IsAny<Brewery>())).Returns(Task.FromResult<Brewery>(breweryToAdd));

        // Act
        var result = await _breweryService.AddBrewery(breweryDTO);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(breweryDTO.Name, result.Name);
    }

    [Fact]
    public async Task UpdateBrewery_ExistingBrewery_ReturnsUpdatedBrewery()
    {
        // Arrange
        int breweryId = 1;
        var breweryDTO = new BreweryDTO { Name = "Updated Brewery" };
        var existingBrewery = new Brewery { BreweryId = breweryId, Name = "Existing Brewery" };

        _mockBreweryRepository.Setup(repo => repo.GetById(breweryId)).Returns(Task.FromResult<Brewery>(existingBrewery));

        // Act
        var result = await _breweryService.UpdateBrewery(breweryId, breweryDTO);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(breweryDTO.Name, result.Name);
    }

    [Fact]
    public async Task GetBreweries_ReturnsAllBreweries()
    {
        // Arrange
        var breweries = new List<Brewery>
    {
        new Brewery { BreweryId = 1, Name = "Brewery 1" },
        new Brewery { BreweryId = 2, Name = "Brewery 2" },
        new Brewery { BreweryId = 3, Name = "Brewery 3" }
    };

        _mockBreweryRepository.Setup(repo => repo.GetAll()).Returns(Task.FromResult<IEnumerable<Brewery>>(breweries));

        // Act
        var result = await _breweryService.GetBreweries();

        // Assert
        Assert.NotNull(result);
        Assert.Equal(3, result.Count()); 
    }

    [Fact]
    public async Task GetBreweryById_ReturnsBreweryById()
    {
        // Arrange
        int breweryId = 1;
        var brewery = new Brewery { BreweryId = breweryId, Name = "Brewery 1" };
        _mockBreweryRepository.Setup(repo => repo.GetById(breweryId)).Returns(Task.FromResult<Brewery>(brewery));

        // Act
        var result = await _breweryService.GetBreweryById(breweryId);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(brewery.Name, result.Name);
    }

    [Fact]
    public async Task AddBreweryBeerLink_ReturnsAddedBreweryBeer()
    {
        // Arrange
        var breweryBeerDTO = new BreweryBeerDTO {  };
        var breweryBeerToAdd = _mapper.Map<BreweryBeer>(breweryBeerDTO);

        _mockBreweryRepository.Setup(repo => repo.AddBreweryBeerLink(It.IsAny<BreweryBeer>())).Returns(Task.FromResult<BreweryBeer>(breweryBeerToAdd));

        // Act
        var result = await _breweryService.AddBreweryBeerLink(breweryBeerDTO);

        // Assert
        Assert.NotNull(result);
    }

    [Fact]
    public void GetAllBreweriesWithBeers_ReturnsAllBreweriesWithBeers()
    {
        // Arrange
        var breweriesWithBeers = new List<Brewery> {  };
        _mockBreweryRepository.Setup(repo => repo.GetAllBreweriesWithBeers()).Returns(Task.FromResult<IEnumerable<Brewery>>(breweriesWithBeers));

        // Act
        var result = _breweryService.GetAllBreweriesWithBeers();

        // Assert
        Assert.NotNull(result);
    }

}
