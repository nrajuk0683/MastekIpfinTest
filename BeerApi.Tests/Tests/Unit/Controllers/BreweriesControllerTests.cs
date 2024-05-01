using BeerApi.Controllers;
using BeerApi.DTOs;
using BeerApi.Services;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace BeerApi.Tests.Tests.Unit.Controllers
{
    public class BreweriesControllerTests
    {
        [Fact]
        public async Task GetAllBreweries_ReturnsAllBreweries()
        {
            // Arrange
            var mockService = new Mock<IBreweryService>();
            var controller = new BreweriesController(mockService.Object);

            var expectedBreweries = new List<BreweryDTO>
            {
                new BreweryDTO {  },
                new BreweryDTO {  },
            };
            mockService.Setup(s => s.GetBreweries()).Returns(Task.FromResult<IEnumerable<BreweryDTO>>(expectedBreweries));

            // Act
            var result = await controller.GetBreweries();

            // Assert
            var okObjectResult = Assert.IsType<OkObjectResult>(result.Result);
            var actualBreweries = Assert.IsAssignableFrom<IEnumerable<BreweryDTO>>(okObjectResult.Value);
        }

        [Fact]
        public async Task AddBrewery_ValidBrewery_ReturnsCreatedResult()
        {
            // Arrange
            var mockService = new Mock<IBreweryService>();
            var controller = new BreweriesController(mockService.Object);

            var breweryToAdd = new BreweryDTO {  };
            mockService.Setup(s => s.AddBrewery(It.IsAny<BreweryDTO>())).Returns(Task.FromResult<BreweryDTO>(breweryToAdd));

            // Act
            var result = await controller.PostBrewery(breweryToAdd);

            // Assert
            var createdResult = Assert.IsType<CreatedAtActionResult>(result.Result);
            Assert.Equal("GetBreweryById", createdResult.ActionName);
            Assert.Equal(breweryToAdd, createdResult.Value);
        }

        [Fact]
        public async Task UpdateBrewery_ExistingBrewery_ReturnsNoContentResult()
        {
            // Arrange
            var mockService = new Mock<IBreweryService>();
            var controller = new BreweriesController(mockService.Object);

            var breweryId = 1;
            var breweryToUpdate = new BreweryDTO {  };
            mockService.Setup(s => s.UpdateBrewery(breweryId, It.IsAny<BreweryDTO>())).Returns(Task.FromResult<BreweryDTO>(breweryToUpdate));

            // Act
            var result = await controller.PutBrewery(breweryId, breweryToUpdate);

            // Assert
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task GetBreweryById_ExistingBreweryId_ReturnsBrewery()
        {
            // Arrange
            var mockService = new Mock<IBreweryService>();
            var controller = new BreweriesController(mockService.Object);

            var breweryId = 1;
            var expectedBrewery = new BreweryDTO {  };
            mockService.Setup(s => s.GetBreweryById(breweryId)).Returns(Task.FromResult<BreweryDTO>(expectedBrewery));

            // Act
            var result = await controller.GetBreweryById(breweryId);

            // Assert
            var okObjectResult = Assert.IsType<OkObjectResult>(result.Result);
            Assert.Equal(expectedBrewery, okObjectResult.Value);
        }

        [Fact]
        public async Task AddBreweryBeerLink_ValidBreweryBeer_ReturnsCreatedResult()
        {
            // Arrange
            var mockService = new Mock<IBreweryService>();
            var controller = new BreweriesController(mockService.Object);

            var breweryBeerToAdd = new BreweryBeerDTO {  };
            mockService.Setup(s => s.AddBreweryBeerLink(It.IsAny<BreweryBeerDTO>())).Returns(Task.FromResult<BreweryBeerDTO>(breweryBeerToAdd));

            // Act
            var result = await controller.AddBreweryBeerLink(breweryBeerToAdd);

            // Assert
            var createdResult = Assert.IsType<CreatedAtActionResult>(result.Result);
            Assert.Equal("GetBreweryByIdWithBeers", createdResult.ActionName);
            Assert.Equal(breweryBeerToAdd, createdResult.Value);
        }

        [Fact]
        public async Task GetBreweryByIdWithBeers_ExistingBreweryId_ReturnsBreweryWithBeers()
        {
            // Arrange
            var mockService = new Mock<IBreweryService>();
            var controller = new BreweriesController(mockService.Object);

            var breweryId = 1;
            var expectedBreweryWithBeers = new BreweryDTO {  };
            mockService.Setup(s => s.GetBreweryByIdWithBeers(breweryId)).Returns(Task.FromResult<BreweryDTO>(expectedBreweryWithBeers));

            // Act
            var result = await controller.GetBreweryByIdWithBeers(breweryId);

            // Assert
            var okObjectResult = Assert.IsType<OkObjectResult>(result.Result);
            Assert.Equal(expectedBreweryWithBeers, okObjectResult.Value);
        }

        [Fact]
        public async Task GetAllBreweriesWithBeers_ReturnsAllBreweriesWithBeers()
        {
            // Arrange
            var mockService = new Mock<IBreweryService>();
            var controller = new BreweriesController(mockService.Object);

            var expectedBreweriesWithBeers = new List<BreweryDTO> {  };
            mockService.Setup(s => s.GetAllBreweriesWithBeers()).Returns(Task.FromResult<IEnumerable<BreweryDTO>>(expectedBreweriesWithBeers));

            // Act
            var result = await controller.GetAllBreweriesWithBeers();

            // Assert
            var okObjectResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnedBreweriesWithBeers = Assert.IsAssignableFrom<IEnumerable<BreweryDTO>>(okObjectResult.Value);
            Assert.Equal(expectedBreweriesWithBeers.Count(), returnedBreweriesWithBeers.Count());
        }
    }
}
