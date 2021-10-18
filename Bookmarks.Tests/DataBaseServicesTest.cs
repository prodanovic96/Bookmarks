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
    public class DataBaseServicesTest
    {
        private readonly DataBaseServices _dataBaseServices;
        private readonly Mock<IUrlRepository> urlRepositoryMock = new Mock<IUrlRepository>();
        private readonly Mock<ILogger<UrlController>> loggerMock = new Mock<ILogger<UrlController>>();
        private readonly Mock<IStringHelper> stringHelperMock = new Mock<IStringHelper>();

        public DataBaseServicesTest()
        {
            _dataBaseServices = new DataBaseServices(urlRepositoryMock.Object, loggerMock.Object, stringHelperMock.Object);
        }

        [Fact]
        public void Add_WithUnexistingItem_ReturnsTrue()
        {
            string title = "test";
            string description = "";
            var unexistingItem = new UrlList()
            {
                Title = title,
                Items = new List<UrlItem>(),
                Description = description,
            };

            // Arrange
            urlRepositoryMock.Setup(repo => repo.Add(unexistingItem)); 
            urlRepositoryMock.Setup(repo => repo.Get(unexistingItem.Title))
                .Returns((UrlList)null);
            // Act
            var result = _dataBaseServices.Add(unexistingItem);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void Add_WithExistingItem_ReturnsFalse()
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
            urlRepositoryMock.Setup(repo => repo.Add(existingItem));
            urlRepositoryMock.Setup(repo => repo.Get(existingItem.Title))
                .Returns(existingItem);

            // Act
            var result = _dataBaseServices.Add(existingItem);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void Add_WithUnexistingItemEmptyTitle_ReturnsTrue()
        {
            int length = 7;
            string title = "";
            string description = "";
            var unexistingItem = new UrlList()
            {
                Title = title,
                Items = new List<UrlItem>(),
                Description = description,
            };

            // Arrange
            urlRepositoryMock.Setup(repo => repo.Add(unexistingItem));
            stringHelperMock.Setup(str => str.RandomString(length))
                .Returns("teststr");
            urlRepositoryMock.Setup(repo => repo.Get(unexistingItem.Title))
                .Returns((UrlList)null);

            // Act
            var result = _dataBaseServices.Add(unexistingItem);

            // Assert
            Assert.True(result);
            Assert.Equal(unexistingItem.Title.Length, length);
        }

        [Fact]
        public void Get_WithExistingItem_ReturnsItem()
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
            urlRepositoryMock.Setup(serv => serv.Get(existingItem.Title))
                .Returns(existingItem);

            // Act
            var result = _dataBaseServices.Get(existingItem.Title);

            // Assert
            Assert.IsType<UrlList>(result);
        }

        [Fact]
        public void Get_WithUnexistingItem_ReturnsNull()
        {
            // Arrange
            urlRepositoryMock.Setup(serv => serv.Get("test"))
                .Returns((UrlList)null);

            // Act
            var result = _dataBaseServices.Get("test");

            // Assert
            Assert.Null(result);
        }
    }
}
