using System;
using Xunit;
using Catalog.DAL.Repositories.Impl;
using Catalog.DAL.EF;
using Microsoft.EntityFrameworkCore;
using Catalog.DAL.Entities;
using Catalog.DAL.Repositories.Interfaces;
using System.Linq;

namespace DAL.Tests
{
    public class ProductRepositoryInMemoryDBTests
    {
        public CatalogContext Context => SqlLiteInMemoryContext();

        private CatalogContext SqlLiteInMemoryContext()
        {

            var options = new DbContextOptionsBuilder<CatalogContext>()
                .UseSqlite("DataSource=:memory:")
                .Options;

            var context = new CatalogContext(options);
            context.Database.OpenConnection();
            context.Database.EnsureCreated();
            return context;
        }

        [Fact]
        public void Create_InputProductWithId0_SetProductId1()
        {
            // Arrange
            int expectedListCount = 1;
            var context = SqlLiteInMemoryContext();
            EFUnitOfWork uow = new EFUnitOfWork(context);
            Catalog.DAL.Repositories.Interfaces.ProductRepository repository = uow.Streets;

            Product Product = new Product()
            {
                CatalogID = 5,
                Name = "test",
                Description = "testD",
                Catalog = new Catalog.DAL.Entities.Catalog() { CatalogID = 5}
            };

            //Act
            repository.Create(Product);
            uow.Save();
            var factListCount = context.Products.Count();

            // Assert
            Assert.Equal(expectedListCount, factListCount);
        }

        [Fact]
        public void Delete_InputExistProductId_Removed()
        {
            // Arrange
            int expectedListCount = 0;
            var context = SqlLiteInMemoryContext();
            EFUnitOfWork uow = new EFUnitOfWork(context);
            Catalog.DAL.Repositories.Interfaces.ProductRepository repository = uow.Streets;
            Product Product = new Product()
            {
                //ProductId = 1,
                CatalogID = 5,
                Name = "test",
                Description = "testD",
                Catalog = new Catalog.DAL.Entities.Catalog() { CatalogID = 5 }
            };
            context.Products.Add(Product);
            context.SaveChanges();

            //Act
            repository.Delete(Product.ProductID);
            uow.Save();
            var factProductCount = context.Products.Count();

            // Assert
            Assert.Equal(expectedListCount, factProductCount);
        }

        [Fact]
        public void Get_InputExistProductId_ReturnProduct()
        {
            // Arrange
            var context = SqlLiteInMemoryContext();
            EFUnitOfWork uow = new EFUnitOfWork(context);
            Catalog.DAL.Repositories.Interfaces.ProductRepository repository = uow.Streets;
            Product expectedProduct = new Product()
            {
                //ProductId = 1,
                CatalogID = 5,
                Name = "test",
                Description = "testD",
                Catalog = new Catalog.DAL.Entities.Catalog() { CatalogID = 5 }
            };
            context.Products.Add(expectedProduct);
            context.SaveChanges();

            //Act
            var factProduct = repository.Get(expectedProduct.ProductID);

            // Assert
            Assert.Equal(expectedProduct, factProduct);
        }
    }
}
