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
        private readonly Mock<IService> serviceMock = new Mock<IService>();

        public UrlControllerTest()
        {
            _controller = new UrlController(serviceMock.Object);
        }

        [Fact]
        public void GetUrlList_WithUnexistingItem_ReturnsNotFoundResult()
        {
            // Arrange        
            serviceMock.Setup(serv => serv.Get(It.IsAny<string>()))
                .Returns((UrlList)null);
            
            // Act
            var result = _controller.GetUrlList("test");

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public void GetUrlList_WithExistingItem_ReturnsOk()
        {
            string title = "test";
            string description = "";
            var expectedItem = new UrlList()
            {
                Title = title,
                List = new List<UrlItem>(),
                Description = description,
            };

            // Arrange
            serviceMock.Setup(serv => serv.Get(expectedItem.Title))
                .Returns(expectedItem);

            // Act
            var result = _controller.GetUrlList(expectedItem.Title);

            // Assert\
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public void PostUrlList_UnexistingItem_ReturnsOk()
        {
            string title = "test";
            string description = "";
            var unexistingItem = new UrlList()
            {
                Title = title,
                List = new List<UrlItem>(),
                Description = description,
            };

            // Arrange
            serviceMock.Setup(serv => serv.Add(unexistingItem))
                .Returns(true);

            // Act
            var result = _controller.PostUrlList(unexistingItem);

            // Assert
            Assert.IsType<StatusCodeResult>(result);
        }

        [Fact]
        public void PostUrlList_WithExistingItem_ReturnsBadRequest()
        {
            string title = "test";
            string description = "";
            var existingItem = new UrlList()
            {
                Title = title,
                List = new List<UrlItem>(),
                Description = description,
            };
            
            // Arrange
            serviceMock.Setup(serv => serv.Add(existingItem))
                .Returns(false);

            // Act
            var result = _controller.PostUrlList(existingItem);

            // Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }
    }
}
