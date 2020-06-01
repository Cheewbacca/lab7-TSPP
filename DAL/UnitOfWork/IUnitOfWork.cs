using Catalog.DAL.Repositories.Interfaces;
using System;

namespace Catalog.DAL.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        IOSBBRepository OSBBs { get; }
        IStreetRepository Streets { get; }
        void Save();
    }
}