using Bookmarks.Api.Controllers;
using Bookmarks.Api.Helper;
using Bookmarks.Api.Models;
using Bookmarks.Api.Repository;
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
        private readonly Mock<ILogger<UrlController>> loggerMock = new Mock<ILogger<UrlController>>();
        private readonly Mock<IStringHelper> stringHelperMock = new Mock<IStringHelper>();
        private readonly Mock<IDataBaseRepository> dataBaseMock = new Mock<IDataBaseRepository>();

        public UrlControllerTest()
        {
            _controller = new UrlController(loggerMock.Object, dataBaseMock.Object, stringHelperMock.Object);
        }

        [Fact]
        public void GetUrlList_WithUnexistingItem_ReturnsBadRequst()
        {
            // Arrange        
            dataBaseMock.Setup(repo => repo.GetFromDataBase(It.IsAny<string>()))
                .Returns((UrlList)null);
            
            // Act
            var result = _controller.GetUrlList("test");

            // Assert
            Assert.IsType<BadRequestResult>(result);
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
            dataBaseMock.Setup(repo => repo.GetFromDataBase(expectedItem.Title))
                .Returns(expectedItem);
            dataBaseMock.Setup(repo => repo.Contain(expectedItem.Title))
               .Returns(true);

            // Act
            var result = _controller.GetUrlList(expectedItem.Title);

            // Assert\
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public void PostUrlList_ValidLinkWithUnexistingItem_ReturnsOk()
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
            dataBaseMock.Setup(repo => repo.AddToDataBase(unexistingItem.Title, unexistingItem))
                .Returns(true);
            dataBaseMock.Setup(repo => repo.Contain(unexistingItem.Title))
                .Returns(false);

            // Act
            var result = _controller.PostUrlList(unexistingItem);

            // Assert
            Assert.IsType<OkResult>(result);
        }

        [Fact]
        public void PostUrlList_ValidLinkWithExistingItem_ReturnsBadRequest()
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
            dataBaseMock.Setup(repo => repo.AddToDataBase(existingItem.Title, existingItem))
                .Returns(false);
            dataBaseMock.Setup(repo => repo.Contain(existingItem.Title))
                .Returns(true);
            // Act
            var result = _controller.PostUrlList(existingItem);

            // Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public void PostUrlList_InvalidLink_ReturnsBadRequest()
        {
            string title = "test";
            string description = "";
            var invalidLink = new UrlList()
            {
                Title = title,
                List = new List<UrlItem>(),
                Description = description,
            };
            UrlItem item = new UrlItem { Name = "test", Description = "", Link = "test" };
            invalidLink.List.Add(item);

            // Arrange
            stringHelperMock.Setup(repo => repo.UrlValidation(item.Link))
                .Returns(false);
 
            // Act
            var result = _controller.PostUrlList(invalidLink);

            // Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }
    }
}
