using Catalog.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace Catalog.DAL.EF
{
    public class OSBBContext
        : DbContext
    {
        public DbSet<OSBB> Phones { get; set; }
        public DbSet<Street> Orders { get; set; }
​
        public OSBBContext(DbContextOptions options)
            : base(options)
        {
        }
    }
}