using Catalog.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Catalog.DAL.EF
{
    public class OSBBContext
        : DbContext
    {
        public DbSet<OSBB> Osbbs { get; set; }
        public DbSet<Street> Streets { get; set; }

        public OSBBContext(DbContextOptions options)
            : base(options)
        {
        }

    }
}
