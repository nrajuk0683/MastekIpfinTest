using BeerApi.Controllers;
using BeerApi.DTOs;
using BeerApi.Models;
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
    public class BeersControllerTests
    {
        [Fact]
        public async Task PostBeer_ReturnsCreatedResult()
        {
            // Arrange
            var mockService = new Mock<IBeerService>();
            var controller = new BeersController(mockService.Object);

            var beerToAdd = new BeerDTO {  };
            var expectedTask = Task.FromResult<BeerDTO>(beerToAdd);
            mockService.Setup(s => s.AddBeer(It.IsAny<BeerDTO>())).Returns(expectedTask);

            // Act
            var result = await controller.PostBeer(beerToAdd);

            // Assert
            var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result.Result);
            Assert.Equal("GetBeerById", createdAtActionResult.ActionName);
            mockService.Verify(s => s.AddBeer(It.IsAny<BeerDTO>()), Times.Once);
        }

        [Fact]
        public async Task GetBeers_ReturnsAllBeers()
        {
            // Arrange
            var mockService = new Mock<IBeerService>();
            var controller = new BeersController(mockService.Object);

            var expectedBeers = new List<BeerDTO>
            {
                new BeerDTO {  },
                new BeerDTO {  },
            };
            var expectedTask = Task.FromResult<IEnumerable<BeerDTO>>(expectedBeers);
            mockService.Setup(s => s.GetBeers(It.IsAny<decimal?>(), It.IsAny<decimal?>())).Returns(expectedTask);

            // Act
            var result = await controller.GetBeers(null, null);

            // Assert
            var okObjectResult = Assert.IsType<OkObjectResult>(result.Result);
            var actualBeers = Assert.IsType<List<BeerDTO>>(okObjectResult.Value);

            Assert.Equal(expectedBeers, actualBeers);
            mockService.Verify(s => s.GetBeers(It.IsAny<decimal?>(), It.IsAny<decimal?>()), Times.Once);
        }

        [Fact]
        public async Task UpdateBeer_ExistingBeer_ReturnsNoContentResult()
        {
            // Arrange
            var mockService = new Mock<IBeerService>();
            var controller = new BeersController(mockService.Object);

            var beerId = 1;
            var beerToUpdate = new BeerDTO {  };

            mockService.Setup(s => s.UpdateBeer(beerId, It.IsAny<BeerDTO>()));

            // Act
            var result = await controller.PutBeer(beerId, beerToUpdate);

            // Assert
            Assert.IsType<NoContentResult>(result);
            mockService.Verify(s => s.UpdateBeer(beerId, beerToUpdate), Times.Once);
        }

        [Fact]
        public async Task GetBeerById_ExistingBeer_ReturnsBeer()
        {
            // Arrange
            var mockService = new Mock<IBeerService>();
            var controller = new BeersController(mockService.Object);

            var beerId = 1;
            var expectedBeer = new BeerDTO {  };
            mockService.Setup(s => s.GetBeerById(beerId)).ReturnsAsync(expectedBeer);


            // Act
            var result = await controller.GetBeerById(beerId);

            // Assert
            var okObjectResult = Assert.IsType<OkObjectResult>(result.Result);
            var actualBeer = Assert.IsType<BeerDTO>(okObjectResult.Value);

            Assert.Equal(expectedBeer, actualBeer);
            mockService.Verify(s => s.GetBeerById(beerId), Times.Once);
        }
    }
}
