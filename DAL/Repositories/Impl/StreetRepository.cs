using Catalog.DAL.EF;
using Catalog.DAL.Entities;
using DAL.Repositories.Interfaces;

namespace Catalog.DAL.Repositories.Impl
{
    public class StreetRepository
        : BaseRepository<Street>, IStreetRepository
    {
        internal StreetRepository(OSBBContext context)
            : base(context)
        {
        }
    }
}