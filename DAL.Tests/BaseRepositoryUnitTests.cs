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
    class TestProductRepository
        : BaseRepository<Product>
    {
        public TestProductRepository(DbContext context) 
            : base(context)
        {
        }
    }

    public class BaseRepositoryUnitTests
    {

        [Fact]
        public void Create_InputProductInstance_CalledAddMethodOfDBSetWithProductInstance()
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
            var repository = new TestProductRepository(mockContext.Object);

            Product expectedProduct = new Mock<Product>().Object;

            //Act
            repository.Create(expectedProduct);

            // Assert
            mockDbSet.Verify(
                dbSet => dbSet.Add(
                    expectedProduct
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
            //IProductRepository repository = uow.Products;
            var repository = new TestProductRepository(mockContext.Object);

            Product expectedProduct = new Product() { ProductID = 1};
            mockDbSet.Setup(mock => mock.Find(expectedProduct.ProductID)).Returns(expectedProduct);

            //Act
            repository.Delete(expectedProduct.ProductID);

            // Assert
            mockDbSet.Verify(
                dbSet => dbSet.Find(
                    expectedProduct.ProductID
                    ), Times.Once());
            mockDbSet.Verify(
                dbSet => dbSet.Remove(
                    expectedProduct
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

            Product expectedProduct = new Product() { ProductID = 1 };
            mockDbSet.Setup(mock => mock.Find(expectedProduct.ProductID))
                    .Returns(expectedProduct);
            var repository = new TestProductRepository(mockContext.Object);

            //Act
            var actualProduct = repository.Get(expectedProduct.ProductID);

            // Assert
            mockDbSet.Verify(
                dbSet => dbSet.Find(
                    expectedProduct.ProductID
                    ), Times.Once());
            Assert.Equal(expectedProduct, actualProduct);
        }

      
    }
}
