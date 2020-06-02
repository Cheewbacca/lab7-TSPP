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
        public void Create_InputStreetWithId0_SetStreetId1()
        {
            // Arrange
            int expectedListCount = 1;
            var context = SqlLiteInMemoryContext();
            EFUnitOfWork uow = new EFUnitOfWork(context);
            Catalog.DAL.Repositories.Interfaces.ProductRepository repository = uow.Streets;

            Product street = new Product()
            {
                CatalogID = 5,
                Name = "test",
                Description = "testD",
                Catalog = new Catalog.DAL.Entities.Catalog() { CatalogID = 5}
            };

            //Act
            repository.Create(street);
            uow.Save();
            var factListCount = context.Products.Count();

            // Assert
            Assert.Equal(expectedListCount, factListCount);
        }

        [Fact]
        public void Delete_InputExistStreetId_Removed()
        {
            // Arrange
            int expectedListCount = 0;
            var context = SqlLiteInMemoryContext();
            EFUnitOfWork uow = new EFUnitOfWork(context);
            Catalog.DAL.Repositories.Interfaces.ProductRepository repository = uow.Streets;
            Product street = new Product()
            {
                //StreetId = 1,
                CatalogID = 5,
                Name = "test",
                Description = "testD",
                Catalog = new Catalog.DAL.Entities.Catalog() { CatalogID = 5 }
            };
            context.Products.Add(street);
            context.SaveChanges();

            //Act
            repository.Delete(street.ProductID);
            uow.Save();
            var factStreetCount = context.Products.Count();

            // Assert
            Assert.Equal(expectedListCount, factStreetCount);
        }

        [Fact]
        public void Get_InputExistStreetId_ReturnStreet()
        {
            // Arrange
            var context = SqlLiteInMemoryContext();
            EFUnitOfWork uow = new EFUnitOfWork(context);
            Catalog.DAL.Repositories.Interfaces.ProductRepository repository = uow.Streets;
            Product expectedStreet = new Product()
            {
                //StreetId = 1,
                CatalogID = 5,
                Name = "test",
                Description = "testD",
                Catalog = new Catalog.DAL.Entities.Catalog() { CatalogID = 5 }
            };
            context.Products.Add(expectedStreet);
            context.SaveChanges();

            //Act
            var factStreet = repository.Get(expectedStreet.ProductID);

            // Assert
            Assert.Equal(expectedStreet, factStreet);
        }
    }
}
