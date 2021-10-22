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
            urlRepositoryMock.Setup(repo => repo.Existing(unexistingItem.Title))
                .Returns(false);
            // Act
            var result = _dataBaseServices.Add(unexistingItem);

            // Assert
            urlRepositoryMock.Verify(repo => repo.Add(unexistingItem), Times.Once);
            urlRepositoryMock.Verify(repo => repo.Existing(It.IsAny<string>()));
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
            urlRepositoryMock.Setup(repo => repo.Existing(existingItem.Title))
                .Returns(true);

            // Act
            var result = _dataBaseServices.Add(existingItem);

            // Assert
            urlRepositoryMock.Verify(repo => repo.Existing(It.IsAny<string>()), Times.Once);
            urlRepositoryMock.Verify(repo => repo.Add(existingItem), Times.Never);
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
            urlRepositoryMock.Setup(repo => repo.Existing(unexistingItem.Title))
                .Returns(true);
 
            // Act
            var result = _dataBaseServices.Add(unexistingItem);

            // Assert
            urlRepositoryMock.Verify(repo => repo.Existing(It.IsAny<string>()), Times.Exactly(2));
            urlRepositoryMock.Verify(repo => repo.Add(unexistingItem));
            stringHelperMock.Verify(str => str.RandomString(It.IsAny<int>()), Times.AtLeastOnce);
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
            urlRepositoryMock.Verify(serv => serv.Get(It.IsAny<string>()));
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
            urlRepositoryMock.Verify(serv => serv.Get(It.IsAny<string>()));
            Assert.Null(result);
        }

        [Fact]
        public void Existing_WithExistingItem_ReturnsTrue()
        {
            // Arrange
            urlRepositoryMock.Setup(serv => serv.Existing(It.IsAny<string>()))
                .Returns(true);

            // Act
            var result = _dataBaseServices.Existing("test");

            // Assert
            urlRepositoryMock.Verify(serv => serv.Existing(It.IsAny<string>()));
            Assert.True(result);
        }

        [Fact]
        public void Existing_WithUnexistingItem_ReturnsFalse()
        {
            // Arrange
            urlRepositoryMock.Setup(serv => serv.Existing(It.IsAny<string>()))
                .Returns(false);

            // Act
            var result = _dataBaseServices.Existing("test");

            // Assert
            urlRepositoryMock.Verify(serv => serv.Existing(It.IsAny<string>()));
            Assert.False(result);
        }
    }
}
