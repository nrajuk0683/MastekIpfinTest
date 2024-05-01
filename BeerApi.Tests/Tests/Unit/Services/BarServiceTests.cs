using AutoMapper;
using BeerApi.DTOs;
using BeerApi.Models;
using BeerApi.Repositories;
using BeerApi.Services;
using Moq;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

public class BarServiceTests
{
    private readonly BarService _barService;
    private readonly Mock<IBarRepository> _mockBarRepository;
    private readonly IMapper _mapper;

    public BarServiceTests()
    {
        _mockBarRepository = new Mock<IBarRepository>();

        var config = new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<BarDTO, Bar>().ReverseMap();
            cfg.CreateMap<BeerDTO, Beer>().ReverseMap();
            cfg.CreateMap<BarBeerDTO, BarBeer>().ReverseMap();
        });
        _mapper = config.CreateMapper();

        _barService = new BarService(_mockBarRepository.Object, _mapper);
    }

    [Fact]
    public async Task AddBar_ReturnsAddedBar()
    {
        // Arrange
        var barDTO = new BarDTO { Name = "Test Bar" };
        var barToAdd = _mapper.Map<Bar>(barDTO);
        var expectedTask = Task.FromResult<Bar>(barToAdd);

        _mockBarRepository.Setup(repo => repo.Add(It.IsAny<Bar>())).Returns(expectedTask);

        var mockRepository = new Mock<IBarRepository>();
        var mockMapper = new Mock<IMapper>();

        var service = new BarService(mockRepository.Object, mockMapper.Object);
        //var barDTO = new BarDTO { /* Initialize BarDTO properties */ };

        mockMapper.Setup(m => m.Map<Bar>(It.IsAny<BarDTO>())).Returns(new Bar());
        mockMapper.Setup(m => m.Map<BarDTO>(It.IsAny<Bar>())).Returns(new BarDTO());

        // Act
        var result = await _barService.AddBar(barDTO);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(barDTO.Name, result.Name);
    }

    [Fact]
    public async Task UpdateBar_ExistingBar_UpdatesBar()
    {
        // Arrange
        int barId = 1;
        var barDTO = new BarDTO { Name = "Updated Bar" };
        var existingBar = new Bar { BarId = barId, Name = "Existing Bar" };
        var expectedTask = Task.FromResult<Bar>(existingBar);
        _mockBarRepository.Setup(repo => repo.GetById(barId)).Returns(expectedTask);

        // Act
        await _barService.UpdateBar(barId, barDTO);

        // Assert
        _mockBarRepository.Verify(repo => repo.Update(existingBar), Times.Once);
    }

    [Fact]
    public async Task GetBars_ReturnsAllBars()
    {
        // Arrange
        var bars = new List<Bar>
    {
        new Bar { BarId = 1, Name = "Bar 1" },
        new Bar { BarId = 2, Name = "Bar 2" },
        new Bar { BarId = 3, Name = "Bar 3" }
    };
        var expectedTask = Task.FromResult<IEnumerable<Bar>>(bars);
        _mockBarRepository.Setup(repo => repo.GetAll()).Returns(expectedTask);

        // Act
        var result = await _barService.GetBars();

        // Assert
        Assert.NotNull(result);
        Assert.Equal(3, result.Count()); 
    }

    [Fact]
    public async Task GetBarById_ReturnsBarById()
    {
        // Arrange
        int barId = 1;
        var bar = new Bar { BarId = barId, Name = "Bar 1" };
        var expectedTask = Task.FromResult<Bar>(bar);
        _mockBarRepository.Setup(repo => repo.GetById(barId)).Returns(expectedTask);

        // Act
        var result = await _barService.GetBarById(barId);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(bar.Name, result.Name);
    }

    [Fact]
    public async Task AddBarBeerLink_ReturnsAddedBarBeer()
    {
        // Arrange
        var barBeerDTO = new BarBeerDTO {  };
        var barBeerToAdd = _mapper.Map<BarBeer>(barBeerDTO);
        var expectedTask = Task.FromResult<BarBeer>(barBeerToAdd);
        _mockBarRepository.Setup(repo => repo.AddBarBeerLink(It.IsAny<BarBeer>())).Returns(expectedTask);

        // Act
        var result = await _barService.AddBarBeerLink(barBeerDTO);

        // Assert
        Assert.NotNull(result);
    }

    [Fact]
    public async Task GetAllBarsWithBeers_ReturnsAllBarsWithBeers()
    {
        // Arrange
        var barsWithBeers = new List<Bar> { };
        var expectedTask = Task.FromResult<List<Bar>>(barsWithBeers);
        _mockBarRepository.Setup(repo => repo.GetAllBarsWithBeers()).Returns(expectedTask);

        // Act
        var result = await _barService.GetAllBarsWithBeers();

        // Assert
        Assert.NotNull(result);
    }

}
