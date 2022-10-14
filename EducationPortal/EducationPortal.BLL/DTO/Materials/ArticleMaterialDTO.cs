using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EducationPortal.BLL.DTO
{
    public class ArticleMaterialDTO : MaterialDTO
    {
        public DateTime DateOfPublish { get; set; }

        public string Resource { get; set; }
    }
}
