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
        : BaseRepository<Street>
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
            DbContextOptions opt = new DbContextOptionsBuilder<OSBBContext>()
                .Options;
            var mockContext = new Mock<OSBBContext>(opt);
            var mockDbSet = new Mock<DbSet<Street>>();
            mockContext
                .Setup(context => 
                    context.Set<Street>(
                        ))
                .Returns(mockDbSet.Object);
            //EFUnitOfWork uow = new EFUnitOfWork(mockContext.Object);
            var repository = new TestStreetRepository(mockContext.Object);

            Street expectedStreet = new Mock<Street>().Object;

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
            DbContextOptions opt = new DbContextOptionsBuilder<OSBBContext>()
                .Options;
            var mockContext = new Mock<OSBBContext>(opt);
            var mockDbSet = new Mock<DbSet<Street>>();
            mockContext
                .Setup(context =>
                    context.Set<Street>(
                        ))
                .Returns(mockDbSet.Object);
            //EFUnitOfWork uow = new EFUnitOfWork(mockContext.Object);
            //IStreetRepository repository = uow.Streets;
            var repository = new TestStreetRepository(mockContext.Object);

            Street expectedStreet = new Street() { StreetId = 1};
            mockDbSet.Setup(mock => mock.Find(expectedStreet.StreetId)).Returns(expectedStreet);

            //Act
            repository.Delete(expectedStreet.StreetId);

            // Assert
            mockDbSet.Verify(
                dbSet => dbSet.Find(
                    expectedStreet.StreetId
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
            DbContextOptions opt = new DbContextOptionsBuilder<OSBBContext>()
                .Options;
            var mockContext = new Mock<OSBBContext>(opt);
            var mockDbSet = new Mock<DbSet<Street>>();
            mockContext
                .Setup(context =>
                    context.Set<Street>(
                        ))
                .Returns(mockDbSet.Object);

            Street expectedStreet = new Street() { StreetId = 1 };
            mockDbSet.Setup(mock => mock.Find(expectedStreet.StreetId))
                    .Returns(expectedStreet);
            var repository = new TestStreetRepository(mockContext.Object);

            //Act
            var actualStreet = repository.Get(expectedStreet.StreetId);

            // Assert
            mockDbSet.Verify(
                dbSet => dbSet.Find(
                    expectedStreet.StreetId
                    ), Times.Once());
            Assert.Equal(expectedStreet, actualStreet);
        }

      
    }
}
