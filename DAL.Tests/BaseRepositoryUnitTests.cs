using System;
using Xunit;
using Catalog.DAL.Repositories.Impl;
using Catalog.DAL.EF;
using Microsoft.EntityFrameworkCore;
using Catalog.DAL.Entities;
using Catalog.DAL.Repositories.Interfaces;
using System.Linq;
using Moq;

namespace DAL.Tests
{
    class TestStreetRepository
        : BaseRepository<Product>
    {
        public TestStreetRepository(DbContext context) 
            : base(context)
        {
        }
    }

    public class BaseRepositoryUnitTests
    {

        [Fact]
        public void Create_InputStreetInstance_CalledAddMethodOfDBSetWithStreetInstance()
        {
            // Arrange
            DbContextOptions opt = new DbContextOptionsBuilder<CatalogContext>()
                .Options;
            var mockContext = new Mock<CatalogContext>(opt);
            var mockDbSet = new Mock<DbSet<Product>>();
            mockContext
                .Setup(context => 
                    context.Set<Product>(
                        ))
                .Returns(mockDbSet.Object);
            //EFUnitOfWork uow = new EFUnitOfWork(mockContext.Object);
            var repository = new TestStreetRepository(mockContext.Object);

            Product expectedStreet = new Mock<Product>().Object;

            //Act
            repository.Create(expectedStreet);

            // Assert
            mockDbSet.Verify(
                dbSet => dbSet.Add(
                    expectedStreet
                    ), Times.Once());
        }

        [Fact]
        public void Delete_InputId_CalledFindAndRemoveMethodsOfDBSetWithCorrectArg()
        {
            // Arrange
            DbContextOptions opt = new DbContextOptionsBuilder<CatalogContext>()
                .Options;
            var mockContext = new Mock<CatalogContext>(opt);
            var mockDbSet = new Mock<DbSet<Product>>();
            mockContext
                .Setup(context =>
                    context.Set<Product>(
                        ))
                .Returns(mockDbSet.Object);
            //EFUnitOfWork uow = new EFUnitOfWork(mockContext.Object);
            //IStreetRepository repository = uow.Streets;
            var repository = new TestStreetRepository(mockContext.Object);

            Product expectedStreet = new Product() { ProductID = 1};
            mockDbSet.Setup(mock => mock.Find(expectedStreet.ProductID)).Returns(expectedStreet);

            //Act
            repository.Delete(expectedStreet.ProductID);

            // Assert
            mockDbSet.Verify(
                dbSet => dbSet.Find(
                    expectedStreet.ProductID
                    ), Times.Once());
            mockDbSet.Verify(
                dbSet => dbSet.Remove(
                    expectedStreet
                    ), Times.Once());
        }

        [Fact]
        public void Get_InputId_CalledFindMethodOfDBSetWithCorrectId()
        {
            // Arrange
            DbContextOptions opt = new DbContextOptionsBuilder<CatalogContext>()
                .Options;
            var mockContext = new Mock<CatalogContext>(opt);
            var mockDbSet = new Mock<DbSet<Product>>();
            mockContext
                .Setup(context =>
                    context.Set<Product>(
                        ))
                .Returns(mockDbSet.Object);

            Product expectedStreet = new Product() { ProductID = 1 };
            mockDbSet.Setup(mock => mock.Find(expectedStreet.ProductID))
                    .Returns(expectedStreet);
            var repository = new TestStreetRepository(mockContext.Object);

            //Act
            var actualStreet = repository.Get(expectedStreet.ProductID);

            // Assert
            mockDbSet.Verify(
                dbSet => dbSet.Find(
                    expectedStreet.ProductID
                    ), Times.Once());
            Assert.Equal(expectedStreet, actualStreet);
        }

      
    }
}
