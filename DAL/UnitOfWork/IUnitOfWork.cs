using Catalog.DAL.Entities;
using Catalog.DAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Catalog.DAL.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        IOSBBRepository OSBBs { get; }
        IStreetRepository Streets { get; }
        void Save();
    }
}
