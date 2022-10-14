using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EducationPortal.BLL.DTO
{
    public class BookMaterialDTO:MaterialDTO
    {
        public string Author { get; set; }

        public int Pages { get; set; }

        public string Format { get; set; }

        public int Year { get; set; }
    }
}
