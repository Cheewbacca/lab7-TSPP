using System.Collections.Generic;

namespace Catalog.DAL.Entities
{
    public class OSBB
    {
        public int OSBBID { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Director { get; set; }
        public IEnumerable<Street> Streets { get; set; }
        //public IEnumerable<User> Users { get; set; }
    }
}