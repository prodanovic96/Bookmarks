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
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Bookmarks.Tests
{
    public class ServiceTest
    {
        private readonly Service _service;
        private readonly Mock<ILogger<UrlController>> loggerMock = new Mock<ILogger<UrlController>>();
        private readonly Mock<IStringHelper> stringHelperMock = new Mock<IStringHelper>();
        private readonly Mock<IDataBaseRepository> dataBaseMock = new Mock<IDataBaseRepository>();
        private readonly Mock<IService> serviceMock = new Mock<IService>();

        public ServiceTest()
        {
            _service = new Service(loggerMock.Object, dataBaseMock.Object, stringHelperMock.Object);
        }

        [Fact]
        public void Add_WithExistingItem_ReturnsFalse()
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
            dataBaseMock.Setup(serv => serv.AddToDataBase(existingItem.Title, existingItem))
                .Returns(false);

            // Act
            var result = _service.Add(existingItem);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void Add_WithUnexistingItem_ReturnsTrue()
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
            dataBaseMock.Setup(serv => serv.AddToDataBase(existingItem.Title, existingItem))
                .Returns(true);

            // Act
            var result = _service.Add(existingItem);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void Get_WithUnexistingItem_ReturnsNull()
        {
            // Arrange
            serviceMock.Setup(serv => serv.Exists(It.IsAny<string>()))
                .Returns(null);

            // Act
            var result = _service.Get("test");

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public void Get_WithExistingItem_ReturnsItem()
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
            serviceMock.Setup(serv => serv.Exists(existingItem.Title))
                .Returns(true);

            dataBaseMock.Setup(repo => repo.Contain(existingItem.Title))
                .Returns(true);

            dataBaseMock.Setup(repo => repo.GetFromDataBase(existingItem.Title))
                .Returns(existingItem);

            // Act
            var result = _service.Get(existingItem.Title);

            // Assert
            Assert.Equal(result, existingItem);
        }

        [Fact]
        public void Exists_WithUnexistingItem_ReturnsFalse()
        {
            // Arrange
            serviceMock.Setup(serv => serv.Exists(It.IsAny<string>()))
                .Returns(false);

            // Act
            var result = _service.Exists("test");

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void Exists_WithExistingItemNull_ReturnsFalse()
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
            dataBaseMock.Setup(repo => repo.Contain(existingItem.Title))
                .Returns(true);
            dataBaseMock.Setup(repo => repo.GetFromDataBase(existingItem.Title))
                .Returns((UrlList)null);

            // Act
            var result = _service.Exists(existingItem.Title);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void Exists_WithExistingItemNotNull_ReturnsTrue()
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
            dataBaseMock.Setup(repo => repo.Contain(existingItem.Title))
                .Returns(true);
            dataBaseMock.Setup(repo => repo.GetFromDataBase(existingItem.Title))
                .Returns(existingItem);

            // Act
            var result = _service.Exists(existingItem.Title);

            // Assert
            Assert.True(result);
        }
    }
}
