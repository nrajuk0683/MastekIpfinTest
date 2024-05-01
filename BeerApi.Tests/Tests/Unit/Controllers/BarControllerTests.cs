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
    public class BarControllerTests
    {
        [Fact]
        public async Task GetAllBars_ReturnsAllBars()
        {
            // Arrange
            var mockService = new Mock<IBarService>();
            var controller = new BarController(mockService.Object);

            var expectedBars = new List<BarDTO>
            {
                new BarDTO {  },
                new BarDTO {  },
            };

            mockService.Setup(s => s.GetBars()).ReturnsAsync(expectedBars);

            // Act
            var result = await controller.GetBars();

            // Assert
            Assert.NotNull(result);
            mockService.Verify(s => s.GetBars(), Times.Once);
        }

        [Fact]
        public async Task PostBar_ReturnsCreatedResult()
        {
            // Arrange
            var mockService = new Mock<IBarService>();
            var controller = new BarController(mockService.Object);

            var barToAdd = new BarDTO { Name="Bar1" };

            mockService.Setup(s => s.AddBar(It.IsAny<BarDTO>())).ReturnsAsync(barToAdd);

            // Act
            var result = await controller.PostBar(barToAdd);

            // Assert
            var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result.Result);
            Assert.Equal("GetBarById", createdAtActionResult.ActionName);
            mockService.Verify(s => s.AddBar(It.IsAny<BarDTO>()), Times.Once);
        }

        [Fact]
        public async Task GetBarById_ReturnsBar()
        {
            // Arrange
            var mockService = new Mock<IBarService>();
            var controller = new BarController(mockService.Object);

            var barId = 1;
            var expectedBar = new BarDTO {  };

            mockService.Setup(s => s.GetBarById(barId)).ReturnsAsync(expectedBar);

            // Act
            var result = await controller.GetBarById(barId);

            // Assert
            var actionResult = Assert.IsType<ActionResult<BarDTO>>(result);
            var okObjectResult = Assert.IsType<OkObjectResult>(actionResult.Result);
            var actualBar = Assert.IsType<BarDTO>(okObjectResult.Value);

            Assert.Equal(expectedBar, actualBar);
            mockService.Verify(s => s.GetBarById(barId), Times.Once);
        }

        [Fact]
        public async Task AddBarBeerLink_ReturnsCreatedResult()
        {
            // Arrange
            var mockService = new Mock<IBarService>();
            var controller = new BarController(mockService.Object);

            var barBeerToAdd = new BarBeerDTO {  };

            mockService.Setup(s => s.AddBarBeerLink(It.IsAny<BarBeerDTO>())).ReturnsAsync(barBeerToAdd);

            // Act
            var result = await controller.AddBarBeerLink(barBeerToAdd);

            // Assert
            var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result.Result);
            Assert.Equal("GetBarByIdWithBeers", createdAtActionResult.ActionName);
            mockService.Verify(s => s.AddBarBeerLink(It.IsAny<BarBeerDTO>()), Times.Once);
        }

        [Fact]
        public async Task GetAllBarsWithBeers_ReturnsAllBarsWithBeers()
        {
            // Arrange
            var mockService = new Mock<IBarService>();
            var controller = new BarController(mockService.Object);

            var expectedBarsWithBeers = new List<BarDTO>
            {
                new BarDTO {  },
                new BarDTO {  },
            };

            mockService.Setup(s => s.GetAllBarsWithBeers()).ReturnsAsync(expectedBarsWithBeers);

            // Act
            var result = await controller.GetAllBarsWithBeers();

            // Assert
            var okObjectResult = Assert.IsType<OkObjectResult>(result.Result);
            var actualBarsWithBeers = Assert.IsType<List<BarDTO>>(okObjectResult.Value);

            Assert.Equal(expectedBarsWithBeers, actualBarsWithBeers);
            mockService.Verify(s => s.GetAllBarsWithBeers(), Times.Once);
        }
    }
}
