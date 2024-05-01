using AutoMapper;
using BeerApi.DTOs;
using BeerApi.Models;
using BeerApi.Repositories;
using BeerApi.Services;
using Moq;
using System.Collections.Generic;
using System.Linq;
using Xunit;

public class BeerServiceTests
{
    private readonly BeerService _beerService;
    private readonly Mock<IBeerRepository> _mockBeerRepository;
    private readonly IMapper _mapper;

    public BeerServiceTests()
    {
        _mockBeerRepository = new Mock<IBeerRepository>();

        var config = new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<BeerDTO, Beer>().ReverseMap();
        });
        _mapper = config.CreateMapper();

        _beerService = new BeerService(_mockBeerRepository.Object, _mapper);
    }

    [Fact]
    public async Task AddBeer_ReturnsAddedBeer()
    {
        // Arrange
        var beerDTO = new BeerDTO { Name = "Test Beer", PercentageAlcoholByVolume = 5.0m };

        var beerToAdd = _mapper.Map<Beer>(beerDTO);
        var expectedTask = Task.FromResult<Beer>(beerToAdd);
        _mockBeerRepository.Setup(repo => repo.Add(It.IsAny<Beer>())).Returns(expectedTask);

        // Act
        var result = await _beerService.AddBeer(beerDTO);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(beerDTO.Name, result.Name);
        Assert.Equal(beerDTO.PercentageAlcoholByVolume, result.PercentageAlcoholByVolume);
    }

    [Fact]
    public async Task UpdateBeer_ExistingBeer_UpdatesBeer()
    {
        // Arrange
        int beerId = 1;
        var beerDTO = new BeerDTO { Name = "Updated Beer", PercentageAlcoholByVolume = 6.0m };
        var existingBeer = new Beer { BeerId = beerId, Name = "Existing Beer", PercentageAlcoholByVolume = 5.0m };
        var expectedTask = Task.FromResult<Beer>(existingBeer);
        _mockBeerRepository.Setup(repo => repo.GetById(beerId)).Returns(expectedTask);

        // Act
        await _beerService.UpdateBeer(beerId, beerDTO);

        // Assert
        _mockBeerRepository.Verify(repo => repo.Update(existingBeer), Times.Once);
        Assert.Equal(beerDTO.Name, existingBeer.Name);
        Assert.Equal(beerDTO.PercentageAlcoholByVolume, existingBeer.PercentageAlcoholByVolume);
    }

    [Fact]
    public async Task GetBeers_WithFilters_ReturnsFilteredBeers()
    {
        // Arrange
        decimal gtAlcoholByVolume = 5.0m;
        decimal ltAlcoholByVolume = 8.0m;
        var beers = new List<Beer>
        {
            new Beer { BeerId = 1, Name = "Beer 1", PercentageAlcoholByVolume = 6.0m },
            new Beer { BeerId = 2, Name = "Beer 2", PercentageAlcoholByVolume = 7.0m },
            new Beer { BeerId = 3, Name = "Beer 3", PercentageAlcoholByVolume = 4.0m }
        };
        var expectedTask = Task.FromResult<IEnumerable<Beer>>(beers);
        _mockBeerRepository.Setup(repo => repo.GetAll()).Returns(expectedTask);

        // Act
        var result = await _beerService.GetBeers(gtAlcoholByVolume, ltAlcoholByVolume);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(2, result.Count()); 
    }

    [Fact]
    public async Task GetBeerById_ExistingId_ReturnsBeer()
    {
        // Arrange
        int beerId = 1;
        var beer = new Beer { BeerId = beerId, Name = "Beer 1", PercentageAlcoholByVolume = 5.0m };
        var expectedTask = Task.FromResult<Beer>(beer);
        _mockBeerRepository.Setup(repo => repo.GetById(beerId)).Returns(expectedTask);

        // Act
        var result = await _beerService.GetBeerById(beerId);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(beer.Name, result.Name);
        Assert.Equal(beer.PercentageAlcoholByVolume, result.PercentageAlcoholByVolume);
    }

    [Fact]
    public async Task GetBeers_ByVolumeRange_ReturnsBeersWithinRange()
    {
        // Arrange
        var mockRepository = new Mock<IBeerRepository>();
        var mockMapper = new Mock<IMapper>();

        var service = new BeerService(mockRepository.Object, mockMapper.Object);
        var gtAlcoholByVolume = 5.0m;
        var ltAlcoholByVolume = 8.0m;
        var expectedBeers = new List<Beer>
            {
                new Beer { PercentageAlcoholByVolume = 6.0m },
                new Beer { PercentageAlcoholByVolume = 7.5m },
            };

        mockRepository.Setup(r => r.GetAll()).ReturnsAsync(expectedBeers);
        mockMapper.Setup(m => m.Map<IEnumerable<BeerDTO>>(It.IsAny<IEnumerable<Beer>>()))
            .Returns<IEnumerable<Beer>>(beers => beers.Select(b => new BeerDTO {  }));

        // Act
        var result = await service.GetBeers(gtAlcoholByVolume, ltAlcoholByVolume);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(2, result.Count()); 
        mockRepository.Verify(r => r.GetAll(), Times.Once);
    }
}
