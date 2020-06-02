using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Catalog.DAL.Entities
{
    public class OSBB
    {
        public int OSBBID { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Director { get; set; }

        public List<Street> Streets { get; set; }
    }
}
