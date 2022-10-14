using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EducationPortal.DAL.Entities.PagedList
{
    public class PagedList<T>
    {
        public IEnumerable<T>? Items { get; set; }

        public int Count { get; set; }
    }
}
