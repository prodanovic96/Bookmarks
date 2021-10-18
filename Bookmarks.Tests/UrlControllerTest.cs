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
            Assert.IsType<BadRequestObjectResult>(result);
        }
    }
}
