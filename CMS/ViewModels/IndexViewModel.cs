using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CMS.ViewModels
{
    public class IndexViewModel<T> where T : class
    {
        public IEnumerable<T> Items;
    }
}
