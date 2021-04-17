using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Text;

namespace Data
{
    class Context : DbContext
    {
        public Context() : base("name=ConnectionString")
        {
        }
    }
}
