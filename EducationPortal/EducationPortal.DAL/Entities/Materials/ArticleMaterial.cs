using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EducationPortal.DAL.Entities
{
    public class ArticleMaterial : Material
    {
        public DateTime DateOfPublish { get; set; }

        public string Resource { get; set; }
    }
}
