using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Catalog.DAL.Entities
{
    public class Catalog
    {
        public int CatalogID { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Director { get; set; }

        public List<Product> Products { get; set; }
    }
}
