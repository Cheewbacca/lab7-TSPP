using Catalog.BLL.Services.Impl;
using Catalog.BLL.Services.Interfaces;
using Catalog.DAL.EF;
using Catalog.DAL.Entities;
using Catalog.DAL.Repositories.Interfaces;
using Catalog.DAL.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using Moq;
using OSBB.Security;
using OSBB.Security.Identity;
using System;
using System.Collections.Generic;
using Xunit;
using System.Linq;

namespace BLL.Tests
{
    public class ProductServiceTests
    {

        [Fact]
        public void Ctor_InputNull_ThrowArgumentNullException()
        {
            // Arrange
            IUnitOfWork nullUnitOfWork = null;

            // Act
            // Assert
            Assert.Throws<ArgumentNullException>(() => new ProductService(nullUnitOfWork));
        }

        [Fact]
        public void GetProducts_UserIsAdmin_ThrowMethodAccessException()
        {
            // Arrange
            Client user = new Admin(1, "test", 1);
            SecurityContext.SetUser(user);
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            IProductService productService = new ProductService(mockUnitOfWork.Object);

            // Act
            // Assert
            Assert.Throws<MethodAccessException>(() => productService.GetProducts(0));
        }

        [Fact]
        public void GetProducts_ProductFromDAL_CorrectMappingToProductDTO()
        {
            // Arrange
            Client user = new Director(1, "test", 1);
            SecurityContext.SetUser(user);
            var productService = GetProducts();

            // Act
            var actualproductDto = productService.GetProducts(0).First();

            // Assert
            Assert.True(
                actualproductDto.ProductID == 1
                && actualproductDto.Name == "testN"
                && actualproductDto.Description == "testD"
                );
        }

        IProductService GetProducts()
        {
            var mockContext = new Mock<IUnitOfWork>();
            var expectedProduct = new Product() { ProductID = 1, Name = "testN", Description = "testD", CatalogID = 2};
            var mockDbSet = new Mock<ProductRepository>();
            mockDbSet.Setup(z => 
                z.Find(
                    It.IsAny<Func<Product,bool>>(), 
                    It.IsAny<int>(), 
                    It.IsAny<int>()))
                  .Returns(
                    new List<Product>() { expectedProduct }
                    );
            mockContext
                .Setup(context =>
                    context.Streets)
                .Returns(mockDbSet.Object);

            IProductService productService = new ProductService(mockContext.Object);

            return productService;
        }
    }
}
