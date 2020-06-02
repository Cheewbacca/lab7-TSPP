using System;
using System.Collections.Generic;
using System.Text;

namespace Catalog.DAL.Entities
{
    public class Street
    {
        public int StreetId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public int OSBBID { get; set; }
        public OSBB OSBB { get; set; }
    }
}
