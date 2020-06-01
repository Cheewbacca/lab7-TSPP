using Catalog.DAL.EF;
using Catalog.DAL.Entities;
using Catalog.DAL.Repositories.Interfaces;

namespace Catalog.DAL.Repositories.Impl
{
    public class OSBBRepository
        : BaseRepository<OSBB>, IOSBBRepository
    {
​
        internal OSBBRepository(OSBBContext context)
            : base(context)
        {
        }
    }
}