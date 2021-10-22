using Bookmarks.Api.Controllers;
using Bookmarks.Api.Helper;
using Bookmarks.Api.Models;
using Bookmarks.Api.Repository;
using Bookmarks.Api.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using Xunit;

namespace Bookmarks.Tests
{
    public class UrlControllerTest
    {
        private readonly UrlController _controller;
        private readonly Mock<IDataBaseServices> dataBaseServicesMock = new Mock<IDataBaseServices>();

        public UrlControllerTest()
        {
            _controller = new UrlController(dataBaseServicesMock.Object);
        }

        [Fact]
        public void GetUrlList_WithUnexistingItem_ReturnsNotFoundResult()
        {
            // Arrange        
            dataBaseServicesMock.Setup(serv => serv.Get(It.IsAny<string>()))
                .Returns((UrlList)null);
            
            // Act
            var result = _controller.GetUrlList("test");

            // Assert
            dataBaseServicesMock.Verify(serv => serv.Get(It.IsAny<string>()),Times.Once);
            Assert.IsType<NotFoundObjectResult>(result);
        }

        [Fact]
        public void GetUrlList_WithExistingItem_ReturnsOk()
        {
            string title = "test";
            string description = "";
            var expectedItem = new UrlList()
            {
                Title = title,
                Items = new List<UrlItem>(),
                Description = description,
            };

            // Arrange
            dataBaseServicesMock.Setup(serv => serv.Get(expectedItem.Title))
                .Returns(expectedItem);

            // Act
            var result = _controller.GetUrlList(expectedItem.Title);

            // Assert\
            dataBaseServicesMock.Verify(serv => serv.Get(It.IsAny<string>()),Times.Exactly(2));
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public void PostUrlList_UnexistingItem_ReturnsOk()
        {
            // Arrange
            dataBaseServicesMock.Setup(serv => serv.Add(It.IsAny<UrlList>()))
                .Returns(true);

            // Act
            var result = _controller.PostUrlList(new UrlList());

            // Assert
            dataBaseServicesMock.Verify(serv => serv.Add(It.IsAny<UrlList>()));
            Assert.IsType<StatusCodeResult>(result);
        }

        [Fact]
        public void PostUrlList_WithExistingItem_ReturnsBadRequest()
        {
            // Arrange
            dataBaseServicesMock.Setup(serv => serv.Add(It.IsAny<UrlList>()))
                .Returns(false);

            // Act
            var result = _controller.PostUrlList(new UrlList());

            // Assert
            dataBaseServicesMock.Verify(serv => serv.Add(It.IsAny<UrlList>()));
            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public void IsNameReserved_WithUnexistingItem_ReturnsOk()
        {
            // Arrange
            dataBaseServicesMock.Setup(serv => serv.Existing(It.IsAny<string>()))
                .Returns(false);

            // Act
            var result = _controller.IsNameReserved("test");

            // Assert\
            dataBaseServicesMock.Verify(serv => serv.Existing(It.IsAny<string>()));
            Assert.IsType<OkResult>(result);
        }

        [Fact]
        public void IsNameReserved_WithExistingItem_ReturnsBadRequest()
        {
            string title = "test";
            string description = "";
            var existingItem = new UrlList()
            {
                Title = title,
                Items = new List<UrlItem>(),
                Description = description,
            };
            // Arrange
            dataBaseServicesMock.Setup(serv => serv.Existing(existingItem.Title))
                .Returns(true);

            // Act
            var result = _controller.IsNameReserved(existingItem.Title);

            // Assert
            dataBaseServicesMock.Verify(serv => serv.Existing(It.IsAny<string>()));
            Assert.IsType<NotFoundObjectResult>(result);
        }
    }
}
