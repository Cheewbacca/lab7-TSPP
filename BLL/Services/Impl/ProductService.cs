using Catalog.BLL.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using Catalog.DAL.Entities;
using Catalog.BLL.DTO;
using Catalog.DAL.Repositories.Interfaces;
using AutoMapper;
using Catalog.DAL.UnitOfWork;
using OSBB.Security;
using OSBB.Security.Identity;

namespace Catalog.BLL.Services.Impl
{
    public class ProductService
        : IProductService
    {
        private readonly IUnitOfWork _database;
        private int pageSize = 10;

        public ProductService( 
            IUnitOfWork unitOfWork)
        {
            if (unitOfWork == null)
            {
                throw new ArgumentNullException(
                    nameof(unitOfWork));
            }
            _database = unitOfWork;
        }

        /// <exception cref="MethodAccessException"></exception>
        public IEnumerable<ProductDTO> GetProducts(int pageNumber)
        {
            var user = SecurityContext.GetUser();
            var userType = user.GetType();
            if (userType != typeof(Director)
                && userType != typeof(Accountant))
            {
                throw new MethodAccessException();
            }
            var catalogID = user.CatalogID;
            var streetsEntities = 
                _database
                    .Streets
                    .Find(z => z.ProductID == catalogID, pageNumber, pageSize);
            var mapper = 
                new MapperConfiguration(
                    cfg => cfg.CreateMap<Product, ProductDTO>()
                    ).CreateMapper();
            var streetsDto = 
                mapper
                    .Map<IEnumerable<Product>, List<ProductDTO>>(
                        streetsEntities);
            return streetsDto;
        }

        public void AddStreet(ProductDTO street)
        {
            var user = SecurityContext.GetUser();
            var userType = user.GetType();
            if (userType != typeof(Director)
                || userType != typeof(Accountant))
            {
                throw new MethodAccessException();
            }
            if (street == null)
            {
                throw new ArgumentNullException(nameof(street));
            }

            validate(street);

            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<ProductDTO, Product>()).CreateMapper();
            var streetEntity = mapper.Map<ProductDTO, Product>(street);
            _database.Streets.Create(streetEntity);
        }

        private void validate(ProductDTO street)
        {
            if (string.IsNullOrEmpty(street.Name))
            {
                throw new ArgumentException("Name повинне містити значення!");
            }
        }
    }
}
