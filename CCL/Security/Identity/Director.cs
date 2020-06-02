﻿using System;
using System.Collections.Generic;
using System.Text;

namespace OSBB.Security.Identity
{
    public class Director
        : Client
    {
        public Director(int userId, string name, int catalogId) 
            : base(userId, name, catalogId, nameof(Director))
        {
        }
    }
}
