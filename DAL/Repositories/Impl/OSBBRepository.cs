using Catalog.DAL.EF;
using DAL.Repositories.Interfaces;

namespace Catalog.DAL.Repositories.Impl
{
    public class OSBBRepository
        : BaseRepository<OSBB>, OSBB
    {
​
        internal OSBBRepository(OSBBContext context)
            : base(context)
        {
        }
    }
}