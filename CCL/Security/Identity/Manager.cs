using System;
using System.Collections.Generic;
using System.Text;

namespace OSBB.Security.Identity
{
    public class Manager
        : Client
    {
        public Manager(int userId, string name, int catalogId) 
            : base(userId, name, catalogId, nameof(Manager))
        {
        }
    }
}
